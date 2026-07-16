using BlazorCodeChallenge.Components.Pages;
using BlazorCodeChallenge.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorCodeChallenge.Services
{
    public class MovieFavoritesService(IJSRuntime jsRuntime)
    {
        private const string LocalStorageKey = "MovieFavorites";

        /// <summary>
        /// GetFavoriteMoviesAsync: Retrieves the list of favorite movies from local storage.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Movie>> GetFavoriteMoviesAsync()
        {
            try
            {
                var json = await jsRuntime.InvokeAsync<string?>(
                    "localStorage.getItem",
                    LocalStorageKey);

                if (string.IsNullOrWhiteSpace(json))
                    return [];

                return JsonSerializer.Deserialize<List<Movie>>(json) ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving favorite movies: {ex.Message}");
                return [];
            }
        }

        /// <summary>
        /// AddFavoriteMovieAsync: Adds a collection of movies to local storage as favorites asynchronously.
        /// </summary>
        /// <param name="movies">The list of movies to add to the favorites.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddFavoriteMovieAsync(List<Movie> movies)
        {
            try
            {
                var json = JsonSerializer.Serialize(movies);
                await jsRuntime.InvokeVoidAsync("localStorage.setItem", LocalStorageKey, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding favorite movie: {ex.Message}");
            }
        }

        /// <summary>
        /// AddFavorite: Adds the specified movie to the favorites list if it is not already included.
        /// </summary>
        /// <param name="movie">The movie to add to the favorites list.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task AddFavorite(Movie movie)
        {
            var currentFavorites = await GetFavoriteMoviesAsync();

            if(!currentFavorites.Any(m => m.Id == movie.Id))
            {
                currentFavorites.Add(movie);
                await AddFavoriteMovieAsync(currentFavorites);
            }
        }

        /// <summary>
        /// RemoveFavorite: Removes a specified movie from the favorites list if it is already included.
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public async Task RemoveFavorite(Movie movie)
        {
            var currentFavorites = await GetFavoriteMoviesAsync();

            currentFavorites = currentFavorites.Where(m => m.Id != movie.Id).ToList();

            await AddFavoriteMovieAsync(currentFavorites);
        }

        /// <summary>
        /// IsFavorite: Determines if a movie is in the favorites movie list.
        /// </summary>
        /// <param name="id">Id of the movie in question</param>
        /// <returns></returns>
        public async Task<bool> IsFavorite(int id)
        {
            var currentFavorites = await GetFavoriteMoviesAsync();
            bool IsFavorite = currentFavorites.Any(m => m.Id == id);
            return IsFavorite;
        }
    }
}
