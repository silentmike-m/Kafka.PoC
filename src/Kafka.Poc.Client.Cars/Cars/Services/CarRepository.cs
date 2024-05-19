namespace Kafka.Poc.Client.Cars.Cars.Services;

using Kafka.Poc.Client.Cars.Cars.Interfaces;
using Kafka.Poc.Client.Cars.Cars.Models;

internal sealed class CarRepository : ICarRepository
{
    private readonly Dictionary<Guid, CarEntity> cars = new();

    public async Task AddAsync(CarEntity car, CancellationToken cancellationToken = default)
    {
        this.cars[car.Id] = car;

        await Task.CompletedTask;
    }

    public async Task<IReadOnlyList<CarEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await Task.FromResult<IReadOnlyList<CarEntity>>(this.cars.Values.ToList());
}
