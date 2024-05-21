namespace Kafka.Poc.Client.Cars.Kafka.Services;

using System.Text.Json;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using global::Kafka.Poc.Client.Cars.Cars.Commands;
using global::Kafka.Poc.Client.Cars.Kafka.Models;
using MediatR;

internal sealed class CarClientService : BackgroundService
{
    private readonly KafkaOptions kafkaOptions;
    private readonly ISender mediator;

    public CarClientService(KafkaOptions kafkaOptions, ISender mediator)
    {
        this.kafkaOptions = kafkaOptions;
        this.mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await CreateTopicAsync(this.kafkaOptions);

        var consumerConfig = CreateConsumerConfig(this.kafkaOptions);

        using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();

        consumer.Subscribe("CAR-TOPIC-1234");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = consumer.Consume(stoppingToken);

                if (message.IsPartitionEOF)
                {
                    continue;
                }

                var carMessage = JsonSerializer.Deserialize<CarMessage>(message.Message.Value);

                await this.mediator.Send(new AddCar
                {
                    EngineSize = carMessage.EngineSize,
                    Id = carMessage.Id,
                    Manufacturer = carMessage.Manufacturer,
                    Model = carMessage.Model,
                }, stoppingToken);

                consumer.StoreOffset(message);
                consumer.Commit();
            }
        }
        catch (Exception exception)
        {
            consumer.Close();

            throw;
        }
    }

    private static ConsumerConfig CreateConsumerConfig(KafkaOptions options)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = options.BootstrapServers,
            ClientId = options.ClientId,
            EnableAutoCommit = true,
            EnableAutoOffsetStore = false,
            GroupId = "1",
            SecurityProtocol = SecurityProtocol.Plaintext,
            StatisticsIntervalMs = options.StatisticsIntervalInMilliSeconds,
        };

        if (options.UseSaslSsl)
        {
            config.SaslMechanism = options.SaslMechanism;
            config.SaslPassword = options.SaslPassword;
            config.SaslUsername = options.SaslUserName;
            config.SecurityProtocol = SecurityProtocol.SaslSsl;
        }

        return config;
    }

    private static async Task CreateTopicAsync(KafkaOptions options)
    {
        var falsePositiveErrorCodes = new List<ErrorCode>
        {
            ErrorCode.NoError,
            ErrorCode.TopicAlreadyExists,
        };

        var adminConfig = new AdminClientConfig
        {
            BootstrapServers = options.BootstrapServers,
            ClientId = options.ClientId,
            SecurityProtocol = SecurityProtocol.Plaintext,
            StatisticsIntervalMs = options.StatisticsIntervalInMilliSeconds,
        };

        using var adminClient = new AdminClientBuilder(adminConfig).Build();

        try

        {
            await adminClient.CreateTopicsAsync(new[]
            {
                new TopicSpecification
                {
                    Name = "CAR-TOPIC",
                    NumPartitions = 1,
                },
            });
        }
        catch (CreateTopicsException createTopicsException)
        {
            var test = false;
            //ignore
        }
    }
}
