using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music.Shared;

namespace MusicApp.Pages
{
    public class AlbumsModel : PageModel
    {
        public IEnumerable<Album>? Albums { get; set; }

        public async Task OnGetAsync([FromServices] AlbumClient albumClient)
        {
            Albums = await albumClient.GetAlbumsAsync();
        }
    }
}
