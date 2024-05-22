namespace Kafka.Poc.Client.Customers.Customers.CommandHandlers;

using Kafka.Poc.Client.Customers.Customers.Commands;
using Kafka.Poc.Client.Customers.Customers.Interfaces;
using Kafka.Poc.Client.Customers.Customers.Models;
using MediatR;

internal sealed class SubscribeRepositoryHandler : IRequestHandler<SubscribeRepository>
{
    private readonly ICustomerRepository customerRepository;
    private readonly ILogger<SubscribeRepositoryHandler> logger;

    public SubscribeRepositoryHandler(ICustomerRepository customerRepository, ILogger<SubscribeRepositoryHandler> logger)
    {
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

        await this.customerRepository.UpsertAsync(repository, cancellationToken);
    }
}
