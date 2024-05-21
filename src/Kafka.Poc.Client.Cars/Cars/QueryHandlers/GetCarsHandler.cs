namespace Kafka.Poc.Client.Cars.Cars.QueryHandlers;

using global::Kafka.Poc.Client.Cars.Cars.Interfaces;
using global::Kafka.Poc.Client.Cars.Cars.Queries;
using global::Kafka.Poc.Client.Cars.Cars.ViewModels;
using MediatR;

internal sealed class GetCarsHandler : IRequestHandler<GetCars, IReadOnlyList<Car>>
{
    private readonly ICarRepository carRepository;
    private readonly ILogger<GetCarsHandler> logger;

    public GetCarsHandler(ICarRepository carRepository, ILogger<GetCarsHandler> logger)
    {
        this.carRepository = carRepository;
        this.logger = logger;
    }

    public async Task<IReadOnlyList<Car>> Handle(GetCars request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get all cars");

        var cars = await this.carRepository.GetAllAsync(cancellationToken);

        var result = new List<Car>();

        foreach (var car in cars)
        {
            var carResult = new Car
            {
                EngineSize = car.EngineSize,
                Id = car.Id,
                Manufacturer = car.Manufacturer,
                Model = car.Model,
            };

            result.Add(carResult);
        }

        return result;
    }
}
