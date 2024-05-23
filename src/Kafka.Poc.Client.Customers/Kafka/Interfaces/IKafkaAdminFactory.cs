namespace Kafka.Poc.Client.Customers.Kafka.Interfaces;

internal interface IKafkaAdminFactory
{
    Task CreateTopicAsync(string topicName);
}
