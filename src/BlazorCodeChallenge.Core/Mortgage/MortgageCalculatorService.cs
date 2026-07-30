using BlazorCodeChallenge.Contracts.Mortgage;

namespace BlazorCodeChallenge.Core.Mortgage;

/// <summary>
/// Provides the default implementation of
/// <see cref="IMortgageCalculatorService"/>.
/// </summary>
public sealed class MortgageCalculatorService
    : IMortgageCalculatorService
{
    /// <inheritdoc />
    public MortgageCalculationResponse Calculate(
        MortgageCalculationRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            request.LoanAmount);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            request.AnnualInterestRate);

        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(
            request.TermYears);

        var principal = (double)request.LoanAmount;
        var monthlyRate =
            ((double)request.AnnualInterestRate / 100d) / 12d;

        var numberOfPayments =
            request.TermYears * 12;

        var factor =
            Math.Pow(1 + monthlyRate, numberOfPayments);

        var monthlyPayment =
            principal *
            monthlyRate *
            factor /
            (factor - 1);

        var totalPaid =
            monthlyPayment * numberOfPayments;

        var totalInterest =
            totalPaid - principal;

        return new MortgageCalculationResponse
        {
            MonthlyPayment =
                Math.Round((decimal)monthlyPayment, 2),

            TotalInterest =
                Math.Round((decimal)totalInterest, 2),

            TotalPaid =
                Math.Round((decimal)totalPaid, 2)
        };
    }
}
