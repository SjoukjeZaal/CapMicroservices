using Microsoft.Extensions.Logging;
using Music.Shared;
using System.Collections.Generic;

namespace MusicAPI
{
    public class AlbumService : IAlbumService
    {
        private readonly ILogger _logger;

        public AlbumService(ILogger<AlbumService> logger)
            => _logger = logger;

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
    }
}
