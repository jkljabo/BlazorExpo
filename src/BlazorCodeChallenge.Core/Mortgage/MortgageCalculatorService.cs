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
    public MortgageCalculationResponse Calculate(MortgageCalculationRequest request)
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

        var monthlyPaymentDecimal = (decimal)monthlyPayment;

        var schedule = BuildPaymentSchedule(
            request.LoanAmount, monthlyPaymentDecimal,
            request.AnnualInterestRate, request.ExtraMonthlyPrincipal);

        return new MortgageCalculationResponse
        {
            MonthlyPayment =
                Math.Round((decimal)monthlyPayment, 2),

            TotalInterest =
                Math.Round((decimal)totalInterest, 2),

            TotalPaid =
                Math.Round((decimal)totalPaid, 2),

            PaymentSchedule = schedule.PaymentSchedule,

            ActualNumberOfPayments = schedule.ActualNumberOfPayments,

            MonthsSaved = request.TermYears * 12 - schedule.ActualNumberOfPayments,

            InterestSaved = Math.Round((decimal)totalInterest, 2) - schedule.TotalInterestPaid
        };
    }

    private static MortgageScheduleBuilder BuildPaymentSchedule(
        decimal loanAmount, decimal monthlyPayment,
        decimal annualInterestRate, decimal extraMonthlyPrincipal)
    {
        int paymentNumber = 0;
        decimal remainingBalance = loanAmount;
        decimal monthlyRate = annualInterestRate / 100m / 12m;

        var schedule = new MortgageScheduleBuilder(loanAmount);

        while (remainingBalance > 0.01m)
        {
            paymentNumber++;

            // New financial algorithm
            // While loan not paid

            //  var interest = CalculateInterest(...);
            var interest = remainingBalance * monthlyRate;

            //  var principal = CalculatePrincipal(...);
            var principal = monthlyPayment - interest;

            //  principal = ApplyExtraPrincipal(...);
            principal += extraMonthlyPrincipal;

            //  principal = CapPrincipal(...);
            if (principal > remainingBalance)
            {
                principal = remainingBalance;
            }

            //  remainingBalance = UpdateBalance(...);
            remainingBalance -= principal;

            bool isFinalPayment = remainingBalance <= 0.01m;

            //  schedule.Add(...);
            schedule.Add(paymentNumber, (principal + interest), principal,
                interest, remainingBalance, isFinalPayment);

            // End

        }

        return schedule;
    }

    private sealed class MortgageScheduleBuilder
    {
        private readonly decimal _originalLoanAmount;
        private readonly List<MortgagePaymentDetail> _payments = [];
        private decimal _displayedPrincipalTotal;

        public IReadOnlyList<MortgagePaymentDetail> PaymentSchedule => _payments;

        public int ActualNumberOfPayments => _payments.Count;

        public decimal TotalInterestPaid { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalLoanAmount"></param>
        public MortgageScheduleBuilder(decimal originalLoanAmount)
        {
            _originalLoanAmount = originalLoanAmount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="paymentNumber"></param>
        /// <param name="payment"></param>
        /// <param name="principal"></param>
        /// <param name="interest"></param>
        /// <param name="remainingBalance"></param>
        /// <param name="isFinalPayment"></param>
        public void Add(int paymentNumber, decimal payment, decimal principal,
            decimal interest, decimal remainingBalance, bool isFinalPayment)
        {
            decimal roundedPrincipal;

            if (isFinalPayment)
            {
                roundedPrincipal = _originalLoanAmount - _displayedPrincipalTotal;
            }
            else
            {
                roundedPrincipal = Math.Round(principal, 2);
            }

            _displayedPrincipalTotal += roundedPrincipal;

            _payments.Add(new MortgagePaymentDetail
            {
                PaymentNumber = paymentNumber,
                Payment = Math.Round(payment, 2),
                Principal = roundedPrincipal,
                Interest = Math.Round(interest, 2),
                RemainingBalance = Math.Round(remainingBalance, 2)
            });
            TotalInterestPaid += interest;
        }
    }
}
