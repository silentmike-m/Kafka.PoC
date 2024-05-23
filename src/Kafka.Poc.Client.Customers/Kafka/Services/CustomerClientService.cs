namespace Kafka.Poc.Client.Customers.Kafka.Services;

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text.Json;
using global::Kafka.Poc.Client.Customers.Customers.Commands;
using global::Kafka.Poc.Client.Customers.Kafka.Interfaces;
using global::Kafka.Poc.Client.Customers.Kafka.Models;
using MediatR;

internal sealed class CustomerClientService : ICustomerClientService
{
    private readonly IKafkaConsumerFactory consumerFactory;
    private readonly ISender mediator;
    private readonly ConcurrentDictionary<string, (Task task, CancellationTokenSource cancellationToken)> tasks = new();

    public CustomerClientService(IKafkaConsumerFactory consumerFactory, ISender mediator)
    {
        this.consumerFactory = consumerFactory;
        this.mediator = mediator;
    }

    public async Task SubscribeAsync(Guid consumerGroupId, Guid repositoryId, CancellationToken cancellationToken)
    {
        var taskId = repositoryId.ToString();

        if (this.tasks.ContainsKey(taskId))
        {
            return;
        }

        var taskCancellationToken = new CancellationTokenSource();

        this.AddTask(taskId, this.ProcessMessageAsync(consumerGroupId.ToString(), repositoryId.ToString(), taskCancellationToken.Token), taskCancellationToken);

        await Task.CompletedTask;
    }

    public async Task UnSubscribeAsync(Guid repositoryId, CancellationToken cancellationToken)
    {
        var taskId = repositoryId.ToString();

        this.TryRemoveTask(taskId);

        await Task.CompletedTask;
    }

    private void AddTask(string taskId, Task task, CancellationTokenSource cancellationTokenSource)
        => this.tasks.GetOrAdd(taskId, (task, cancellationTokenSource));

    private async Task ConsumeMessageAsync(string messageValue, string partitionId, CancellationToken cancellationToken)
    {
        var customerMessage = JsonSerializer.Deserialize<CustomerMessage>(messageValue);

        if (customerMessage is null)
        {
            return;
        }

        var request = new AddCustomer
        {
            FirstName = customerMessage.FirstName,
            Id = customerMessage.Id,
            LastName = customerMessage.LastName,
            RepositoryId = Guid.Parse(partitionId),
        };

        await this.mediator.Send(request, cancellationToken);
    }

    private async Task ProcessMessageAsync(string consumerGroupId, string partitionKey, CancellationToken cancellationToken)
    {
        using (Activity.Current?.Source.StartActivity())
        {
            using var consumer = await this.consumerFactory.CreateConsumerAsync(consumerGroupId, cancellationToken);

            while (cancellationToken.IsCancellationRequested is false)
                try
                {
                    var message = consumer.Consume(cancellationToken);

                    if (message.IsPartitionEOF)
                    {
                        continue;
                    }

                    if (partitionKey.Equals(message.Message.Key, StringComparison.OrdinalIgnoreCase))
                    {
                        await this.ConsumeMessageAsync(message.Message.Value, message.Message.Key, cancellationToken);
                    }

                    consumer.StoreOffset(message);
                }
                catch (Exception)
                {
                    consumer.Close();

                    throw;
                }
        }
    }

    private void TryRemoveTask(string taskId)
    {
        if (this.tasks.TryGetValue(taskId, out var taskInfo))
        {
            taskInfo.cancellationToken.Cancel(false);
            taskInfo.cancellationToken.Dispose();

            this.tasks.TryRemove(taskId, out _);
        }
    }
}
