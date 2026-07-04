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
                movie = await TMDBService.GetMovieByIdAsync(MovieId);
                trailer = await TMDBService.GetMovieTrailerAsync(MovieId);
                credits = await TMDBService.GetMovieCreditsAsync(MovieId);
                actors = credits?.Cast ?? [];
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error, show a message to the user)
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
                //string trailerURL = $"https://www.youtube.com/embed/{trailer.Key}";
                //await jsModule.InvokeVoidAsync("initVideoPlayer", trailerURL);
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

        public void Dispose()
        {
            // nothing (preferred long-term)
        }
    }
}
