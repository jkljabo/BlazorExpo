using BlazorCodeChallenge.Contracts.Diagnostics;
using BlazorCodeChallenge.Contracts.Engineering;
using BlazorCodeChallenge.Core.Diagnostics;
using BlazorCodeChallenge.Core.Engineering;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCodeChallenge.EnterpriseApi.Controllers;

/// <summary>
/// Provides engineering status and runtime diagnostic endpoints.
/// </summary>
[ApiController]
[Route("api/engineering")]
[ApiExplorerSettings(GroupName = "Engineering")]
public sealed class EngineeringController : ControllerBase
{
    private readonly IEngineeringSuiteService engineeringSuiteService;
    private readonly IRuntimeDiagnosticsService runtimeDiagnosticsService;
    private readonly IWebHostEnvironment environment;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="EngineeringController"/> class.
    /// </summary>
    /// <param name="engineeringSuiteService">
    /// The service that provides engineering status information.
    /// </param>
    /// <param name="runtimeDiagnosticsService">
    /// The service that provides runtime diagnostic information.
    /// </param>
    /// <param name="environment">
    /// The current hosting environment.
    /// </param>
    public EngineeringController(
        IEngineeringSuiteService engineeringSuiteService,
        IRuntimeDiagnosticsService runtimeDiagnosticsService,
        IWebHostEnvironment environment)
    {
        this.engineeringSuiteService = engineeringSuiteService;
        this.runtimeDiagnosticsService = runtimeDiagnosticsService;
        this.environment = environment;
    }

    /// <summary>
    /// Retrieves the operational status of the engineering services.
    /// </summary>
    /// <returns>
    /// The current operational status of the engineering services.
    /// </returns>
    [HttpGet("status")]
    [ProducesResponseType<EngineeringSuiteStatusResponse>(
        StatusCodes.Status200OK)]
    public ActionResult<EngineeringSuiteStatusResponse> GetStatus()
    {
        return Ok(engineeringSuiteService.GetStatus());
    }

    /// <summary>
    /// Retrieves the runtime diagnostic information for the current application.
    /// </summary>
    /// <returns>
    /// The runtime diagnostic information for the current application.
    /// </returns>
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
