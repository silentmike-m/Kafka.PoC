namespace Kafka.Poc.Server.Kafka.Models;

using global::Kafka.PoC.Shared.Interfaces;

internal sealed record CustomerMessage : ICustomerMessage
{
    public required string FirstName { get; init; }
    public required Guid Id { get; init; }
    public required string LastName { get; init; }
}
