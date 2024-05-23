namespace Kafka.Poc.Client.Customers.Kafka.Services;

using Confluent.Kafka;
using Confluent.Kafka.Admin;
using global::Kafka.Poc.Client.Cars.Kafka;
using global::Kafka.Poc.Client.Customers.Kafka.Extensions;
using global::Kafka.Poc.Client.Customers.Kafka.Interfaces;

internal sealed class KafkaAdminFactory : IKafkaAdminFactory
{
    private readonly KafkaOptions kafkaOptions;

    public KafkaAdminFactory(KafkaOptions kafkaOptions)
        => this.kafkaOptions = kafkaOptions;

    public async Task CreateTopicAsync(string topicName)
    {
        var falsePositiveErrorCodes = new List<ErrorCode>
        {
            ErrorCode.NoError,
            ErrorCode.TopicAlreadyExists,
        };

        var adminConfig = new AdminClientConfig();
        adminConfig.FillConfig(this.kafkaOptions);

        using var adminClient = new AdminClientBuilder(adminConfig).Build();

        try
        {
            await adminClient.CreateTopicsAsync(new[]
            {
                new TopicSpecification
                {
                    Name = topicName,
                    NumPartitions = 1,
                },
            });
        }
        catch (CreateTopicsException createTopicsException)
        {
            foreach (var result in createTopicsException.Results)
            {
                if (falsePositiveErrorCodes.Contains(result.Error))
                {
                    continue;
                }

                throw;
            }
        }
    }
}
