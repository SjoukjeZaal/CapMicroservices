using Music.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicAPI
{
    public interface IAlbumService
    {
        public IEnumerable<Album> GetAlbums();

        Task<IEnumerable<Album>> GetAlbumsFromStateAsync();
    }
}
