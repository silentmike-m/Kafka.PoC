namespace Kafka.Poc.Client.Customers.Kafka;

using global::Kafka.Poc.Client.Cars.Kafka;
using global::Kafka.Poc.Client.Customers.Kafka.Interfaces;
using global::Kafka.Poc.Client.Customers.Kafka.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetSection(nameof(KafkaOptions)).Get<KafkaOptions>();
        options ??= new KafkaOptions();

        services.AddSingleton(options);

        services.AddSingleton<IKafkaAdminService, KafkaAdminService>();

        services.AddSingleton<ICustomerClientService, CustomerClientService>();

        return services;
    }
}
