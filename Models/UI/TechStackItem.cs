namespace BlazorCodeChallenge.Models.UI
{
    /// <summary>
    /// Represents a technology displayed within the TechStack component.
    /// </summary>
    public sealed record TechStackItem
    {
        /// <summary>
        /// Gets the display name of the technology.
        /// </summary>
        public required string Name { get; init; }

        /// <summary>
        /// Gets the CSS class used to render the technology icon.
        /// </summary>
        public required string IconClass { get; init; }
    }
}
