namespace Kafka.Poc.Client.Customers.Customers.CommandHandlers;

using global::Kafka.Poc.Client.Customers.Customers.Commands;
using global::Kafka.Poc.Client.Customers.Customers.Interfaces;
using global::Kafka.Poc.Client.Customers.Customers.Models;
using MediatR;

internal sealed class AddCustomerHandler : IRequestHandler<AddCustomer>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<AddCustomerHandler> logger;

    public AddCustomerHandler(ICustomerRepository customerRepository, ILogger<AddCustomerHandler> logger)
    {
        this.customerRepository = customerRepository;
        this.logger = logger;
    }

    public async Task Handle(AddCustomer request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to add customer with id {CustomerId}", request.Id);

        var repository = await this.customerRepository.GetRepositoryAsync(request.RepositoryId, cancellationToken);

        repository ??= new CustomerRepositoryEntity
        {
            Id = request.Id,
        };

        var customer = new CustomerEntity
        {
            FirstName = request.FirstName,
            Id = request.Id,
            LastName = request.LastName,
        };

        repository.Customers[request.Id] = customer;

        await this.customerRepository.UpsertAsync(repository, cancellationToken);
    }
}
