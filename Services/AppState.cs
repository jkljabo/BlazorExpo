using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Services
{
    public class AppState
    {
        public string CurrentTheme { get; private set; } = "gray";

        // Note Code Fix:
        // public FooterBrand CurrentFooterBrand { get; private set; }
        public FooterBrand CurrentFooterBrand { get; private set; } = FooterBrands.MagicSquareCode;

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
