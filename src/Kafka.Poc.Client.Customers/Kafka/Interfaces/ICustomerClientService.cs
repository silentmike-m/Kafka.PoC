namespace Kafka.Poc.Client.Customers.Kafka.Interfaces;

internal interface ICustomerClientService
{
    Task SubscribeAsync(string consumerGroupId, string repositoryId, CancellationToken cancellationToken);
    Task UnSubscribeAsync(string repositoryId, CancellationToken cancellationToken);
}
