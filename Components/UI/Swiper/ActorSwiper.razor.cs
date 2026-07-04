using BlazorCodeChallenge.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorCodeChallenge.Components.UI.Swiper
{
    public partial class ActorSwiper
    {
        [Parameter,EditorRequired] public List<Cast> Actors { get; set; } = [];

        ElementReference? swiperContainer;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && swiperContainer.HasValue)
            {
                await using var jsModule = await JS.InvokeAsync<IJSObjectReference>("import", "/Components/UI/Swiper/ActorSwiper.razor.js");

                await jsModule.InvokeVoidAsync("initializeSwiper", swiperContainer);
            }
        }

        private string GetProfileImageUrl(string? profilePath)
        {
            if (string.IsNullOrEmpty(profilePath))
            {
                return "/images/MovieTime/profile.jpg"; // Path to your placeholder image
            }
            return $"https://image.tmdb.org/t/p/w500{profilePath}";
        }
    }
}
