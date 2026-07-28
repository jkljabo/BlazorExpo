using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class Search
    {
        private MovieListResponse? searchResults;
        private bool isLoading = false;
        private string? errorMessage;

        [SupplyParameterFromQuery] public string? Query { get; set; }


        protected override async Task OnParametersSetAsync()
        {
            if (!string.IsNullOrEmpty(Query))
            {
                isLoading = true;
                errorMessage = null;
                searchResults = null;

                try
                {
                    searchResults = await TMDBService.SearchMoviesAsync(Query);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    errorMessage = "Unable to search for movies. Please try again later.";
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
    }
}
