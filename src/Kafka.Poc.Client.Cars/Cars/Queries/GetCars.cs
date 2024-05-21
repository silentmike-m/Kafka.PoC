namespace Kafka.Poc.Client.Cars.Cars.Queries;

using global::Kafka.Poc.Client.Cars.Cars.ViewModels;
using MediatR;

public sealed record GetCars : IRequest<IReadOnlyList<Car>>;
