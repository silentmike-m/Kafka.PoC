namespace Kafka.Poc.Client.Customers.Controllers;

using global::Kafka.Poc.Client.Customers.Customers.Commands;
using global::Kafka.Poc.Client.Customers.Customers.Queries;
using global::Kafka.Poc.Client.Customers.Customers.ViewModels;
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

    [HttpPost(Name = "UnSubscribeRepository")]
    public async Task<IActionResult> UnSubscribeRepository([FromBody] UnSubscribeRepository request, CancellationToken cancellationToken = default)
    {
        await this.Mediator.Send(request, cancellationToken);

        return this.Ok();
    }
}
