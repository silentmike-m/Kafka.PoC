namespace Kafka.Poc.Client.Cars.Cars.ViewModels;

public sealed record Car
{
    public required decimal EngineSize { get; init; }
    public required Guid Id { get; init; }
    public required string Manufacturer { get; init; }
    public required string Model { get; init; }
}
