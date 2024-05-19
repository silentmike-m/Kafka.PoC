namespace Kafka.Poc.Client.Customers.Customers.QueryHandlers;

using Kafka.Poc.Client.Customers.Customers.Interfaces;
using Kafka.Poc.Client.Customers.Customers.Queries;
using Kafka.Poc.Client.Customers.Customers.ViewModels;
using MediatR;

internal sealed class GetCustomersHandler : IRequestHandler<GetCustomers, IReadOnlyList<Customer>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<GetCustomersHandler> logger;

    public GetCustomersHandler(ICustomerRepository customerRepository, ILogger<GetCustomersHandler> logger)
    {
        this.customerRepository = customerRepository;
        this.logger = logger;
    }

    public async Task<IReadOnlyList<Customer>> Handle(GetCustomers request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get all customers");

        var customers = await this.customerRepository.GetAllAsync(cancellationToken);

        var result = new List<Customer>();

        foreach (var customer in customers)
        {
            var customerResult = new Customer
            {
                FirstName = customer.FirstName,
                Id = customer.Id,
                LastName = customer.LastName,
            };

            result.Add(customerResult);
        }

        return result;
    }
}
