using System;

namespace Music.Shared
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Artist { get; set; }
    }
}
