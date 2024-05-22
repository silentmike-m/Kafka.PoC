namespace Kafka.Poc.Client.Customers.Customers.Models;

internal sealed record CustomerRepositoryEntity
{
    public Guid ConsumerGroupId { get; set; } = Guid.NewGuid();
    public Dictionary<Guid, CustomerEntity> Customers { get; init; } = new();
    public required Guid Id { get; init; }
}
