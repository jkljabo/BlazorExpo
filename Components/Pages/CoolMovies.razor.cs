using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Services;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class CoolMovies
    {
        private MovieListResponse? coolMovies;
        private bool isLoading = true;
        private string? errorMessage;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                isLoading = true;
                errorMessage = null;
                coolMovies = await TMDBService.GetCoolMoviesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                errorMessage = "Unable to load cool movies. Please try again later.";
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
