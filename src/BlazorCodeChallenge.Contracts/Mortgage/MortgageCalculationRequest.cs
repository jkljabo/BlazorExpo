using System.ComponentModel.DataAnnotations;

namespace BlazorCodeChallenge.Contracts.Mortgage;

/// <summary>
/// Represents the information required to calculate a fixed-rate mortgage.
/// </summary>
public sealed class MortgageCalculationRequest
{
    /// <summary>
    /// Gets the principal amount being financed.
    /// </summary>
    [Required]
    [Range(typeof(decimal), "0.01", "1000000000")]
    public decimal LoanAmount { get; init; }

    /// <summary>
    /// Gets the annual interest rate expressed as a percentage.
    /// </summary>
    [Range(typeof(decimal), "0.01", "100")]
    public decimal AnnualInterestRate { get; init; }

    /// <summary>
    /// Gets the loan term in years.
    /// </summary>
    [Range(1, 50)]
    public int TermYears { get; init; }

    /// <summary>
    /// Gets the extra principal paid exceeding the expected monthly principal.
    /// </summary>
    public decimal ExtraMonthlyPrincipal { get; init; }
}