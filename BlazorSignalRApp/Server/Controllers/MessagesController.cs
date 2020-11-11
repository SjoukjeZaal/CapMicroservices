using Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Dapr;
using BlazorSignalRApp.Server.Hubs;
using BlazorSignalRApp.Shared;

namespace BlazorSignalRApp.Server.Controllers
{
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly ILogger<MessagesController> logger;
        private readonly IHubContext<ChatHub> hubContext;

        public MessagesController(
            ILogger<MessagesController> logger,
            IHubContext<ChatHub> hubContext)
        {
            this.hubContext = hubContext;
            this.logger = logger;
        }

        [Topic("pubsub", "message")]
        [HttpPost("message")]
        public async Task Message(Message message)
        {
            Console.WriteLine("Message received");
            Console.WriteLine($"I got a message from {message.Name}");
            Console.WriteLine($"Saying: {message.Content}");
            Console.WriteLine("Sending message to all subscribers");
            await hubContext.Clients.All.SendAsync("ReceiveMessage", message.Name, message.Content);
        }
    }
}
