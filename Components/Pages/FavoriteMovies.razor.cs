using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class FavoriteMovies
    {
        private List<Movie> favoriteMovies = new List<Movie>();
        private bool isLoading = true;

        protected override async Task OnParametersSetAsync()
        {
            // load the favs from local storage service
            try
            {
                isLoading = true;
                favoriteMovies = await MovieFavoritesService.GetFavoriteMoviesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
