namespace Kafka.Poc.Client.Cars.Cars.Queries;

using Kafka.Poc.Client.Cars.Cars.ViewModels;
using MediatR;

public sealed record GetCars : IRequest<IReadOnlyList<Car>>;
