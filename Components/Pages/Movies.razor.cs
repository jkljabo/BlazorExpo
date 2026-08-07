using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Models.UI;
using BlazorCodeChallenge.Services;

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

        /// <summary>
        /// Technologies showcased by the Movies page.
        /// </summary>
        private static readonly IReadOnlyList<TechStackItem> Technologies =
        [
            new()
            {
                Name = "Blazor",
                IconClass = "devicon-blazor-original"
            },

            new()
            {
                Name = "C#",
                IconClass = "devicon-csharp-plain"
            },

            new()
            {
                Name = "Bootstrap",
                IconClass = "devicon-bootstrap-plain"
            },

            new()
            {
                Name = "JavaScript",
                IconClass = "devicon-javascript-plain"
            },

            new()
            {
                Name = "HTML5",
                IconClass = "devicon-html5-plain"
            },

            new()
            {
                Name = "CSS3",
                IconClass = "devicon-css3-plain"
            }
        ];
    }
}
