namespace BlazorCodeChallenge.Models.UI
{

    /// <summary>
    /// Determines how technology icons are rendered.
    /// </summary>
    public enum TechStackVariant
    {
        /// <summary>
        /// Uses the application's default TechStack icon styling.
        /// </summary>
        Auto,

        /// <summary>
        /// Displays the icons using their native DevIcon colors.
        /// </summary>
        Colored,

        /// <summary>
        /// Displays all icons using the current foreground color.
        /// </summary>
        Monochrome
    }
}
