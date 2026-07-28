using BlazorCodeChallenge.Models;
using BlazorCodeChallenge.Services;
using Microsoft.JSInterop;
using NSubstitute;
using System.Text.Json;

namespace BlazorCodeChallenge.Tests.Services;

public class MovieFavoritesServiceTests
{
    private const string LocalStorageKey = "MovieFavorites";

    [Fact]
    public async Task GetFavoriteMoviesAsync_LoadsFavoritesFromLocalStorage()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();

        var storedFavorites = new List<Movie>
        {
            new() { Id = 101 },
            new() { Id = 202 }
        };

        var json = JsonSerializer.Serialize(storedFavorites);

        jsRuntime
            .InvokeAsync<string?>(
                "localStorage.getItem",
                Arg.Any<object?[]>())
            .Returns(new ValueTask<string?>(json));

        var service = new MovieFavoritesService(jsRuntime);

        // Act
        var favorites = await service.GetFavoriteMoviesAsync();

        // Assert
        Assert.Equal(2, favorites.Count);
        Assert.Contains(favorites, movie => movie.Id == 101);
        Assert.Contains(favorites, movie => movie.Id == 202);
    }

    [Fact]
    public async Task GetFavoriteMoviesAsync_AfterInitialLoad_UsesInMemoryCache()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();

        var storedFavorites = new List<Movie>
        {
            new() { Id = 101 }
        };

        var json = JsonSerializer.Serialize(storedFavorites);

        jsRuntime
            .InvokeAsync<string?>(
                "localStorage.getItem",
                Arg.Any<object?[]>())
            .Returns(new ValueTask<string?>(json));

        var service = new MovieFavoritesService(jsRuntime);

        // Act
        var firstResult = await service.GetFavoriteMoviesAsync();
        var secondResult = await service.GetFavoriteMoviesAsync();

        // Assert
        Assert.Single(firstResult);
        Assert.Single(secondResult);

        await jsRuntime
            .Received(1)
            .InvokeAsync<string?>(
                "localStorage.getItem",
                Arg.Any<object?[]>());
    }

    [Fact]
    public async Task AddFavorite_AddsMovieAndPersistsFavorites()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();

        jsRuntime
            .InvokeAsync<string?>(
                "localStorage.getItem",
                Arg.Any<object?[]>())
            .Returns(new ValueTask<string?>((string?)null));

        var service = new MovieFavoritesService(jsRuntime);
        var movie = new Movie { Id = 101 };

        // Act
        await service.AddFavorite(movie);

        // Assert
        var favorites = await service.GetFavoriteMoviesAsync();

        Assert.Single(favorites);
        Assert.Equal(101, favorites[0].Id);

        await jsRuntime
            .Received(1)
            .InvokeVoidAsync(
                "localStorage.setItem",
                Arg.Is<object?[]>(args =>
                    args != null &&
                    args.Length == 2 &&
                    Equals(args[0], LocalStorageKey)));
    }

    [Fact]
    public async Task AddFavorite_WhenMovieAlreadyExists_DoesNotPersistDuplicate()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();

        var storedFavorites = new List<Movie>
        {
            new() { Id = 101 }
        };

        var json = JsonSerializer.Serialize(storedFavorites);

        jsRuntime
            .InvokeAsync<string?>(
                "localStorage.getItem",
                Arg.Any<object?[]>())
            .Returns(new ValueTask<string?>(json));

        var service = new MovieFavoritesService(jsRuntime);

        // Act
        await service.AddFavorite(new Movie { Id = 101 });

        // Assert
        var favorites = await service.GetFavoriteMoviesAsync();

        Assert.Single(favorites);

        await jsRuntime
            .DidNotReceive()
            .InvokeVoidAsync(
                "localStorage.setItem",
                Arg.Any<object?[]>());
    }

    [Fact]
    public async Task RemoveFavorite_RemovesMovieAndPersistsFavorites()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();

        var storedFavorites = new List<Movie>
        {
            new() { Id = 101 },
            new() { Id = 202 }
        };

        var json = JsonSerializer.Serialize(storedFavorites);

        jsRuntime
            .InvokeAsync<string?>(
                "localStorage.getItem",
                Arg.Any<object?[]>())
            .Returns(new ValueTask<string?>(json));

        var service = new MovieFavoritesService(jsRuntime);

        // Act
        await service.RemoveFavorite(new Movie { Id = 101 });

        // Assert
        var favorites = await service.GetFavoriteMoviesAsync();

        Assert.Single(favorites);
        Assert.Equal(202, favorites[0].Id);

        await jsRuntime
            .Received(1)
            .InvokeVoidAsync(
                "localStorage.setItem",
                Arg.Any<object?[]>());
    }

    [Fact]
    public async Task GetFavoriteMoviesAsync_ReturnedListDoesNotModifyInternalCache()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();

        var storedFavorites = new List<Movie>
        {
            new() { Id = 101 }
        };

        var json = JsonSerializer.Serialize(storedFavorites);

        jsRuntime
            .InvokeAsync<string?>(
                "localStorage.getItem",
                Arg.Any<object?[]>())
            .Returns(new ValueTask<string?>(json));

        var service = new MovieFavoritesService(jsRuntime);

        // Act
        var firstResult = await service.GetFavoriteMoviesAsync();
        firstResult.Clear();

        var secondResult = await service.GetFavoriteMoviesAsync();

        // Assert
        Assert.Empty(firstResult);
        Assert.Single(secondResult);
        Assert.Equal(101, secondResult[0].Id);
    }

    [Fact]
    public async Task GetFavoriteMoviesAsync_WhenLocalStorageFails_PropagatesException()
    {
        // Arrange
        var jsRuntime = Substitute.For<IJSRuntime>();

        jsRuntime
            .InvokeAsync<string?>(
                "localStorage.getItem",
                Arg.Any<object?[]>())
            .Returns(new ValueTask<string?>(
                Task.FromException<string?>(
                    new InvalidOperationException("Storage unavailable."))));

        var service = new MovieFavoritesService(jsRuntime);

        // Act
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(
            () => service.GetFavoriteMoviesAsync());

        // Assert
        Assert.Equal("Storage unavailable.", exception.Message);
    }
}
