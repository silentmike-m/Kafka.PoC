namespace Kafka.Poc.Client.Customers.Kafka.Interfaces;

internal interface ICustomerClientService
{
    Task SubscribeAsync(Guid consumerGroupId, Guid repositoryId, CancellationToken cancellationToken);
    Task UnSubscribeAsync(Guid repositoryId, CancellationToken cancellationToken);
}
