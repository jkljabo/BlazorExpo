using BlazorCodeChallenge.Contracts.Engineering;
using BlazorCodeChallenge.Core.Engineering;
using BlazorCodeChallenge.EnterpriseApi.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCodeChallenge.Engineering.Tests.Api;

public class EngineeringControllerTests
{
    [Fact]
    public void GetStatus_ReturnsOkWithEngineeringSuiteStatus()
    {
        // Arrange
        var service = new EngineeringSuiteService();
        var controller = new EngineeringController(service);

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
}
