using BlazorCodeChallenge.Contracts.Diagnostics;
using BlazorCodeChallenge.Contracts.Engineering;
using BlazorCodeChallenge.Core.Diagnostics;
using BlazorCodeChallenge.Core.Engineering;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCodeChallenge.EnterpriseApi.Controllers;

[ApiController]
[Route("api/engineering")]
public sealed class EngineeringController : ControllerBase
{
    private readonly IEngineeringSuiteService engineeringSuiteService;
    private readonly IRuntimeDiagnosticsService runtimeDiagnosticsService;
    private readonly IWebHostEnvironment environment;

    public EngineeringController(
        IEngineeringSuiteService engineeringSuiteService,
        IRuntimeDiagnosticsService runtimeDiagnosticsService,
        IWebHostEnvironment environment)
    {
        this.engineeringSuiteService = engineeringSuiteService;
        this.runtimeDiagnosticsService = runtimeDiagnosticsService;
        this.environment = environment;
    }

    [HttpGet("status")]
    [ProducesResponseType<EngineeringSuiteStatusResponse>(
        StatusCodes.Status200OK)]
    public ActionResult<EngineeringSuiteStatusResponse> GetStatus()
    {
        return Ok(engineeringSuiteService.GetStatus());
    }

    [HttpGet("diagnostics")]
    [ProducesResponseType<RuntimeDiagnosticsResponse>(
        StatusCodes.Status200OK)]
    public ActionResult<RuntimeDiagnosticsResponse> GetDiagnostics()
    {
        return Ok(
            runtimeDiagnosticsService.GetDiagnostics(
                environment.ApplicationName,
                environment.EnvironmentName));
    }
}
