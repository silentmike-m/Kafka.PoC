namespace Kafka.Poc.Server.Cars.CommandHandlers;

using global::Kafka.Poc.Server.Cars.Commands;
using global::Kafka.Poc.Server.Kafka.Interfaces;
using global::Kafka.Poc.Server.Kafka.Models;
using MediatR;

internal sealed class AddCarHandler : IRequestHandler<AddCar>
{
    private readonly ILogger<AddCarHandler> logger;
    private readonly IKafkaProducerService producerService;

    public AddCarHandler(ILogger<AddCarHandler> logger, IKafkaProducerService producerService)
    {
        this.logger = logger;
        this.producerService = producerService;
    }

    public async Task Handle(AddCar request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to add car");

        var message = new CarMessage
        {
            EngineSize = request.EngineSize,
            Id = request.Id,
            Manufacturer = request.Manufacturer,
            Model = request.Model,
        };

        await this.producerService.PublishAsync(message, cancellationToken);
    }
}
