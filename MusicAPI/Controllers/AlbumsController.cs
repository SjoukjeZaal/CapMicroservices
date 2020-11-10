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
        public IActionResult GetAlbums()
            => Ok(_albumService.GetAlbums());
    }
}
