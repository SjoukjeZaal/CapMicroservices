namespace MusicStore.Web.Shared
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Artist { get; set; }
        public bool ProcessingOrder { get; set; } = false;
    }
}
