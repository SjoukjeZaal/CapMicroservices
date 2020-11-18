using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Music.Shared;

namespace OrdersService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ILogger<OrdersController> _logger;
        private readonly DaprClient _daprClient;

        public OrdersController(ILogger<OrdersController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [Topic("musicstore-servicebus", "order_albums")]
        [HttpPost]
        public async Task<IActionResult> CreateOrder(IEnumerable<Album> albums)
        {
            // TODO: We could move this to an inventory/catalog microservice?
            // TODO: Move statestore name (+ key to configuration and use IOptions<T>)
            // get orders from state to determine the next id
            var orders = await _daprClient.GetStateAsync<List<Order>>("music-store", "orders");

            if (orders == null)
            {
                orders = new List<Order>();
            }

            var newOrder = new Order { Albums = albums };
            orders.Add(newOrder);

            // Log order processed message to simulate processing
            _logger.LogInformation($"Processing order {newOrder.Id}...");

            // save orders back into state
            await _daprClient.SaveStateAsync("music-store", "orders", orders);

            Thread.Sleep(1000);

            // Log order processed message to simulate processing
            _logger.LogInformation($"Order {newOrder.Id} processed, notifying anyone who is listening out there...");

            // Send message to topic to notify anyone else about the order successfully being processed
            await _daprClient.PublishEventAsync("musicstore-servicebus", "order_processed", newOrder);

            return Ok();
        }
    }
}
