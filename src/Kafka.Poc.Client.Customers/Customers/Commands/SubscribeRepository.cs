namespace Kafka.Poc.Client.Customers.Customers.Commands;

using MediatR;

public sealed record SubscribeRepository : IRequest
{
    public required bool FromPast { get; init; }
    public required Guid Id { get; init; }
}
