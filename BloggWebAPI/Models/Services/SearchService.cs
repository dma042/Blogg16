using BloggWebAPI.Models;

namespace BloggWebAPI.Services
{
    public class SearchService
    {
        private readonly HttpClient _httpClient;

        public SearchService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
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
    }
}