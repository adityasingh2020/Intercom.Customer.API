using Intercom.Customer.Contract;
using Intercom.Customer.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace InterCom.Customer.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/v1")]
    public class CustomerController : ControllerBase
    {
        readonly ILogger<CustomerController> _logger;
        readonly ICustomerService _customerService;
        readonly AppSettings _settings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="settings"></param>
        /// <param name="customerService"></param>
        public CustomerController(ILogger<CustomerController> logger, IOptions<AppSettings> settings, ICustomerService customerService)
        {
            _logger = logger;
            _settings = settings.Value;
            _customerService = customerService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [HttpGet("GetNearbyCustomers")]
        [ProducesResponseType(typeof(List<Intercom.Customer.Contract.Customer>), (int)HttpStatusCode.OK)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<Intercom.Customer.Contract.Customer>> Get(double? distance, string filePath)
        {
            if (distance.HasValue && distance <= 0)
                return BadRequest();
            if (!distance.HasValue)
                distance = _settings.Distance;

            try
            {
                var res = await _customerService.GetNearByCustomers(distance.Value, filePath);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error while fetching customers", ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "Something went wrong!");
            }

        }
    }
}
