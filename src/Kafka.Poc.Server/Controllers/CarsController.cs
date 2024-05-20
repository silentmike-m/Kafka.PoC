namespace Kafka.Poc.Server.Controllers;

using global::Kafka.Poc.Server.Cars.Commands;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("[controller]/[action]")]
public sealed class CarsController : ApiControllerBase
{
    [HttpPost(Name = "AddCar")]
    public async Task<IActionResult> AddCar([FromBody] AddCar request, CancellationToken cancellationToken = default)
    {
        await this.Mediator.Send(request, cancellationToken);

        return this.Accepted();
    }
}
