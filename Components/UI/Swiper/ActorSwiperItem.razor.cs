using BlazorCodeChallenge.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorCodeChallenge.Components.UI.Swiper
{
    public partial class ActorSwiperItem
    {
        [Parameter, EditorRequired] public Cast? Actor { get; set; }

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
