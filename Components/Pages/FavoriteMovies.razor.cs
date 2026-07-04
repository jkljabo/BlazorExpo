using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class FavoriteMovies
    {
        //private MovieListResponse? favMovies;

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

        //protected override async Task OnInitializedAsync()
        //{
        //    try
        //    {
        //        isLoading = true;
        //        favoriteMovies = await TMDBService.GetFavoriteMoviesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exceptions (e.g., log the error, show a message to the user)
        //        Console.Write(ex.Message);
        //    }
        //    finally
        //    {
        //        isLoading = false;
        //    }
        //}

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
