namespace BlazorCodeChallenge.Contracts.Mortgage;

/// <summary>
/// Represents the calculated results of a mortgage calculation.
/// </summary>
public sealed class MortgageCalculationResponse
{
    /// <summary>
    /// Gets the calculated monthly mortgage payment.
    /// </summary>
    public decimal MonthlyPayment { get; init; }

    /// <summary>
    /// Gets the total interest paid over the life of the loan.
    /// </summary>
    public decimal TotalInterest { get; init; }

    /// <summary>
    /// Gets the total amount repaid over the life of the loan.
    /// </summary>
    public decimal TotalPaid { get; init; }

    /// <summary>
    /// Gets the amortization schedule for the loan.
    /// </summary>
    public IReadOnlyList<MortgagePaymentDetail> PaymentSchedule { get; init; }
        = [];

    /// <summary>
    /// Gets the actual number of payments made over the life of the loan.
    /// </summary>
    public int ActualNumberOfPayments { get; init; }

    /// <summary>
    /// Gets the number of months saved by extra monthly principal paid.
    /// </summary>
    public int MonthsSaved { get; init; }

    /// <summary>
    /// Gets the amount of interest saved by extra monthly principal paid.
    /// </summary>
    public decimal InterestSaved { get; init; }
}