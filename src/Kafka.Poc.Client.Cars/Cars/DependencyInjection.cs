namespace Kafka.Poc.Client.Cars.Cars;

using Kafka.Poc.Client.Cars.Cars.Interfaces;
using Kafka.Poc.Client.Cars.Cars.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddCars(this IServiceCollection services)
    {
        services.AddSingleton<ICarRepository, CarRepository>();

        return services;
    }
}
