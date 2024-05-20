namespace Kafka.Poc.Server.Cars.Commands;

using MediatR;

public sealed record AddCar : IRequest
{
    public required decimal EngineSize { get; init; }
    public required Guid Id { get; init; }
    public required string Manufacturer { get; init; }
    public required string Model { get; init; }
}
