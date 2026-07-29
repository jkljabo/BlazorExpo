using BlazorCodeChallenge.Contracts.Diagnostics;
using BlazorCodeChallenge.Contracts.Engineering;
using BlazorCodeChallenge.Core.Diagnostics;
using BlazorCodeChallenge.Core.Engineering;
using BlazorCodeChallenge.EnterpriseApi.Controllers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace BlazorCodeChallenge.Engineering.Tests.Api;

public class EngineeringControllerTests
{
    [Fact]
    public void GetStatus_ReturnsOkWithEngineeringSuiteStatus()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.GetStatus();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var response =
            Assert.IsType<EngineeringSuiteStatusResponse>(
                okResult.Value);

        Assert.Equal("BlazorExpo Engineering Suite", response.Name);
        Assert.Equal("Operational", response.Status);
    }

    [Fact]
    public void GetDiagnostics_ReturnsOkWithRuntimeDiagnostics()
    {
        // Arrange
        var controller = CreateController();

        // Act
        var result = controller.GetDiagnostics();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);

        var response =
            Assert.IsType<RuntimeDiagnosticsResponse>(
                okResult.Value);

        Assert.Equal(
            "BlazorCodeChallenge.EnterpriseApi",
            response.ApplicationName);

        Assert.Equal(
            "Testing",
            response.EnvironmentName);
    }

    private static EngineeringController CreateController()
    {
        return new EngineeringController(
            new EngineeringSuiteService(),
            new TestRuntimeDiagnosticsService(),
            new TestWebHostEnvironment());
    }

    private sealed class TestRuntimeDiagnosticsService
        : IRuntimeDiagnosticsService
    {
        public RuntimeDiagnosticsResponse GetDiagnostics(
            string applicationName,
            string environmentName)
        {
            return new RuntimeDiagnosticsResponse
            {
                ApplicationName = applicationName,
                EnvironmentName = environmentName,
                FrameworkDescription = "Test Framework",
                OperatingSystemDescription = "Test Operating System",
                ProcessArchitecture = "Test Process Architecture",
                MachineArchitecture = "Test Machine Architecture",
                RuntimeVersion = "Test Runtime",
                TimestampUtc = DateTimeOffset.UtcNow
            };
        }
    }

    private sealed class TestWebHostEnvironment : IWebHostEnvironment
    {
        public string ApplicationName { get; set; } =
            "BlazorCodeChallenge.EnterpriseApi";

        public IFileProvider WebRootFileProvider { get; set; } =
            new NullFileProvider();

        public string WebRootPath { get; set; } = string.Empty;

        public string EnvironmentName { get; set; } = "Testing";

        public string ContentRootPath { get; set; } = string.Empty;

        public IFileProvider ContentRootFileProvider { get; set; } =
            new NullFileProvider();
    }
}
