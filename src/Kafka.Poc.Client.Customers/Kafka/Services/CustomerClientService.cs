namespace Kafka.Poc.Client.Customers.Kafka.Services;

using System.Collections.Concurrent;
using System.Diagnostics;
using global::Kafka.Poc.Client.Customers.Kafka.Interfaces;

internal sealed class CustomerClientService : ICustomerClientService
{
    private readonly ICustomerMessageProcessingService customerMessageProcessingService;
    private readonly ConcurrentDictionary<string, (Task task, CancellationTokenSource cancellationToken)> tasks = new();

    public CustomerClientService(ICustomerMessageProcessingService customerMessageProcessingService)
        => this.customerMessageProcessingService = customerMessageProcessingService;

    public async Task SubscribeAsync(string consumerGroupId, string repositoryId, CancellationToken cancellationToken)
    {
        if (this.tasks.ContainsKey(repositoryId))
        {
            return;
        }

        var taskCancellationToken = new CancellationTokenSource();

        this.AddTask(repositoryId, this.ProcessMessageAsync(consumerGroupId, repositoryId, taskCancellationToken.Token), taskCancellationToken);

        await Task.CompletedTask;
    }

    public async Task UnSubscribeAsync(string repositoryId, CancellationToken cancellationToken)
    {
        this.TryRemoveTask(repositoryId);

        await Task.CompletedTask;
    }

    private void AddTask(string repositoryId, Task task, CancellationTokenSource cancellationTokenSource)
        => this.tasks.GetOrAdd(repositoryId, (task, cancellationTokenSource));

    private async Task ProcessMessageAsync(string consumerGroupId, string repositoryId, CancellationToken cancellationToken)
    {
        using (Activity.Current?.Source.StartActivity())
        {
            await this.customerMessageProcessingService.ProcessMessage(consumerGroupId, repositoryId, cancellationToken);
        }
    }

    private void TryRemoveTask(string repositoryId)
    {
        if (this.tasks.TryGetValue(repositoryId, out var taskInfo))
        {
            taskInfo.cancellationToken.Cancel(false);
            taskInfo.cancellationToken.Dispose();

            this.tasks.TryRemove(repositoryId, out _);
        }
    }
}
