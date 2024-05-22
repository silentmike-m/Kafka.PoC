namespace Kafka.Poc.Client.Customers.Customers.Queries;

using global::Kafka.Poc.Client.Customers.Customers.ViewModels;
using MediatR;

public sealed record GetCustomers : IRequest<IReadOnlyList<Customer>>
{
    public required Guid RepositoryId { get; init; }
}
