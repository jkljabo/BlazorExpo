using BlazorCodeChallenge.Components.Services;
using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace BlazorCodeChallenge.Components.Pages
{
    public partial class WeatherDashboard
    {
        [Inject]
        public HttpClient Http { get; set; } = default!;

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
        /// Dispose : Releases resources used by the instance and resets the FooterBrandService.
        /// </summary>
        public void Dispose()
        {
            // nothing (preferred long-term)
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
                (lat, lon) = await LookupCoordinates();
            }

            await LoadForecast(lat, lon);
        }

        /// <summary>
        /// LookupCoordinates : Retrieves the latitude and longitude for the specified address using the city, state, or zip code, querying
        /// the OpenStreetMap Nominatim API.
        /// </summary>
        /// <returns>A tuple containing the latitude and longitude as double values.</returns>
        private async Task<(double lat, double lon)> LookupCoordinates()
        {
            try
            {
                var address =
                !string.IsNullOrWhiteSpace(ZipCode)
                    ? ZipCode
                    : $"{City}, {State}";

                Http.DefaultRequestHeaders.Clear();

                Http.DefaultRequestHeaders.Add(
                    "User-Agent",
                    "BlazorWeatherDashboard/1.0");


                var url =
                    $"https://nominatim.openstreetmap.org/search" +
                    $"?q={Uri.EscapeDataString(address)}" +
                    $"&countrycodes=us" +
                    $"&format=jsonv2";

                var result = await Http.GetFromJsonAsync<List<NominatimLocation>>(url);

                var location = result?.FirstOrDefault();

                if (location == null)
                {
                    throw new Exception("Location not found.");
                }

                return (
                    double.Parse(location.Lat),
                    double.Parse(location.Lon)
                );

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// LoadForecast :     
        /// </summary>
        /// <param name="lat"></param>
        /// <param name="lon"></param>
        /// <returns></returns>
        private async Task LoadForecast(double lat, double lon)
        {
            Http.DefaultRequestHeaders.Clear();

            Http.DefaultRequestHeaders.Add(
                "User-Agent",
                "MyWeatherApp (your@email.com)");

            var pointInfo =
                await Http.GetFromJsonAsync<PointResponse>(
                    $"https://api.weather.gov/points/{lat},{lon}");

            var forecast =
                await Http.GetFromJsonAsync<ForecastResponse>(
                    pointInfo!.properties.forecast);

            ForecastPeriods =
                forecast!.properties.periods;

            CurrentPeriod =
                ForecastPeriods.FirstOrDefault();

            RadarUrl =
                $"https://radar.weather.gov/ridge/standard/{pointInfo.properties.radarStation}_loop.gif";
        }
    }
}
