namespace Kafka.Poc.Client.Customers.Customers.CommandHandlers;

using global::Kafka.Poc.Client.Customers.Customers.Commands;
using global::Kafka.Poc.Client.Customers.Customers.Interfaces;
using global::Kafka.Poc.Client.Customers.Kafka.Interfaces;
using MediatR;

internal sealed class UnSubscribeRepositoryHandler : IRequestHandler<UnSubscribeRepository>
{
    private readonly ICustomerClientService customerClientService;
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<UnSubscribeRepositoryHandler> logger;

    public UnSubscribeRepositoryHandler(ICustomerClientService customerClientService, ICustomerRepository customerRepository, ILogger<UnSubscribeRepositoryHandler> logger)
    {
        this.customerClientService = customerClientService;
        this.customerRepository = customerRepository;
        this.logger = logger;
    }

    public async Task Handle(UnSubscribeRepository request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to unsubscribe repository with id '{RepositoryId}'", request.Id);

        var repository = await this.customerRepository.GetRepositoryAsync(request.Id, cancellationToken);

        if (repository is not null)
        {
            await this.customerClientService.UnSubscribeAsync(request.Id.ToString(), cancellationToken);
        }
    }
}
