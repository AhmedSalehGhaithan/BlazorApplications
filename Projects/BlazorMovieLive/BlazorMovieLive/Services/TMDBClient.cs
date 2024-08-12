using BlazorMovieLive.Models;
using System.Net.Http.Json;

namespace BlazorMovieLive.Services
{
    public class TMDBClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TMDBClient(HttpClient httpClient,IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri("https://api.themoviedb.org/3/");

            _httpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));

            string apiKey = _configuration["TMDBKey"] ?? throw new Exception("TMDBKey not found!");

            _httpClient.DefaultRequestHeaders.Authorization = new("Bearer", apiKey);
        }

        public Task<PopularMoviePagedResponse?> GetPopularMoviesAsync(/*int page = 1*/)
        {
            //if (page < 1) page = 1;
            //if (page > 500) page = 500;

            return _httpClient.GetFromJsonAsync<PopularMoviePagedResponse>($"movie/popular");
        }


    }
}
