using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
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
        /// Dispose : Releases resources used by the instance and resets the FooterBrandService.
        /// </summary>
        public void Dispose()
        {
            // nothing (preferred long-term)
        }
    }
}
