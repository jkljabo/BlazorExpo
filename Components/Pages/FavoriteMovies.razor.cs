using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class FavoriteMovies
    {
        private List<Movie> favoriteMovies = [];
        private bool isLoading = true;
        private string? errorMessage;

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                isLoading = true;
                errorMessage = null;

                favoriteMovies = await MovieFavoritesService.GetFavoriteMoviesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                errorMessage = "Unable to load your favorite movies. Please try again later.";
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
