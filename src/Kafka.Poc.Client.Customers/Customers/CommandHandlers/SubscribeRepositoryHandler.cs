namespace Kafka.Poc.Client.Customers.Customers.CommandHandlers;

using global::Kafka.Poc.Client.Customers.Customers.Commands;
using global::Kafka.Poc.Client.Customers.Customers.Interfaces;
using global::Kafka.Poc.Client.Customers.Customers.Models;
using global::Kafka.Poc.Client.Customers.Kafka.Interfaces;
using MediatR;

internal sealed class SubscribeRepositoryHandler : IRequestHandler<SubscribeRepository>
{
    private readonly ICustomerClientService customerClientService;
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<SubscribeRepositoryHandler> logger;

    public SubscribeRepositoryHandler(ICustomerClientService customerClientService, ICustomerRepository customerRepository, ILogger<SubscribeRepositoryHandler> logger)
    {
        this.customerClientService = customerClientService;
        this.customerRepository = customerRepository;
        this.logger = logger;
    }

    public async Task Handle(SubscribeRepository request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to subscribe repository with id '{RepositoryId}' with past events '{FromPast}'", request.Id, request.FromPast);

        var repository = await this.customerRepository.GetRepositoryAsync(request.Id, cancellationToken);

        repository ??= new CustomerRepositoryEntity
        {
            Id = request.Id,
        };

        if (request.FromPast is false)
        {
            repository.ConsumerGroupId = Guid.NewGuid();
        }

        await this.customerClientService.SubscribeAsync(repository.ConsumerGroupId.ToString(), repository.Id.ToString(), cancellationToken);

        await this.customerRepository.UpsertAsync(repository, cancellationToken);
    }
}
