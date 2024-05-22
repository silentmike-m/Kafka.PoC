namespace Kafka.Poc.Client.Customers.Customers.Commands;

using MediatR;

public sealed record UnSubscribeRepository : IRequest
{
    public required Guid Id { get; init; }
}
