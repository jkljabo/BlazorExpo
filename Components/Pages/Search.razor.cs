using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class Search
    {
        private MovieListResponse? searchResults;
        private bool isLoading = false;

        [SupplyParameterFromQuery] public string? Query { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(Query))
            {
                isLoading = true;
                try
                {
                    searchResults = await TMDBService.SearchMoviesAsync(Query);
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
