using System.ComponentModel.DataAnnotations;

namespace BlazorCodeChallenge.Contracts.Mortgage;

/// <summary>
/// Represents the information required to calculate a mortgage.
/// </summary>
public sealed class MortgageCalculationRequest
{
    /// <summary>
    /// Gets the original principal borrowed from the lender.
    /// </summary>
    [Required]
    [Range(typeof(decimal), "0.01", "1000000000")]
    public decimal LoanAmount { get; init; }

    /// <summary>
    /// Gets the annual interest rate expressed as a percentage.
    /// For example, 6.25 represents 6.25%.
    /// </summary>
    [Range(typeof(decimal), "0.01", "100")]
    public decimal AnnualInterestRate { get; init; }

    /// <summary>
    /// Gets the mortgage term in years.
    /// </summary>
    [Range(1, 50)]
    public int TermYears { get; init; }

    /// <summary>
    /// Gets the optional additional principal paid each month.
    /// This amount is applied directly to the loan balance.
    /// </summary>
    public decimal ExtraMonthlyPrincipal { get; init; }
}