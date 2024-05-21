namespace Kafka.Poc.Client.Cars.Kafka;

using global::Kafka.Poc.Client.Cars.Kafka.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(KafkaOptions)).Get<KafkaOptions>();
        options ??= new KafkaOptions();

        services.AddSingleton(options);

        services.AddHostedService<CarClientService>();

        return services;
    }
}
