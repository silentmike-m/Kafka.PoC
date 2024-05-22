namespace Kafka.Poc.Client.Customers.Controllers;

using Kafka.Poc.Client.Customers.Customers.Commands;
using Kafka.Poc.Client.Customers.Customers.Queries;
using Kafka.Poc.Client.Customers.Customers.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("[controller]/[action]")]
public sealed class CustomersController : ApiControllerBase
{
    [HttpGet(Name = "GetCustomerRepositories")]
    public async Task<IReadOnlyList<Guid>> GetCustomerRepositories(CancellationToken cancellationToken = default)
        => await this.Mediator.Send(new GetCustomerRepositories(), cancellationToken);

    [HttpGet(Name = "GetCustomers")]
    public async Task<IReadOnlyList<Customer>> GetCustomers([FromBody] GetCustomers request, CancellationToken cancellationToken = default)
        => await this.Mediator.Send(request, cancellationToken);

    [HttpPost(Name = "SubscribeRepository")]
    public async Task<IActionResult> SubscribeRepository([FromBody] SubscribeRepository request, CancellationToken cancellationToken = default)
    {
        await this.Mediator.Send(request, cancellationToken);

        return this.Ok();
    }
}
