namespace Kafka.Poc.Client.Customers.Customers.Models;

internal sealed record CustomerEntity
{
    public required string FirstName { get; init; }
    public required Guid Id { get; init; }
    public required string LastName { get; init; }
}
