using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Models.UI;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class Home
    {
        /// <summary>
        /// OnInitialized : Initializes the component, sets the footer brand, and configures default loan values and the edit context.
        /// </summary>
        /// <remarks>Default loan values are assigned at startup for demonstration purposes.</remarks>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            AppState.SetFooterBrand(FooterBrands.MagicSquareCode);
        }

        /// <summary>
        /// Technologies showcased by the Home page.
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
