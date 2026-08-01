using BlazorCodeChallenge.Contracts.Mortgage;
using BlazorCodeChallenge.Core.Mortgage;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCodeChallenge.EnterpriseApi.Controllers;

/// <summary>
/// Provides mortgage calculation endpoints.
/// </summary>
[ApiController]
[Route("api/mortgage")]
[ApiExplorerSettings(GroupName = "Mortgage")]
public sealed class MortgageController : ControllerBase
{
    private readonly IMortgageCalculatorService mortgageCalculatorService;

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="MortgageController"/> class.
    /// </summary>
    /// <param name="mortgageCalculatorService">
    /// The service used to perform mortgage calculations.
    /// </param>
    public MortgageController(IMortgageCalculatorService mortgageCalculatorService)
    {
        this.mortgageCalculatorService = mortgageCalculatorService;
    }

    /// <summary>
    /// Calculates an amortization schedule for a fixed-rate mortgage.
    /// </summary>
    /// <param name="request">
    /// The mortgage calculation request.
    /// </param>
    /// <returns>
    /// The calculated mortgage details and amortization schedule.
    /// </returns>
    /// <response code="200">
    /// The mortgage calculation completed successfully.
    /// </response>
    /// <response code="400">
    /// The request failed validation.
    /// </response>
    [HttpPost("calculate")]
    [ProducesResponseType<MortgageCalculationResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public ActionResult<MortgageCalculationResponse> Calculate(
        [FromBody] MortgageCalculationRequest request)
    {
        return Ok(
            mortgageCalculatorService.Calculate(request));
    }
}
