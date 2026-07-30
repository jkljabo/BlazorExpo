/// <summary>
/// Represents a single payment within an amortization schedule.
/// </summary>
public sealed class MortgagePaymentDetail
{
    /// <summary>
    /// Gets the sequential payment number.
    /// </summary>
    public int PaymentNumber { get; init; }

    /// <summary>
    /// Gets the total payment amount.
    /// </summary>
    public decimal Payment { get; init; }

    /// <summary>
    /// Gets the portion of the payment applied to the loan principal.
    /// </summary>
    public decimal Principal { get; init; }

    /// <summary>
    /// Gets the portion of the payment applied to interest.
    /// </summary>
    public decimal Interest { get; init; }

    /// <summary>
    /// Gets the remaining loan balance after the payment is applied.
    /// </summary>
    public decimal RemainingBalance { get; init; }
}
