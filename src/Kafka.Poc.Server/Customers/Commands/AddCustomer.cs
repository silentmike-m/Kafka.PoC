﻿namespace Kafka.Poc.Server.Customers.Commands;

using MediatR;

public sealed record AddCustomer : IRequest
{
    public required string FirstName { get; init; }
    public required Guid Id { get; init; }
    public required string LastName { get; init; }
    public required Guid RepositoryId { get; init; }
}
