namespace Kafka.Poc.Client.Customers.Controllers;

using Kafka.Poc.Client.Customers.Customers.Queries;
using Kafka.Poc.Client.Customers.Customers.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("[controller]/[action]")]
public sealed class CustomersController : ApiControllerBase
{
    [HttpGet(Name = "GetCustomers")]
    public async Task<IReadOnlyList<Customer>> GetCustomers(CancellationToken cancellationToken = default)
        => await this.Mediator.Send(new GetCustomers(), cancellationToken);
}
