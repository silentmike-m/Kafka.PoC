namespace Kafka.Poc.Client.Customers.Kafka.Services;

using Confluent.Kafka;
using global::Kafka.Poc.Client.Cars.Kafka;
using global::Kafka.Poc.Client.Customers.Kafka.Extensions;
using global::Kafka.Poc.Client.Customers.Kafka.Interfaces;

internal sealed class KafkaConsumerFactory : IKafkaConsumerFactory
{
    private readonly IKafkaAdminFactory adminFactory;
    private readonly KafkaOptions kafkaOptions;

    public KafkaConsumerFactory(IKafkaAdminFactory adminFactory, KafkaOptions kafkaOptions)
    {
        this.adminFactory = adminFactory;
        this.kafkaOptions = kafkaOptions;
    }

    public async Task<IConsumer<string, string>> CreateConsumerAsync(string consumerGroupId, string topicName, CancellationToken cancellationToken = default)
    {
        await this.adminFactory.CreateTopicAsync(topicName);

        var consumerConfig = CreateConsumerConfig(consumerGroupId, this.kafkaOptions);

        var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();

        consumer.Subscribe(topicName);

        return consumer;
    }

    private static ConsumerConfig CreateConsumerConfig(string consumerGroupId, KafkaOptions options)
    {
        var config = new ConsumerConfig
        {
            EnableAutoCommit = true,
            EnableAutoOffsetStore = false,
            GroupId = consumerGroupId,
        };

        config.FillConfig(options);

        return config;
    }
}
