namespace Kafka.Poc.Client.Customers.Customers.Services;

using global::Kafka.Poc.Client.Customers.Customers.Interfaces;
using global::Kafka.Poc.Client.Customers.Customers.Models;

internal sealed class CustomerRepository : ICustomerRepository
{
    private readonly Dictionary<Guid, CustomerRepositoryEntity> customerRepositories = new();

    public async Task<IReadOnlyList<Guid>> GetRepositoriesAsync(CancellationToken cancellationToken = default)
    {
        var result = this.customerRepositories.Keys.ToList();

        return await Task.FromResult(result);
    }

    public async Task<CustomerRepositoryEntity?> GetRepositoryAsync(Guid id, CancellationToken cancellationToken = default)
    {
        if (this.customerRepositories.TryGetValue(id, out var repository))
        {
            return await Task.FromResult(repository);
        }

        return await Task.FromResult<CustomerRepositoryEntity?>(null);
    }

    public async Task UpsertAsync(CustomerRepositoryEntity repository, CancellationToken cancellationToken = default)
    {
        this.customerRepositories[repository.Id] = repository;

        await Task.CompletedTask;
    }

    public async Task<IReadOnlyList<CustomerEntity>> GetAllAsync(Guid repositoryId, CancellationToken cancellationToken = default)
    {
        var result = new List<CustomerEntity>();

        if (this.customerRepositories.TryGetValue(repositoryId, out var repository))
        {
            result = repository.Customers.Values.ToList();
        }

        return await Task.FromResult(result);
    }
}
