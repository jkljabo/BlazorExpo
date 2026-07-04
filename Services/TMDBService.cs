using BlazorCodeChallenge.Components.Pages;
using BlazorCodeChallenge.Models;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorCodeChallenge.Services
{
    public class TMDBService
    {
        private readonly HttpClient _http;

        /// <summary>
        /// Configures JSON serialization to use snake_case naming for property names.
        /// </summary>
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
        };

        /// <summary>
        /// TMDBService
        /// </summary>
        /// <param name="http"></param>
        /// <param name="config"></param>
        public TMDBService(HttpClient http, IConfiguration config)
        {
            _http = http;
            string? tmdbKey = config["TMDBAccessKey"];

            if (!string.IsNullOrEmpty(tmdbKey))
            {
                _http.BaseAddress = new Uri("https://api.themoviedb.org/3/");
                _http.DefaultRequestHeaders.Authorization = new("Bearer", tmdbKey);
            }
            else
            {
                // When deployed to Netlify or the like, the TMDBAccessKey may not be available in the configuration. Handle this case with an edge function.
                _http.BaseAddress = new Uri(_http.BaseAddress + "tmdb/");
            }
        }

        /// <summary>
        /// GetMoviesAsync
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpIOException"></exception>
        public async Task<MovieListResponse> GetMoviesAsync()
        {
            // Set the TMDB API URL and Authorization header
            string url = "movie/now_playing?region=US&language=en-us";

            // Set the TMDB API Base URL for API images
            string imageBaseUrl = "https://image.tmdb.org/t/p/w500";

            MovieListResponse? response = await _http.GetFromJsonAsync<MovieListResponse>(url, _jsonOptions) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to load movies!");

            foreach (var movie in response.Results)
            {
                if (!string.IsNullOrEmpty(movie.PosterPath))
                {
                    movie.PosterPath = $"{imageBaseUrl}{movie.PosterPath}";
                }
                else
                {
                    movie.PosterPath = "images/MovieTime/poster.png";
                }
            }
            return response;
        }

        /// <summary>
        /// GetCoolMoviesAsync
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpIOException"></exception>
        public async Task<MovieListResponse> GetCoolMoviesAsync()
        {
            // Set the TMDB API URL and Authorization header
            string url = "movie/popular?region=US&language=en-us";

            // Set the TMDB API Base URL for API images
            string imageBaseUrl = "https://image.tmdb.org/t/p/w500";

            MovieListResponse? response = await _http.GetFromJsonAsync<MovieListResponse>(url, _jsonOptions) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to load cool movies!");

            foreach (var movie in response.Results)
            {
                if (!string.IsNullOrEmpty(movie.PosterPath))
                {
                    movie.PosterPath = $"{imageBaseUrl}{movie.PosterPath}";
                }
                else
                {
                    movie.PosterPath = "images/MovieTime/poster.png";
                }
            }
            return response;
        }

        /// <summary>
        /// GetFavoriteMoviesAsync
        /// </summary>
        /// <returns></returns>
        /// <exception cref="HttpIOException"></exception>
        public async Task<MovieListResponse> GetFavoriteMoviesAsync()
        {
            // Set the TMDB API URL and Authorization header
            string url = "movie/popular?region=US&language=en-us";

            // Set the TMDB API Base URL for API images
            string imageBaseUrl = "https://image.tmdb.org/t/p/w500";

            MovieListResponse? response = await _http.GetFromJsonAsync<MovieListResponse>(url, _jsonOptions) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to load cool movies!");

            foreach (var movie in response.Results)
            {
                if (!string.IsNullOrEmpty(movie.PosterPath))
                {
                    movie.PosterPath = $"{imageBaseUrl}{movie.PosterPath}";
                }
                else
                {
                    movie.PosterPath = "images/MovieTime/poster.png";
                }
            }
            return response;
        }

        /// <summary>
        /// SearchMoviesAsync: Searches for movies matching the specified query using the TMDB API.
        /// </summary>
        /// <param name="query">The search term to use when querying for movies.</param>
        /// <returns>A task representing the asynchronous operation, containing the list of movies that match the search
        /// criteria.</returns>
        /// <exception cref="HttpIOException">Thrown when the TMDB API response is invalid.</exception>
        public async Task<MovieListResponse> SearchMoviesAsync(string query)
        {
            // Set the TMDB API URL and Authorization header
            string url = $"search/movie?query={Uri.EscapeDataString(query)}&region=US&language=en-us&include_adult=false";

            // Set the TMDB API Base URL for API images
            string imageBaseUrl = "https://image.tmdb.org/t/p/w500";

            MovieListResponse? response = await _http.GetFromJsonAsync<MovieListResponse>(url, _jsonOptions) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to load search results!");

            foreach (var movie in response.Results)
            {
                if (!string.IsNullOrEmpty(movie.PosterPath))
                {
                    movie.PosterPath = $"{imageBaseUrl}{movie.PosterPath}";
                }
                else
                {
                    movie.PosterPath = "images/MovieTime/poster.png";
                }
            }
            return response;
        }

        /// <summary>
        /// GetMovieByIdAsync: Retrieves detailed information about a specific movie by its ID using the TMDB API.
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns></returns>
        /// <exception cref="HttpIOException"></exception>
        public async Task<MovieDetails> GetMovieByIdAsync(int movieId)
        {
            // Set the TMDB API URL and Authorization header
            string url = $"movie/{movieId}";

            // Set the TMDB API Base URL for API images
            string imageBaseUrl = "https://image.tmdb.org/t/p/w500";

            MovieDetails? movie = await _http.GetFromJsonAsync<MovieDetails>(url, _jsonOptions) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to load movie details!");

            movie.PosterPath = !string.IsNullOrEmpty(movie.PosterPath) ? $"{imageBaseUrl}{movie.PosterPath}" : "images/MovieTime/poster.png";

            movie.BackdropPath = !string.IsNullOrEmpty(movie.BackdropPath) ? $"{imageBaseUrl}{movie.BackdropPath}" : "images/MovieTime/backdrop.jpg";

            return movie;
        }

        /// <summary>
        /// GetMovieTrailerAsync: Retrieves the YouTube trailer for the specified movie from the TMDB API.
        /// </summary>
        /// <param name="movieId">The unique identifier of the movie.</param>
        /// <returns>A Video object containing trailer information, or null if no trailer is available.</returns>
        /// <exception cref="HttpIOException">Thrown when the TMDB API response is invalid.</exception>
        public async Task<Video?> GetMovieTrailerAsync(int movieId)
        {
            // Set the TMDB API URL and Authorization header
            string url = $"movie/{movieId}/videos?language=en-US";

            // Set the TMDB API Base URL for API vodeos
            string videoBaseUrl = "https://www.youtube.com/embed/";

            MovieVideosResponse? response = await _http.GetFromJsonAsync<MovieVideosResponse>(url, _jsonOptions) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to load movie trailer!");

            //Video? trailer = response.Results.FirstOrDefault(v => v.Type == "Trailer" && v.Site == "YouTube");

            Video? trailer = response.Results.FirstOrDefault(v => v.Site!.Contains("YouTube", StringComparison.OrdinalIgnoreCase) && v.Type!.Contains("Trailer", StringComparison.OrdinalIgnoreCase));

            if (trailer != null)
            {
                trailer.VideoUrl = $"{videoBaseUrl}{trailer.Key}";
            }
            return trailer;
        }

        /// <summary>
        /// GetMovieCreditsAsync: Retrieves the credits, including cast and crew, for the specified movie.
        /// </summary>
        /// <param name="movieId">The unique identifier of the movie.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the movie credits response, or
        /// null if not found.</returns>
        /// <exception cref="HttpIOException">Thrown when the API response is invalid.</exception>
        public async Task<CreditsResponse?> GetMovieCreditsAsync(int movieId)
        {
            // Set the TMDB API URL and Authorization header
            string url = $"movie/{movieId}/credits?language=en-US";

            // Set the TMDB API Base URL for API images
            string imageBaseUrl = "https://image.tmdb.org/t/p/w500";

            CreditsResponse? response = await _http.GetFromJsonAsync<CreditsResponse>(url, _jsonOptions) ?? throw new HttpIOException(HttpRequestError.InvalidResponse, "Failed to load movie credits!");

            foreach (var cast in response.Cast)
            {
                if (!string.IsNullOrEmpty(cast.ProfilePath))
                {
                    cast.ProfilePath = $"{imageBaseUrl}{cast.ProfilePath}";
                }
                else
                {
                    cast.ProfilePath = "images/MovieTime/profile.jpg";
                }
            }

            foreach (var crew in response.Crew)
            {
                if (!string.IsNullOrEmpty(crew.ProfilePath))
                {
                    crew.ProfilePath = $"{imageBaseUrl}{crew.ProfilePath}";
                }
                else
                {
                    crew.ProfilePath = "images/MovieTime/profile.jpg";
                }
            }

            return response;
        }
    }
}
