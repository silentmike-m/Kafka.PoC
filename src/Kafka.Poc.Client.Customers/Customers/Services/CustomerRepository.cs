namespace Kafka.Poc.Client.Customers.Customers.Services;

using Kafka.Poc.Client.Customers.Customers.Interfaces;
using Kafka.Poc.Client.Customers.Customers.Models;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly Dictionary<Guid, CustomerEntity> customers = new();

    public async Task AddAsync(CustomerEntity customer, CancellationToken cancellationToken = default)
    {
        this.customers[customer.Id] = customer;

        await Task.CompletedTask;
    }

    public async Task<IReadOnlyList<CustomerEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Task.FromResult<IReadOnlyList<CustomerEntity>>(this.customers.Values.ToList());
}
