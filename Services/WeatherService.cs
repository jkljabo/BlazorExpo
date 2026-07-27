using BlazorCodeChallenge.Models;
using System.Net.Http.Json;

namespace BlazorCodeChallenge.Services
{
    public class WeatherService(HttpClient http)
    {
        public async Task<(double lat, double lon)> LookupCoordinatesAsync(
            string address)
        {
            http.DefaultRequestHeaders.Clear();

            http.DefaultRequestHeaders.Add(
                "User-Agent",
                "BlazorWeatherDashboard/1.0");

            var url =
                $"https://nominatim.openstreetmap.org/search" +
                $"?q={Uri.EscapeDataString(address)}" +
                $"&countrycodes=us" +
                $"&format=jsonv2";

            var result =
                await http.GetFromJsonAsync<List<NominatimLocation>>(url);

            var location = result?.FirstOrDefault();

            if (location == null)
            {
                throw new Exception("Location not found.");
            }

            return (
                double.Parse(location.Lat),
                double.Parse(location.Lon));
        }

        public async Task<WeatherForecastResult> GetForecastAsync(
            double lat,
            double lon)
        {
            http.DefaultRequestHeaders.Clear();

            http.DefaultRequestHeaders.Add(
                "User-Agent",
                "MyWeatherApp (your@email.com)");

            var pointInfo = await http.GetFromJsonAsync<PointResponse>(
                $"https://api.weather.gov/points/{lat},{lon}");

            if (pointInfo == null)
            {
                throw new InvalidOperationException(
                    "Weather point information was not returned.");
            }

            var forecast = await http.GetFromJsonAsync<ForecastResponse>(
                pointInfo.properties.forecast);

            if (forecast == null)
            {
                throw new InvalidOperationException(
                    "Weather forecast information was not returned.");
            }

            return new WeatherForecastResult
            {
                ForecastPeriods = forecast.properties.periods,
                RadarStation = pointInfo.properties.radarStation
            };
        }
    }
}
