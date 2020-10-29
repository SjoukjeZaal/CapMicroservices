using Music.Shared;
using System.Collections.Generic;

namespace MusicAPI
{
    public interface IAlbumService
    {
        public IEnumerable<Album> GetAlbums();
    }
}
