using BloggBlazorServer.Models;
using Microsoft.AspNetCore.Identity;

namespace BloggBlazorServer.Services
{
    public class SearchService
    {
        private readonly HttpClient _httpClient;

        public SearchService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SearchResult> SearchAsync(string query)
        {
            var response = await _httpClient.GetFromJsonAsync<SearchResult>($"api/Search?query={query}");
            return response;
        }
    }

    public class SearchResult
    {
        public List<Post> Poster { get; set; }
        public List<Blogg> Blogger { get; set; }
        public List<Kommentar> Kommentarer { get; set; }
        public List<IdentityUser> Users { get; set; }
    }
}
