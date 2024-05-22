namespace Kafka.Poc.Client.Customers.Customers.Queries;

using MediatR;

public sealed record GetCustomerRepositories : IRequest<IReadOnlyList<Guid>>;
