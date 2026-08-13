using Microsoft.AspNetCore.Components;

namespace BlazorCodeChallenge.Components.Layout;

public partial class ThemeCard
{
    /// <summary>
    /// Theme represented by this preview card.
    /// </summary>
    [Parameter]
    public string Theme { get; set; } = string.Empty;

    /// <summary>
    /// Color weights displayed for the preview palette.
    /// </summary>
    [Parameter]
    public IReadOnlyList<string> PaletteWeights { get; set; } = [];

    /// <summary>
    /// Gets or sets a value indicating whether the theme is currently active.
    /// </summary>
    [Parameter]
    public bool IsCurrentTheme { get; set; }

    /// <summary>
    /// Raised when the card is selected.
    /// </summary>
    [Parameter]
    public EventCallback<string> OnSelected { get; set; }

    private string CardCssClass => IsCurrentTheme
        ? "theme-card theme-card--selected"
        : "theme-card";

    private string DisplayName => string.Join(' ',
        Theme.Split('-')
             .Select(word =>
                 char.ToUpper(word[0]) + word[1..]));

    private string Description =>
        ThemeDescriptions.TryGetValue(Theme, out var description)
            ? description
            : "Application theme preview.";

    private async Task SelectTheme()
    {
        await OnSelected.InvokeAsync(Theme);
    }

    private static readonly Dictionary<string, string> ThemeDescriptions = new()
    {
        ["code-magic"] = "Default BlazorExpo theme inspired by modern developer tooling.",
        ["blue"] = "Professional and clean for enterprise applications.",
        ["indigo"] = "Deep modern tones with strong contrast.",
        ["purple"] = "Creative and vibrant for dashboards.",
        ["pink"] = "Bold and playful for marketing experiences.",
        ["red"] = "High energy with strong visual emphasis.",
        ["orange"] = "Warm and approachable.",
        ["yellow"] = "Bright and optimistic.",
        ["green"] = "Calm and productivity focused.",
        ["teal"] = "Balanced and modern.",
        ["cyan"] = "Fresh, light, and technical.",
        ["gray"] = "Neutral and minimal."
    };
}