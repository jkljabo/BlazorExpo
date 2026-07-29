using BlazorCodeChallenge.Core.Engineering;

namespace BlazorCodeChallenge.Engineering.Tests.Core;

public class EngineeringSuiteServiceTests
{
    [Fact]
    public void GetStatus_ReturnsOperationalEngineeringSuiteStatus()
    {
        // Arrange
        var service = new EngineeringSuiteService();

        // Act
        var result = service.GetStatus();

        // Assert
        Assert.Equal("BlazorExpo Engineering Suite", result.Name);
        Assert.Equal("1.0", result.Version);
        Assert.Equal("Operational", result.Status);

        Assert.Contains("Blazor WebAssembly", result.Capabilities);
        Assert.Contains("ASP.NET Core Web API", result.Capabilities);
        Assert.Contains("Shared Contracts", result.Capabilities);
        Assert.Contains("Core Application Services", result.Capabilities);
    }
}
