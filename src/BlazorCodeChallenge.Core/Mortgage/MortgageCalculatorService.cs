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

        var paymentSchedule = BuildPaymentSchedule(
            request.LoanAmount, Math.Round((decimal)monthlyPayment, 2),
            request.AnnualInterestRate, request.TermYears);

        return new MortgageCalculationResponse
        {
            MonthlyPayment =
                Math.Round((decimal)monthlyPayment, 2),

            TotalInterest =
                Math.Round((decimal)totalInterest, 2),

            TotalPaid =
                Math.Round((decimal)totalPaid, 2),

            PaymentSchedule = paymentSchedule
        };
    }

    private static IReadOnlyList<MortgagePaymentDetail> BuildPaymentSchedule(
        decimal loanAmount, decimal monthlyPayment, decimal annualInterestRate, int termYears)
    {
        int numberOfPayments = termYears * 12;
        decimal monthlyRate = annualInterestRate / 100m / 12m;

        var schedule = new List<MortgagePaymentDetail>(numberOfPayments);

        decimal remainingBalance = loanAmount;
        decimal totalRoundedPrincipal = 0m;

        for (int paymentNumber = 1; paymentNumber <= numberOfPayments; paymentNumber++)
        {
            decimal interest = remainingBalance * monthlyRate;
            decimal principal = monthlyPayment - interest;

            if (paymentNumber == numberOfPayments)
            {
                principal = remainingBalance;
                remainingBalance = 0m;
            }
            else
            {
                remainingBalance -= principal;
            }

            decimal roundedInterest = Math.Round(interest, 2);
            decimal roundedPrincipal = Math.Round(principal, 2);

            // Reconcile the displayed schedule on the final payment.
            if (paymentNumber == numberOfPayments)
            {
                roundedPrincipal = loanAmount - totalRoundedPrincipal;
            }

            totalRoundedPrincipal += roundedPrincipal;

            schedule.Add(new MortgagePaymentDetail
            {
                PaymentNumber = paymentNumber,
                Payment = Math.Round(monthlyPayment, 2),
                Interest = roundedInterest,
                Principal = roundedPrincipal,
                RemainingBalance = Math.Round(remainingBalance, 2)
            });
        }

        return schedule;
    }
}
