using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Models.UI;
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

        /// <summary>
        /// Technologies showcased by the Cool Movies page.
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
