using System.Security.AccessControl;
using Dapr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Music.Shared;
using System.Linq;

namespace CommsService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommunicationsController : ControllerBase
    {
        private readonly ILogger<CommunicationsController> _logger;

        public CommunicationsController(ILogger<CommunicationsController> logger)
        {
            _logger = logger;
        }
        
        [Topic("musicstore_servicebus", "order_processed")]
        [HttpPost("email")]
        public IActionResult SendEmail(Order order)
        {
            _logger.LogInformation($"Order {order.Id} with {order.Albums.Count()} was processed, this service will now send an e-mail to the customer");
            return Ok();
        }
    }
}
