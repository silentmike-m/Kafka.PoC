namespace Kafka.Poc.Client.Customers.Customers;

using Kafka.Poc.Client.Customers.Customers.Interfaces;
using Kafka.Poc.Client.Customers.Customers.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddCustomers(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
