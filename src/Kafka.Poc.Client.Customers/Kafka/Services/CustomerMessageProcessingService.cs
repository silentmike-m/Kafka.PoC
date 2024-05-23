namespace Kafka.Poc.Client.Customers.Kafka.Services;

using System.Text.Json;
using global::Kafka.Poc.Client.Customers.Customers.Commands;
using global::Kafka.Poc.Client.Customers.Kafka.Interfaces;
using global::Kafka.Poc.Client.Customers.Kafka.Models;
using global::Kafka.PoC.Shared.Constants;
using MediatR;

internal sealed class CustomerMessageProcessingService : ICustomerMessageProcessingService
{
    private readonly IKafkaConsumerFactory consumerFactory;
    private readonly ISender mediator;

    public CustomerMessageProcessingService(IKafkaConsumerFactory consumerFactory, ISender mediator)
    {
        this.consumerFactory = consumerFactory;
        this.mediator = mediator;
    }

    public async Task ProcessMessage(string consumerGroupId, string repositoryId, CancellationToken cancellationToken)
    {
        using var consumer = await this.consumerFactory.CreateConsumerAsync(TopicNames.CUSTOMER_TOPIC_NAME, consumerGroupId, cancellationToken);

        while (cancellationToken.IsCancellationRequested is false)
            try
            {
                var message = consumer.Consume(cancellationToken);

                if (message.IsPartitionEOF)
                {
                    continue;
                }

                if (repositoryId.Equals(message.Message.Key, StringComparison.OrdinalIgnoreCase))
                {
                    await this.ConsumeMessageAsync(message.Message.Value, repositoryId, cancellationToken);
                }

                consumer.StoreOffset(message);
            }
            catch (Exception)
            {
                consumer.Close();

                throw;
            }
    }

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
}
