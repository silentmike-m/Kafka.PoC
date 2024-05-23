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

        services.AddSingleton<IKafkaAdminFactory, KafkaAdminFactory>();
        services.AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>();

        services.AddSingleton<ICustomerClientService, CustomerClientService>();
        services.AddTransient<ICustomerMessageProcessingService, CustomerMessageProcessingService>();

        return services;
    }
}
