using System.Data;
using System.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using MusicStore.Web.Server.Hubs;
using MusicStore.Shared;
using Dapr.Client;
using Dapr.Client.Http;

namespace MusicStore.Web.Server.Controllers
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
        public async Task<IEnumerable<Album>> GetAlbums() => await daprClient.InvokeMethodAsync<Album[]>("musicapi", "albums", new HTTPExtension { Verb = HTTPVerb.Get });

        // [Topic("pubsub", "message")]
        // [HttpPost("message")]
        // public async Task Message(Message message)
        // {
        //     Console.WriteLine("Message received");
        //     Console.WriteLine($"I got a message from {message.Name}");
        //     Console.WriteLine($"Saying: {message.Content}");
        //     Console.WriteLine("Sending message to all subscribers");
        //     await hubContext.Clients.All.SendAsync("ReceiveMessage", message.Name, message.Content);
        // }
    }
}
