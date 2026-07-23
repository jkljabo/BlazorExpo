using BlazorCodeChallenge.Models;
using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorCodeChallenge.Services
{
    public class MovieFavoritesService(IJSRuntime jsRuntime)
    {
        private const string LocalStorageKey = "MovieFavorites";

        private List<Movie>? favoriteMoviesCache;

        /// <summary>
        /// Retrieves the list of favorite movies.
        /// Favorites are loaded from local storage once and then cached
        /// in memory for the lifetime of the service.
        /// </summary>
        public async Task<List<Movie>> GetFavoriteMoviesAsync()
        {
            var favorites = await GetOrLoadFavoritesAsync();

            // Return a copy so callers cannot modify the internal cache.
            return new List<Movie>(favorites);
        }

        /// <summary>
        /// Loads favorite movies from local storage when the in-memory
        /// cache has not yet been initialized.
        /// </summary>
        private async Task<List<Movie>> GetOrLoadFavoritesAsync()
        {
            if (favoriteMoviesCache is not null)
            {
                return favoriteMoviesCache;
            }

            try
            {
                var json = await jsRuntime.InvokeAsync<string?>(
                    "localStorage.getItem",
                    LocalStorageKey);

                favoriteMoviesCache = string.IsNullOrWhiteSpace(json)
                    ? []
                    : JsonSerializer.Deserialize<List<Movie>>(json) ?? [];
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error retrieving favorite movies: {ex.Message}");

                favoriteMoviesCache = [];
            }

            return favoriteMoviesCache;
        }

        /// <summary>
        /// Persists the current in-memory favorites collection to local storage.
        /// </summary>
        private async Task SaveFavoritesAsync()
        {
            try
            {
                var favorites = favoriteMoviesCache ?? [];
                var json = JsonSerializer.Serialize(favorites);

                await jsRuntime.InvokeVoidAsync(
                    "localStorage.setItem",
                    LocalStorageKey,
                    json);
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"Error saving favorite movies: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds the specified movie to the favorites list if it is not
        /// already included.
        /// </summary>
        public async Task AddFavorite(Movie movie)
        {
            var currentFavorites = await GetOrLoadFavoritesAsync();

            if (currentFavorites.Any(m => m.Id == movie.Id))
            {
                return;
            }

            currentFavorites.Add(movie);
            await SaveFavoritesAsync();
        }

        /// <summary>
        /// Removes the specified movie from the favorites list if present.
        /// </summary>
        public async Task RemoveFavorite(Movie movie)
        {
            var currentFavorites = await GetOrLoadFavoritesAsync();

            var removed = currentFavorites.RemoveAll(
                m => m.Id == movie.Id);

            if (removed > 0)
            {
                await SaveFavoritesAsync();
            }
        }

        /// <summary>
        /// Determines whether the specified movie is in the favorites list.
        /// </summary>
        public async Task<bool> IsFavorite(int id)
        {
            var currentFavorites = await GetOrLoadFavoritesAsync();

            return currentFavorites.Any(m => m.Id == id);
        }
    }
}
