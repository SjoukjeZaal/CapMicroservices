using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Music.Shared;

namespace MusicApp.Pages
{
    public class AlbumsModel : PageModel
    {
        public Album[]? Albums { get; set; }

        public async Task OnGetAsync([FromServices] AlbumClient albumClient)
        {
            Albums = await albumClient.GetAlbumsAsync();
        }
    }
}
