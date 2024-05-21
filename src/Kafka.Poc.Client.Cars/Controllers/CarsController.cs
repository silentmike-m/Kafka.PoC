namespace Kafka.Poc.Client.Cars.Controllers;

using global::Kafka.Poc.Client.Cars.Cars.Queries;
using global::Kafka.Poc.Client.Cars.Cars.ViewModels;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("[controller]/[action]")]
public sealed class CarsController : ApiControllerBase
{
    [HttpGet(Name = "GetCars")]
    public async Task<IReadOnlyList<Car>> GetCars(CancellationToken cancellationToken = default)
        => await this.Mediator.Send(new GetCars(), cancellationToken);
}
