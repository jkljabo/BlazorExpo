using BlazorCodeChallenge.Models.UI;
using Microsoft.AspNetCore.Components;

namespace BlazorCodeChallenge.Components.Layout
{
    public partial class TechStack
    {
        /// <summary>
        /// Gets or sets the technologies displayed by the component.
        /// </summary>
        [Parameter]
        public IReadOnlyList<TechStackItem> Items { get; set; } = [];

        /// <summary>
        /// Determines how the technology icons are rendered.
        /// </summary>
        [Parameter]
        public TechStackVariant Variant { get; set; } = TechStackVariant.Auto;

        private string VariantClass => 
            Variant == TechStackVariant.Monochrome
                ? "tech-stack--monochrome"
                : string.Empty;
    }
}
