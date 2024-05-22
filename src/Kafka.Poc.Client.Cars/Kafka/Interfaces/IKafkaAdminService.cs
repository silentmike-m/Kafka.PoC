namespace Kafka.Poc.Client.Cars.Kafka.Interfaces;

internal interface IKafkaAdminService
{
    Task CreateTopicAsync(string topicName);
}
