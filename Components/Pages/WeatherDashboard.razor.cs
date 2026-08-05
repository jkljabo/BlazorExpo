using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Models.UI;
using BlazorCodeChallenge.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class WeatherDashboard
    {
        [Inject]
        public WeatherService WeatherService { get; set; } = default!;

        public string? City { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }

        public string? Latitude { get; set; }
        public string? Longitude { get; set; }

        public ForecastPeriod? CurrentPeriod;

        public List<ForecastPeriod> ForecastPeriods = new();

        public string RadarUrl = "";

        /// <summary>
        /// OnInitialized : Initializes the component, sets the footer brand, and configures default loan values and the edit context.
        /// </summary>
        /// <remarks>Default loan values are assigned at startup for demonstration purposes.</remarks>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            AppState.SetFooterBrand(FooterBrands.NWSDashboard);
        }

        /// <summary>
        /// LoadWeather : Asynchronously loads the weather forecast using the specified latitude and longitude or by looking up
        /// coordinates if not provided.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        private async Task LoadWeather()
        {
            double lat;
            double lon;

            if (!string.IsNullOrWhiteSpace(Latitude) &&
                !string.IsNullOrWhiteSpace(Longitude))
            {
                lat = double.Parse(Latitude);
                lon = double.Parse(Longitude);
            }
            else
            {
                var address =
                    !string.IsNullOrWhiteSpace(ZipCode)
                        ? ZipCode
                        : $"{City}, {State}";

                (lat, lon) = await WeatherService.LookupCoordinatesAsync(address);
            }

            var weather = await WeatherService.GetForecastAsync(lat, lon);

            ForecastPeriods = weather.ForecastPeriods;
            CurrentPeriod = ForecastPeriods.FirstOrDefault();

            RadarUrl =
                $"https://radar.weather.gov/ridge/standard/{weather.RadarStation}_loop.gif";
        }

        /// <summary>
        /// Technologies showcased by the Weather Dashboard page.
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
    }
}
