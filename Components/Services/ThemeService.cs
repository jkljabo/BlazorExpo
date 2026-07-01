namespace BlazorCodeChallenge.Components.Services
{
    public class ThemeService
    {
        public string CurrentTheme { get; private set; } = "gray";

        public event Action? ThemeChanged;

        public void SetTheme(string theme)
        {
            CurrentTheme = theme;
            ThemeChanged?.Invoke();
        }
    }
}
