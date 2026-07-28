using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class MovieById
    {
        private MovieDetails? movie;
        private Video? trailer;
        private CreditsResponse? credits;
        private List<Cast> actors = [];
        private bool isLoading = true;

        [Parameter] public int MovieId { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                isLoading = true;

                var movieTask = TMDBService.GetMovieByIdAsync(MovieId);
                var trailerTask = TMDBService.GetMovieTrailerAsync(MovieId);
                var creditsTask = TMDBService.GetMovieCreditsAsync(MovieId);

                await Task.WhenAll(movieTask, trailerTask, creditsTask);

                movie = await movieTask;
                trailer = await trailerTask;
                credits = await creditsTask;

                actors = credits?.Cast ?? [];
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                isLoading = false;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await using var jsModule = await JS.InvokeAsync<IJSObjectReference>("import", "/Components/Pages/MovieById.razor.js");

            if (trailer is not null)
            {
                await jsModule.InvokeVoidAsync("initVideoPlayer", trailer.VideoUrl);
            }
            else
            {
                await jsModule.InvokeVoidAsync("initVideoPlayer", string.Empty);
            }
        }

        protected override void OnInitialized()
        {
            AppState.SetFooterBrand(FooterBrands.MovieTime);
        }
    }
}
