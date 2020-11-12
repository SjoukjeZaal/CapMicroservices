using Dapr.Client;
using Dapr.Client.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Music.Shared;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Album>> GetAlbumsAsync() =>
            await _daprClient.InvokeMethodAsync<IEnumerable<Album>>("musicapi", "albums", new HTTPExtension { Verb = HTTPVerb.Get });
    }
}
