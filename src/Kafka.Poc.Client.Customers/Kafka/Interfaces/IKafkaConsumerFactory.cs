namespace Kafka.Poc.Client.Customers.Kafka.Interfaces;

using Confluent.Kafka;

internal interface IKafkaConsumerFactory
{
    public Task<IConsumer<string, string>> CreateConsumerAsync(string consumerGroupId, CancellationToken cancellationToken = default);
}
