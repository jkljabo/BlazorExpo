using BlazorCodeChallenge.Constants;
using BlazorCodeChallenge.Services;

namespace BlazorCodeChallenge.Tests.Services;

public class AppStateTests
{
    [Fact]
    public void NewAppState_HasExpectedDefaults()
    {
        // Act
        var appState = new AppState();

        // Assert
        Assert.Equal("gray", appState.CurrentTheme);
        Assert.Same(
            FooterBrands.MagicSquareCode,
            appState.CurrentFooterBrand);
    }

    [Fact]
    public void SetTheme_UpdatesCurrentTheme()
    {
        // Arrange
        var appState = new AppState();

        // Act
        appState.SetTheme("dark");

        // Assert
        Assert.Equal("dark", appState.CurrentTheme);
    }

    [Fact]
    public void SetTheme_RaisesStateChanged()
    {
        // Arrange
        var appState = new AppState();
        var eventRaised = false;

        appState.StateChanged += () => eventRaised = true;

        // Act
        appState.SetTheme("dark");

        // Assert
        Assert.True(eventRaised);
    }

    [Fact]
    public void SetFooterBrand_UpdatesBrandAndRaisesStateChanged()
    {
        // Arrange
        var appState = new AppState();
        var eventRaised = false;

        appState.StateChanged += () => eventRaised = true;

        // Act
        appState.SetFooterBrand(FooterBrands.MovieTime);

        // Assert
        Assert.Same(
            FooterBrands.MovieTime,
            appState.CurrentFooterBrand);

        Assert.True(eventRaised);
    }
}
