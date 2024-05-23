namespace Kafka.Poc.Server.Customers.CommandHandlers;

using global::Kafka.Poc.Server.Customers.Commands;
using global::Kafka.Poc.Server.Kafka.Interfaces;
using global::Kafka.Poc.Server.Kafka.Models;
using MediatR;

internal sealed class AddCustomerHandler : IRequestHandler<AddCustomer>
{
    private readonly ILogger<AddCustomerHandler> logger;
    private readonly IKafkaProducerService producerService;

    public AddCustomerHandler(ILogger<AddCustomerHandler> logger, IKafkaProducerService producerService)
    {
        this.logger = logger;
        this.producerService = producerService;
    }

    public async Task Handle(AddCustomer request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to add car");

        var message = new CustomerMessage
        {
            FirstName = request.FirstName,
            Id = request.Id,
            LastName = request.LastName,
        };

        await this.producerService.PublishAsync(message, request.RepositoryId.ToString(), cancellationToken);
    }
}
