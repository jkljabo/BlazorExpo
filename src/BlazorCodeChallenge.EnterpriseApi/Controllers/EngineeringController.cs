using BlazorCodeChallenge.Contracts.Engineering;
using BlazorCodeChallenge.Core.Engineering;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCodeChallenge.EnterpriseApi.Controllers;

[ApiController]
[Route("api/engineering")]
public sealed class EngineeringController : ControllerBase
{
    private readonly IEngineeringSuiteService engineeringSuiteService;

    public EngineeringController(
        IEngineeringSuiteService engineeringSuiteService)
    {
        this.engineeringSuiteService = engineeringSuiteService;
    }

    [HttpGet("status")]
    [ProducesResponseType<EngineeringSuiteStatusResponse>(
        StatusCodes.Status200OK)]
    public ActionResult<EngineeringSuiteStatusResponse> GetStatus()
    {
        return Ok(engineeringSuiteService.GetStatus());
    }
}
