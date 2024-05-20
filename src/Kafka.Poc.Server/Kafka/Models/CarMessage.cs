namespace Kafka.Poc.Server.Kafka.Models;

using global::Kafka.PoC.Shared.Interfaces;

internal sealed record CarMessage : ICarMessage
{
    public required decimal EngineSize { get; init; }
    public required Guid Id { get; init; }
    public required string Manufacturer { get; init; }
    public required string Model { get; init; }
}
