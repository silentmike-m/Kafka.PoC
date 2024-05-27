namespace Kafka.Poc.Server.Kafka.Services;

using System.Text.Json;
using Confluent.Kafka;
using global::Kafka.Poc.Server.Kafka.Interfaces;
using global::Kafka.PoC.Shared.Interfaces;

internal sealed class KafkaProducerService : IKafkaProducerService
{
    private readonly KafkaOptions kafkaOptions;
    private readonly ILogger<KafkaProducerService> logger;

    public KafkaProducerService(KafkaOptions kafkaOptions, ILogger<KafkaProducerService> logger)
    {
        this.kafkaOptions = kafkaOptions;
        this.logger = logger;
    }

    public async Task PublishAsync<TMessage>(TMessage message, CancellationToken cancellationToken = default)
        where TMessage : IMessage
        => await this.PublishAsync(message, string.Empty, cancellationToken);

    public async Task PublishAsync<TMessage>(TMessage message, string partitionId, CancellationToken cancellationToken = default)
        where TMessage : IMessage
    {
        this.logger.LogInformation("Try to publish message {MessageType} to topic {TopicName}", nameof(message), message.TopicName);

        var producerConfig = CreateProducerConfig(this.kafkaOptions);

        using var producer = new ProducerBuilder<string, string>(producerConfig).Build();
        var messageJson = JsonSerializer.Serialize(message);

        await producer.ProduceAsync(message.TopicName, new Message<string, string>
        {
            Key = partitionId,
            Value = messageJson,
        }, cancellationToken);
    }

    private static ProducerConfig CreateProducerConfig(KafkaOptions options)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = options.BootstrapServers,
            ClientId = options.ClientId,
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
}
