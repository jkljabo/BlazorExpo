using BlazorCodeChallenge.Contracts.Mortgage;

namespace BlazorCodeChallenge.Core.Mortgage;

/// <summary>
/// Defines operations for calculating mortgage payment information.
/// </summary>
public interface IMortgageCalculatorService
{
    /// <summary>
    /// Calculates payment information for a fixed-rate mortgage.
    /// </summary>
    /// <param name="request">
    /// The mortgage calculation request.
    /// </param>
    /// <returns>
    /// The calculated mortgage payment information.
    /// </returns>
    MortgageCalculationResponse Calculate(
        MortgageCalculationRequest request);
}