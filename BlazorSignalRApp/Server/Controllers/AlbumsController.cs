using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using BlazorSignalRApp.Server.Hubs;
using BlazorSignalRApp.Shared;
using Dapr.Client;
using Dapr.Client.Http;
using Dapr;

namespace BlazorSignalRApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly ILogger<AlbumsController> logger;
        private readonly IHubContext<MusicStoreHub> hubContext;
        private readonly DaprClient daprClient;

        public AlbumsController(
            ILogger<AlbumsController> logger,
            DaprClient daprClient,
            IHubContext<MusicStoreHub> hubContext)
        {
            this.daprClient = daprClient;
            this.hubContext = hubContext;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbums()
        {
            var albums = await daprClient.InvokeMethodAsync<IEnumerable<Album>>("musicapi", "albums", new HTTPExtension { Verb = HTTPVerb.Get });
            return Ok(albums);
        }

        [HttpPost]
        public async Task<IActionResult> Order(Album album)
        {
            logger.LogInformation("Ordering album now");
            await daprClient.PublishEventAsync("musicstore-servicebus", "order_albums", new List<Album> { album });
            return Ok();
        }

        [Topic("musicstore-servicebus", "order_processed")]
        [HttpPost("notifyOrderProcessed")]
        public async Task<IActionResult> NotifyOrderProcessed(Order order)
        {
            logger.LogInformation($"Order {order.Id} processing confirmation received, notifying client");
            await hubContext.Clients.All.SendAsync("OrderProcessed", order.Albums);
            return Ok();
        }
    }
}
