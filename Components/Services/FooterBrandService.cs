using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Components.Services
{
    public class FooterBrandService
    {
        public FooterBrand Brand { get; private set; } = new();

        public event Action? OnChange;

        public void SetBrand(FooterBrand brand)
        {
            Brand = brand;
            NotifyStateChanged();
        }

        public void Reset()
        {
            Brand = new FooterBrand();
            NotifyStateChanged();
        }

        private void NotifyStateChanged()
        {
            OnChange?.Invoke();
        }
    }
}
