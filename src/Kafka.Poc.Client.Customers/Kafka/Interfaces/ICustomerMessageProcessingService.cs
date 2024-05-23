namespace Kafka.Poc.Client.Customers.Kafka.Interfaces;

internal interface ICustomerMessageProcessingService
{
    Task ProcessMessage(string consumerGroupId, string repositoryId, CancellationToken cancellationToken);
}
