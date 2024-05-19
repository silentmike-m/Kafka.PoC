namespace Kafka.Poc.Client.Customers.Customers.CommandHandlers;

using Kafka.Poc.Client.Customers.Customers.Commands;
using Kafka.Poc.Client.Customers.Customers.Interfaces;
using Kafka.Poc.Client.Customers.Customers.Models;
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

        var customer = new CustomerEntity
        {
            FirstName = request.FirstName,
            Id = request.Id,
            LastName = request.LastName,
        };

        await this.customerRepository.AddAsync(customer, cancellationToken);
    }
}
