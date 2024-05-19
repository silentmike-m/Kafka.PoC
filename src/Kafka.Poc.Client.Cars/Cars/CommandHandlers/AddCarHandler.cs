namespace Kafka.Poc.Client.Cars.Cars.CommandHandlers;

using Kafka.Poc.Client.Cars.Cars.Commands;
using Kafka.Poc.Client.Cars.Cars.Interfaces;
using Kafka.Poc.Client.Cars.Cars.Models;
using MediatR;

internal sealed class AddCarHandler : IRequestHandler<AddCar>
{
    private readonly ICarRepository carRepository;
    private readonly ILogger<AddCarHandler> logger;

    public AddCarHandler(ICarRepository carRepository, ILogger<AddCarHandler> logger)
    {
        this.carRepository = carRepository;
        this.logger = logger;
    }

    public async Task Handle(AddCar request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to add car with id {CarId}", request.Id);

        var car = new CarEntity
        {
            EngineSize = request.EngineSize,
            Id = request.Id,
            Manufacturer = request.Manufacturer,
            Model = request.Model,
        };

        await this.carRepository.AddAsync(car, cancellationToken);
    }
}
