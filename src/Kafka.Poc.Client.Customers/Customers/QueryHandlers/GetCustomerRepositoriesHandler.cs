namespace Kafka.Poc.Client.Customers.Customers.QueryHandlers;

using global::Kafka.Poc.Client.Customers.Customers.Interfaces;
using global::Kafka.Poc.Client.Customers.Customers.Queries;
using MediatR;

internal sealed class GetCustomerRepositoriesHandler : IRequestHandler<GetCustomerRepositories, IReadOnlyList<Guid>>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<GetCustomerRepositoriesHandler> logger;

    public GetCustomerRepositoriesHandler(ICustomerRepository customerRepository, ILogger<GetCustomerRepositoriesHandler> logger)
    {
        this.customerRepository = customerRepository;
        this.logger = logger;
    }

    public async Task<IReadOnlyList<Guid>> Handle(GetCustomerRepositories request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get all customer repositories");

        return await this.customerRepository.GetRepositoriesAsync(cancellationToken);
    }
}
