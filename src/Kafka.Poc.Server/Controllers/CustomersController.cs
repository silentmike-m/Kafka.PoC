namespace Kafka.Poc.Server.Controllers;

using global::Kafka.Poc.Server.Customers.Commands;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("[controller]/[action]")]
public sealed class CustomersController : ApiControllerBase
{
    [HttpPost(Name = "AddCustomer")]
    public async Task<IActionResult> AddCustomer([FromBody] AddCustomer request, CancellationToken cancellationToken = default)
    {
        await this.Mediator.Send(request, cancellationToken);

        return this.Accepted();
    }
}
