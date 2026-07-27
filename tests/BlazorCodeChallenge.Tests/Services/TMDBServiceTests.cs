using BlazorCodeChallenge.Services;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Text;

namespace BlazorCodeChallenge.Tests.Services;

public class TMDBServiceTests
{
    [Fact]
    public async Task GetMoviesAsync_WithPosterPath_PrefixesTmdbImageBaseUrl()
    {
        // Arrange
        const string json = """
        {
          "page": 1,
          "results": [
            {
              "id": 101,
              "title": "Test Movie",
              "poster_path": "/test-poster.jpg"
            }
          ],
          "total_pages": 1,
          "total_results": 1
        }
        """;

        using var httpClient = CreateHttpClient(json);
        var service = CreateService(httpClient);

        // Act
        var result = await service.GetMoviesAsync();

        // Assert
        var movie = Assert.Single(result.Results);

        Assert.Equal(101, movie.Id);
        Assert.Equal("Test Movie", movie.Title);
        Assert.Equal(
            "https://image.tmdb.org/t/p/w500/test-poster.jpg",
            movie.PosterPath);
    }

    [Fact]
    public async Task GetMoviesAsync_WithoutPosterPath_UsesFallbackPoster()
    {
        // Arrange
        const string json = """
        {
          "page": 1,
          "results": [
            {
              "id": 202,
              "title": "Movie Without Poster",
              "poster_path": null
            }
          ],
          "total_pages": 1,
          "total_results": 1
        }
        """;

        using var httpClient = CreateHttpClient(json);
        var service = CreateService(httpClient);

        // Act
        var result = await service.GetMoviesAsync();

        // Assert
        var movie = Assert.Single(result.Results);

        Assert.Equal(202, movie.Id);
        Assert.Equal(
            "images/MovieTime/poster.png",
            movie.PosterPath);
    }

    [Fact]
    public async Task GetMoviesAsync_RequestsNowPlayingEndpoint()
    {
        // Arrange
        const string json = """
        {
          "page": 1,
          "results": [],
          "total_pages": 0,
          "total_results": 0
        }
        """;

        var handler = new StubHttpMessageHandler(json);

        using var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://localhost/")
        };

        var service = CreateService(httpClient);

        // Act
        await service.GetMoviesAsync();

        // Assert
        Assert.NotNull(handler.RequestUri);
        Assert.Equal(
            "https://localhost/tmdb/movie/now_playing?region=US&language=en-us",
            handler.RequestUri.ToString());
    }

    private static HttpClient CreateHttpClient(string json)
    {
        var handler = new StubHttpMessageHandler(json);

        return new HttpClient(handler)
        {
            BaseAddress = new Uri("https://localhost/")
        };
    }

    private static TMDBService CreateService(HttpClient httpClient)
    {
        var configuration = new ConfigurationBuilder().Build();

        return new TMDBService(httpClient, configuration);
    }

    private sealed class StubHttpMessageHandler(string responseContent)
        : HttpMessageHandler
    {
        public Uri? RequestUri { get; private set; }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            RequestUri = request.RequestUri;

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    responseContent,
                    Encoding.UTF8,
                    "application/json")
            };

            return Task.FromResult(response);
        }
    }
}
