using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Services
{
    public class AppState
    {
        public string CurrentTheme { get; private set; } = "gray";

        public FooterBrand CurrentFooterBrand { get; private set; }

        public event Action? StateChanged;

        public void SetTheme(string theme)
        {
            CurrentTheme = theme;
            NotifyStateChanged();
        }

        public void SetFooterBrand(FooterBrand brand)
        {
            CurrentFooterBrand = brand;
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
            => StateChanged?.Invoke();
    }
}
