using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace InterCom.Customer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheckController : Controller
    {
        [HttpGet("")]
        [SwaggerResponse((int)HttpStatusCode.OK)]
        public IActionResult HealthCheck() => Ok("InterCom.Customer.API is running!");
    }
}
