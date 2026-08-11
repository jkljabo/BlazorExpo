using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Models.UI;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class FavoriteMovies
    {
        private List<Movie> favoriteMovies = [];
        private bool isLoading = true;
        private string? errorMessage;

        protected override async Task OnParametersSetAsync()
        {
            await LoadFavoriteMoviesAsync();
        }

        protected override void OnInitialized()
        {
            AppState.SetFooterBrand(FooterBrands.MovieTime);
        }

        /// <summary>
        /// Loads the user's saved favorite movies.
        /// </summary>
        private async Task LoadFavoriteMoviesAsync()
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

        /// <summary>
        /// Technologies showcased by the Favorite Movies page.
        /// </summary>
        private static readonly IReadOnlyList<TechStackItem> Technologies =
        [
            new()
            {
                Name = "Blazor",
                IconClass = "devicon-blazor-original colored"
            },

            new()
            {
                Name = "C#",
                IconClass = "devicon-csharp-plain colored"
            },

            new()
            {
                Name = "Bootstrap",
                IconClass = "devicon-bootstrap-plain colored"
            },

            new()
            {
                Name = "JavaScript",
                IconClass = "devicon-javascript-plain colored"
            },

            new()
            {
                Name = "HTML5",
                IconClass = "devicon-html5-plain colored"
            },

            new()
            {
                Name = "CSS3",
                IconClass = "devicon-css3-plain colored"
            }
        ];
    }
}
