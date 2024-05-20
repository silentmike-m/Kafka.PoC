namespace Kafka.Poc.Server.Kafka;

using Confluent.Kafka;

internal sealed record KafkaOptions
{
    public string BootstrapServers { get; init; } = string.Empty;
    public string ClientId { get; init; } = string.Empty;
    public SaslMechanism? SaslMechanism { get; init; }
    public string? SaslPassword { get; init; }
    public string? SaslUserName { get; init; }
    public int StatisticsIntervalInMilliSeconds { get; init; } = 30000;
    public bool UseSaslSsl { get; init; }
}
