﻿namespace Kafka.Poc.Client.Cars.Kafka.Services;

using Confluent.Kafka;
using Confluent.Kafka.Admin;
using global::Kafka.Poc.Client.Cars.Kafka.Extensions;
using global::Kafka.Poc.Client.Cars.Kafka.Interfaces;

internal sealed class KafkaAdminService : IKafkaAdminService
{
    private readonly KafkaOptions kafkaOptions;

    public KafkaAdminService(KafkaOptions kafkaOptions)
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
