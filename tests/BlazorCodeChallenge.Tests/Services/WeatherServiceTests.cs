using BlazorCodeChallenge.Services;
using System.Net;
using System.Text;

namespace BlazorCodeChallenge.Tests.Services;

public class WeatherServiceTests
{
    [Fact]
    public async Task LookupCoordinatesAsync_WithLocation_ReturnsCoordinates()
    {
        // Arrange
        const string json = """
        [
          {
            "place_id": 12345,
            "lat": "33.9526",
            "lon": "-84.5499",
            "display_name": "Marietta, Georgia, United States"
          }
        ]
        """;

        var handler = new StubHttpMessageHandler(_ => json);

        using var httpClient = new HttpClient(handler);
        var service = new WeatherService(httpClient);

        // Act
        var (lat, lon) =
            await service.LookupCoordinatesAsync("Marietta, GA");

        // Assert
        Assert.Equal(33.9526, lat);
        Assert.Equal(-84.5499, lon);
    }

    [Fact]
    public async Task LookupCoordinatesAsync_EscapesAddressAndRequestsNominatim()
    {
        // Arrange
        const string json = """
        [
          {
            "place_id": 12345,
            "lat": "33.9526",
            "lon": "-84.5499",
            "display_name": "Marietta, Georgia, United States"
          }
        ]
        """;

        var handler = new StubHttpMessageHandler(_ => json);

        using var httpClient = new HttpClient(handler);
        var service = new WeatherService(httpClient);

        // Act
        await service.LookupCoordinatesAsync("Marietta, GA");

        // Assert
        var request = Assert.Single(handler.Requests);

        Assert.Equal(
            "https://nominatim.openstreetmap.org/search?q=Marietta%2C%20GA&countrycodes=us&format=jsonv2",
            request.RequestUri!.OriginalString);
    }

    [Fact]
    public async Task LookupCoordinatesAsync_WithoutLocation_ThrowsException()
    {
        // Arrange
        const string json = "[]";

        var handler = new StubHttpMessageHandler(_ => json);

        using var httpClient = new HttpClient(handler);
        var service = new WeatherService(httpClient);

        // Act
        var exception = await Assert.ThrowsAsync<Exception>(
            () => service.LookupCoordinatesAsync("Not A Real Location"));

        // Assert
        Assert.Equal("Location not found.", exception.Message);
    }

    [Fact]
    public async Task GetForecastAsync_ReturnsForecastPeriodsAndRadarStation()
    {
        // Arrange
        const string pointJson = """
        {
          "properties": {
            "forecast": "https://api.weather.gov/gridpoints/FFC/52,88/forecast",
            "forecastHourly": "https://api.weather.gov/gridpoints/FFC/52,88/forecast/hourly",
            "radarStation": "KFFC"
          }
        }
        """;

        const string forecastJson = """
        {
          "properties": {
            "periods": [
              {
                "number": 1,
                "name": "Today",
                "temperature": 88,
                "temperatureUnit": "F",
                "windSpeed": "5 mph",
                "windDirection": "SW",
                "shortForecast": "Sunny",
                "detailedForecast": "Sunny with clear skies."
              }
            ]
          }
        }
        """;

        var handler = new StubHttpMessageHandler(request =>
            request.RequestUri!.AbsolutePath.StartsWith("/points/")
                ? pointJson
                : forecastJson);

        using var httpClient = new HttpClient(handler);
        var service = new WeatherService(httpClient);

        // Act
        var result =
            await service.GetForecastAsync(33.9526, -84.5499);

        // Assert
        Assert.Equal("KFFC", result.RadarStation);

        var period = Assert.Single(result.ForecastPeriods);

        Assert.Equal(1, period.Number);
        Assert.Equal("Today", period.Name);
        Assert.Equal(88, period.Temperature);
        Assert.Equal("F", period.TemperatureUnit);
        Assert.Equal("Sunny", period.ShortForecast);

        Assert.Equal(2, handler.Requests.Count);

        Assert.Equal(
            "https://api.weather.gov/points/33.9526,-84.5499",
            handler.Requests[0].RequestUri!.OriginalString);

        Assert.Equal(
            "https://api.weather.gov/gridpoints/FFC/52,88/forecast",
            handler.Requests[1].RequestUri!.OriginalString);
    }

    [Fact]
    public async Task GetForecastAsync_WithNullPointResponse_ThrowsMeaningfulException()
    {
        // Arrange
        const string json = "null";

        var handler = new StubHttpMessageHandler(_ => json);

        using var httpClient = new HttpClient(handler);
        var service = new WeatherService(httpClient);

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.GetForecastAsync(33.9526, -84.5499));

        // Assert
        Assert.Equal(
            "Weather point information was not returned.",
            exception.Message);
    }

    [Fact]
    public async Task GetForecastAsync_WithNullForecastResponse_ThrowsMeaningfulException()
    {
        // Arrange
        const string pointJson = """
        {
          "properties": {
            "forecast": "https://api.weather.gov/gridpoints/FFC/52,88/forecast",
            "radarStation": "KFFC"
          }
        }
        """;

        var handler = new StubHttpMessageHandler(request =>
            request.RequestUri!.AbsoluteUri.Contains("/points/")
                ? pointJson
                : "null");

        using var httpClient = new HttpClient(handler);
        var service = new WeatherService(httpClient);

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.GetForecastAsync(33.9526, -84.5499));

        // Assert
        Assert.Equal(
            "Weather forecast information was not returned.",
            exception.Message);
    }

    private sealed class StubHttpMessageHandler(
        Func<HttpRequestMessage, string> responseFactory)
        : HttpMessageHandler
    {
        public List<HttpRequestMessage> Requests { get; } = [];

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            Requests.Add(request);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    responseFactory(request),
                    Encoding.UTF8,
                    "application/json")
            };

            return Task.FromResult(response);
        }
    }
}
