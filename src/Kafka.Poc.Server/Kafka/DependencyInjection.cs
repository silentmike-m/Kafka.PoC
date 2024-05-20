namespace Kafka.Poc.Server.Kafka;

using global::Kafka.Poc.Server.Kafka.Interfaces;
using global::Kafka.Poc.Server.Kafka.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(KafkaOptions)).Get<KafkaOptions>();
        options ??= new KafkaOptions();

        services.AddSingleton(options);

        services.AddScoped<IKafkaProducerService, KafkaProducerService>();

        return services;
    }
}
