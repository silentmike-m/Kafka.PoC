namespace Kafka.Poc.Client.Cars.Kafka.Services;

using System.Text.Json;
using Confluent.Kafka;
using global::Kafka.Poc.Client.Cars.Cars.Commands;
using global::Kafka.Poc.Client.Cars.Kafka.Extensions;
using global::Kafka.Poc.Client.Cars.Kafka.Interfaces;
using global::Kafka.Poc.Client.Cars.Kafka.Models;
using global::Kafka.PoC.Shared.Constants;
using MediatR;

internal sealed class CarClientService : BackgroundService
{
    private readonly IKafkaAdminService kafkaAdminService;
    private readonly KafkaOptions kafkaOptions;
    private readonly ISender mediator;

    public CarClientService(IKafkaAdminService kafkaAdminService, KafkaOptions kafkaOptions, ISender mediator)
    {
        this.kafkaAdminService = kafkaAdminService;
        this.kafkaOptions = kafkaOptions;
        this.mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await this.kafkaAdminService.CreateTopicAsync(TopicNames.CAR_TOPIC_NAME);

        var consumerConfig = CreateConsumerConfig(this.kafkaOptions);

        using var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();

        consumer.Subscribe(TopicNames.CAR_TOPIC_NAME);

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

                if (carMessage is null)
                {
                    continue;
                }

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
        catch (Exception)
        {
            consumer.Close();

            throw;
        }
    }

    private static ConsumerConfig CreateConsumerConfig(KafkaOptions options)
    {
        var config = new ConsumerConfig
        {
            EnableAutoCommit = true,
            EnableAutoOffsetStore = false,
            GroupId = "1",
        };

        config.FillConfig(options);

        return config;
    }
}
