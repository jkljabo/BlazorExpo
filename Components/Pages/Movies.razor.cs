using BlazorCodeChallenge.Services;
using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Net.Http.Json;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class Movies
    {
        private MovieListResponse? movies;
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                isLoading = true;
                movies = await TMDBService.GetMoviesAsync();
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
