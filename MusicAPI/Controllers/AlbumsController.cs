using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MusicAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        public AlbumsController(IAlbumService albumService)
            => _albumService = albumService;

        [HttpGet]
        public async Task<IActionResult> GetAlbums() => Ok(await _albumService.GetAlbumsFromStateAsync());
    }
}
