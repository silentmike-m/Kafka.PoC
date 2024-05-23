namespace Kafka.Poc.Client.Customers.Kafka.Interfaces;

internal interface IKafkaAdminService
{
    Task CreateTopicAsync(string topicName);
}
