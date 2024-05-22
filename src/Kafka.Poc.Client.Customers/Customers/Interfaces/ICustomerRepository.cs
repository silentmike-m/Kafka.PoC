namespace Kafka.Poc.Client.Customers.Customers.Interfaces;

using Kafka.Poc.Client.Customers.Customers.Models;

internal interface ICustomerRepository
{
    Task<IReadOnlyList<Guid>> GetRepositoriesAsync(CancellationToken cancellationToken = default);
    Task<CustomerRepositoryEntity?> GetRepositoryAsync(Guid id, CancellationToken cancellationToken = default);
    Task UpsertAsync(CustomerRepositoryEntity repository, CancellationToken cancellationToken = default);
}
