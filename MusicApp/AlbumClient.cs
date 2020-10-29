using Music.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MusicApp
{
    public class AlbumClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public AlbumClient(HttpClient httpClient)
            => _httpClient = httpClient;

        public async Task<Album[]> GetAlbumsAsync()
        {
            var response = await _httpClient.GetAsync("/api/Album");
            var stream = await response.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<Album[]>(stream, serializerOptions);
        }
    }
}
