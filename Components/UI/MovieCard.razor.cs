using BlazorCodeChallenge.Components.Pages;
using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorCodeChallenge.Components.UI
{
    public partial class MovieCard
    {
        [Parameter, EditorRequired]
        public Movie Movie { get; set; } = null!;

        [Parameter] 
        public EventCallback OnChange { get; set; }

        bool isFavorite;

        protected override async Task OnParametersSetAsync()
        {
            if (Movie != null)
            {
                isFavorite = await MovieFavoritesService.IsFavorite(Movie.Id);
            }
        }

        private async Task HandleToggleFavorite()
        {
            if (Movie is null)
            {
                return;
            }

            if (!isFavorite)
            {
                await MovieFavoritesService.AddFavorite(Movie);
                isFavorite = true;
            }
            else
            {
                await MovieFavoritesService.RemoveFavorite(Movie); 
                isFavorite = false;
            }
            await OnChange.InvokeAsync();
        }
    }
}
