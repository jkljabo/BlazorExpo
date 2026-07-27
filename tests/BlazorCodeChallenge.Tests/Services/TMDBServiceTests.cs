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

    [Fact]
    public async Task SearchMoviesAsync_EscapesQueryAndRequestsSearchEndpoint()
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
        await service.SearchMoviesAsync("Star Wars: A New Hope");

        // Assert
        Assert.NotNull(handler.RequestUri);
        Assert.Equal(
            "https://localhost/tmdb/search/movie?query=Star%20Wars%3A%20A%20New%20Hope&region=US&language=en-us&include_adult=false",
            handler.RequestUri.OriginalString);
    }

    [Fact]
    public async Task GetMovieByIdAsync_WithImagePaths_PrefixesTmdbImageBaseUrl()
    {
        // Arrange
        const string json = """
        {
          "id": 303,
          "title": "Movie Details Test",
          "poster_path": "/details-poster.jpg",
          "backdrop_path": "/details-backdrop.jpg"
        }
        """;

        using var httpClient = CreateHttpClient(json);
        var service = CreateService(httpClient);

        // Act
        var movie = await service.GetMovieByIdAsync(303);

        // Assert
        Assert.Equal(303, movie.Id);
        Assert.Equal("Movie Details Test", movie.Title);
        Assert.Equal(
            "https://image.tmdb.org/t/p/w500/details-poster.jpg",
            movie.PosterPath);
        Assert.Equal(
            "https://image.tmdb.org/t/p/w500/details-backdrop.jpg",
            movie.BackdropPath);
    }

    [Fact]
    public async Task GetMovieByIdAsync_WithoutImagePaths_UsesFallbackImages()
    {
        // Arrange
        const string json = """
        {
          "id": 404,
          "title": "Movie Without Images",
          "poster_path": null,
          "backdrop_path": null
        }
        """;

        using var httpClient = CreateHttpClient(json);
        var service = CreateService(httpClient);

        // Act
        var movie = await service.GetMovieByIdAsync(404);

        // Assert
        Assert.Equal(404, movie.Id);
        Assert.Equal(
            "images/MovieTime/poster.png",
            movie.PosterPath);
        Assert.Equal(
            "images/MovieTime/backdrop.jpg",
            movie.BackdropPath);
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
