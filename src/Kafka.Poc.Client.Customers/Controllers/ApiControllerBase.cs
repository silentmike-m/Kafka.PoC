namespace Kafka.Poc.Client.Customers.Controllers;

using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController, Route("[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender mediator;
    protected ISender Mediator => this.mediator ??= this.HttpContext.RequestServices.GetService<ISender>();
}
