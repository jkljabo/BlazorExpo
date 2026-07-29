using BlazorCodeChallenge.Core.Diagnostics;

namespace BlazorCodeChallenge.Engineering.Tests.Diagnostics;

public sealed class RuntimeDiagnosticsServiceTests
{
    private readonly RuntimeDiagnosticsService _service = new();

    [Fact]
    public void GetDiagnostics_ReturnsRuntimeInformation()
    {
        const string applicationName = "BlazorExpo Engineering Suite";
        const string environmentName = "Test";

        var before = DateTimeOffset.UtcNow;

        var result = _service.GetDiagnostics(
            applicationName,
            environmentName);

        var after = DateTimeOffset.UtcNow;

        Assert.Equal(applicationName, result.ApplicationName);
        Assert.Equal(environmentName, result.EnvironmentName);
        Assert.False(string.IsNullOrWhiteSpace(result.FrameworkDescription));
        Assert.False(string.IsNullOrWhiteSpace(result.OperatingSystemDescription));
        Assert.False(string.IsNullOrWhiteSpace(result.ProcessArchitecture));
        Assert.False(string.IsNullOrWhiteSpace(result.MachineArchitecture));
        Assert.False(string.IsNullOrWhiteSpace(result.RuntimeVersion));
        Assert.InRange(result.TimestampUtc, before, after);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GetDiagnostics_InvalidApplicationName_ThrowsArgumentException(
        string? applicationName)
    {
        Assert.ThrowsAny<ArgumentException>(
            () => _service.GetDiagnostics(applicationName!, "Test"));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void GetDiagnostics_InvalidEnvironmentName_ThrowsArgumentException(
        string? environmentName)
    {
        Assert.ThrowsAny<ArgumentException>(
            () => _service.GetDiagnostics(
                "BlazorExpo Engineering Suite",
                environmentName!));
    }
}
