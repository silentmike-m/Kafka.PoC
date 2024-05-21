namespace Kafka.Poc.Client.Cars.Cars.Interfaces;

using global::Kafka.Poc.Client.Cars.Cars.Models;

internal interface ICarRepository
{
    Task AddAsync(CarEntity car, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<CarEntity>> GetAllAsync(CancellationToken cancellationToken = default);
}
