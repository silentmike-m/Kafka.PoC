namespace Kafka.Poc.Client.Customers.Customers.Interfaces;

using Kafka.Poc.Client.Customers.Customers.Models;

internal interface ICustomerRepository
{
    Task AddAsync(CustomerEntity customer, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CustomerEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}
