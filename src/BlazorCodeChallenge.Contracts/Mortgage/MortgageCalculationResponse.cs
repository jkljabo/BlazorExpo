namespace BlazorCodeChallenge.Contracts.Mortgage;

/// <summary>
/// Represents the results of a mortgage calculation.
/// </summary>
public sealed class MortgageCalculationResponse
{
    /// <summary>
    /// Gets the required monthly payment.
    /// </summary>
    public decimal MonthlyPayment { get; init; }

    /// <summary>
    /// Gets the total interest paid over the life of the loan.
    /// </summary>
    public decimal TotalInterest { get; init; }

    /// <summary>
    /// Gets the total amount paid over the life of the loan.
    /// </summary>
    public decimal TotalPaid { get; init; }

    /// <summary>
    /// Gets the number of payments required to pay off the loan.
    /// </summary>
    public int ActualNumberOfPayments { get; init; }

    /// <summary>
    /// Gets the number of monthly payments saved through extra principal payments.
    /// </summary>
    public int MonthsSaved { get; init; }

    /// <summary>
    /// Gets the total interest saved through extra principal payments.
    /// </summary>
    public decimal InterestSaved { get; init; }

    /// <summary>
    /// Gets the detailed amortization schedule.
    /// </summary>
    public IReadOnlyList<MortgagePaymentDetail> PaymentSchedule { get; init; }
        = [];
}