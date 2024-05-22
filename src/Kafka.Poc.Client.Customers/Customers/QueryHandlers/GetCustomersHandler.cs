namespace Kafka.Poc.Client.Customers.Customers.QueryHandlers;

using global::Kafka.Poc.Client.Customers.Customers.Interfaces;
using global::Kafka.Poc.Client.Customers.Customers.Queries;
using global::Kafka.Poc.Client.Customers.Customers.ViewModels;
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

        var repository = await this.customerRepository.GetRepositoryAsync(request.RepositoryId, cancellationToken);

        var result = new List<Customer>();

        if (repository is not null)
        {
            foreach (var (_, customer) in repository.Customers)
            {
                var customerResult = new Customer
                {
                    FirstName = customer.FirstName,
                    Id = customer.Id,
                    LastName = customer.LastName,
                };

                result.Add(customerResult);
            }
        }

        return result;
    }
}
