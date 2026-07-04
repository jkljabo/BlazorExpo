using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Services;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class CoolMovies
    {
        private MovieListResponse? coolMovies;
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                isLoading = true;
                coolMovies = await TMDBService.GetCoolMoviesAsync();
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
