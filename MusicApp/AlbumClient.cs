using Dapr.Client;
using Dapr.Client.Http;
using Microsoft.Extensions.Logging;
using Music.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusicApp
{
    public class AlbumClient
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<AlbumClient> logger;
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public AlbumClient(DaprClient daprClient, ILogger<AlbumClient> logger)
        {
            _daprClient = daprClient;
            this.logger = logger;
        }

        public async Task<Album[]> GetAlbumsAsync() => await _daprClient.InvokeMethodAsync<Album[]>("musicapi", "albums", new HTTPExtension { Verb = HTTPVerb.Get });

    }
}
