using BlazorCodeChallenge.Contracts.Mortgage;
using BlazorCodeChallenge.Core.Mortgage;
using Microsoft.AspNetCore.Mvc;

namespace BlazorCodeChallenge.EnterpriseApi.Controllers;

/// <summary>
/// Provides mortgage calculation endpoints.
/// </summary>
[ApiController]
[Route("api/mortgage")]
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
    public MortgageController(
        IMortgageCalculatorService mortgageCalculatorService)
    {
        this.mortgageCalculatorService = mortgageCalculatorService;
    }

    /// <summary>
    /// Calculates payment information for a fixed-rate mortgage.
    /// </summary>
    /// <param name="request">
    /// The mortgage calculation request.
    /// </param>
    /// <returns>
    /// The calculated mortgage payment information.
    /// </returns>
    /// <response code="200">
    /// The mortgage calculation completed successfully.
    /// </response>
    /// <response code="400">
    /// The request failed validation.
    /// </response>
    [HttpPost("calculate")]
    [ProducesResponseType<MortgageCalculationResponse>(
        StatusCodes.Status200OK)]
    public ActionResult<MortgageCalculationResponse> Calculate(
        MortgageCalculationRequest request)
    {
        return Ok(
            mortgageCalculatorService.Calculate(request));
    }
}
