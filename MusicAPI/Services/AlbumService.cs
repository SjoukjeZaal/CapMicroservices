using Dapr.Client;
using Microsoft.Extensions.Logging;
using Music.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicAPI
{
    public class AlbumService : IAlbumService
    {
        private readonly ILogger _logger;
        private readonly DaprClient _daprClient;

        public AlbumService(ILogger<AlbumService> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        private List<Album> _albums = new List<Album>()
        {
            new Album { AlbumId = 1, Title = "Thinking Out Loud", Artist = "Ed Sheeran"},
            new Album { AlbumId = 2, Title = "Master of Puppets", Artist = "Metallica"},
            new Album { AlbumId = 3, Title = "The Holographic Principle", Artist = "Epica"},
            new Album { AlbumId = 4, Title = "Dangerously In Love", Artist = "Beyonce"},
            new Album { AlbumId = 5, Title = "Octavarium", Artist = "Dream Theater"}
        };

        public IEnumerable<Album> GetAlbums()
        {
            _logger.LogTrace("GetAlbums invoked");
            return _albums;
        }

        public async Task<IEnumerable<Album>> GetAlbumsFromStateAsync()
        {
            _logger.LogInformation("GetAlbumsFromState invoked");

            var albums = await _daprClient.GetStateAsync<IEnumerable<Album>>("music-store", "albums");

            if (albums == null)
            {
                _logger.LogInformation("No albums found. Seeding state");

                albums = _albums;

                await _daprClient.SaveStateAsync("music-store", "albums", albums);

                _logger.LogInformation("State seeded");
            }

            _logger.LogInformation($"Albums found in cache, here they come!");

            return albums;
        }
    }
}
