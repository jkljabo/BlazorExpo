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
        private string? errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                isLoading = true;
                errorMessage = null;
                movies = await TMDBService.GetMoviesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                errorMessage = "Unable to load movies. Please try again later.";
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
    }
}
