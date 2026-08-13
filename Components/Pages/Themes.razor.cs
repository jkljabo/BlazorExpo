using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models.UI;
using BlazorCodeChallenge.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorCodeChallenge.Components.Pages;

public partial class Themes
{
    private string CurrentTheme = string.Empty;

    protected override void OnInitialized()
    {
        AppState.SetFooterBrand(FooterBrands.MagicSquareCode);

        CurrentTheme = AppState.CurrentTheme;
    }

    private async Task ChangeTheme(string theme)
    {
        CurrentTheme = theme;

        // setTheme is defined in index.html
        await JS.InvokeVoidAsync("setTheme", theme);

        AppState.SetTheme(theme);
    }

    private static readonly IReadOnlyList<string> SiteThemes =
    [
        "code-magic",
        "blue",
        "indigo",
        "purple",
        "pink",
        "red",
        "orange",
        "yellow",
        "green",
        "teal",
        "cyan",
        "gray"
    ];

    private static readonly IReadOnlyList<string> PaletteWeights =
    [
        "core",
        "100",
        "150",
        "200",
        "300",
        "400",
        "500",
        "600",
        "700",
        "800",
        "850",
        "900"
    ];

    /// <summary>
    /// Technologies showcased by the Themes page.
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

    [Inject]
    private AppState AppState { get; set; } = default!;

    [Inject]
    private IJSRuntime JS { get; set; } = default!;
}