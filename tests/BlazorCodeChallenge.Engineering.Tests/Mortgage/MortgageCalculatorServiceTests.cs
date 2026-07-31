using BlazorCodeChallenge.Contracts.Mortgage;
using BlazorCodeChallenge.Core.Mortgage;
using Xunit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlazorCodeChallenge.Engineering.Tests.Mortgage;

public sealed class MortgageCalculatorServiceTests
{
    private readonly MortgageCalculatorService service = new();

    [Fact]
    public void Calculate_ThirtyYearMortgage_ReturnsExpectedValues()
    {
        var request = new MortgageCalculationRequest
        {
            LoanAmount = 350000m,
            AnnualInterestRate = 6.25m,
            TermYears = 30
        };

        var result = service.Calculate(request);

        Assert.Equal(2155.01m, result.MonthlyPayment);
        Assert.Equal(425803.67m, result.TotalInterest);
        Assert.Equal(775803.67m, result.TotalPaid);
    }

    [Fact]
    public void Calculate_FifteenYearMortgage_ReturnsExpectedValues()
    {
        var request = new MortgageCalculationRequest
        {
            LoanAmount = 350000m,
            AnnualInterestRate = 6.25m,
            TermYears = 15
        };

        var result = service.Calculate(request);

        Assert.True(result.MonthlyPayment > 0);
        Assert.True(result.TotalInterest > 0);
        Assert.True(result.TotalPaid > 0);

        Assert.True(result.MonthlyPayment > 2900m);
        Assert.True(result.MonthlyPayment < 3100m);
    }

    [Fact]
    public void Calculate_Throws_WhenLoanAmountIsZero()
    {
        var request = new MortgageCalculationRequest
        {
            LoanAmount = 0m,
            AnnualInterestRate = 6.25m,
            TermYears = 30
        };

        Assert.Throws<ArgumentOutOfRangeException>(
            () => service.Calculate(request));
    }

    [Fact]
    public void Calculate_Throws_WhenInterestRateIsZero()
    {
        var request = new MortgageCalculationRequest
        {
            LoanAmount = 350000m,
            AnnualInterestRate = 0m,
            TermYears = 30
        };

        Assert.Throws<ArgumentOutOfRangeException>(
            () => service.Calculate(request));
    }

    [Fact]
    public void Calculate_Throws_WhenTermYearsIsZero()
    {
        var request = new MortgageCalculationRequest
        {
            LoanAmount = 350000m,
            AnnualInterestRate = 6.25m,
            TermYears = 0
        };

        Assert.Throws<ArgumentOutOfRangeException>(
            () => service.Calculate(request));
    }


    [Fact]
    public void Calculate_Returns360Payments_ForThirtyYearMortgage()
    {
        var request = new MortgageCalculationRequest
        {
            LoanAmount = 350000m,
            AnnualInterestRate = 6.25m,
            TermYears = 30
        };

        var result = service.Calculate(request);
        Assert.Equal(360, result.PaymentSchedule.Count);
    }

    [Fact]
    public void Calculate_FirstPayment_HasExpectedValues()
    {
        var request = new MortgageCalculationRequest
        {
            LoanAmount = 350000m,
            AnnualInterestRate = 6.25m,
            TermYears = 30
        };

        var result = service.Calculate(request);

        var payment = result.PaymentSchedule.First();

        Assert.Equal(1, payment.PaymentNumber);

        Assert.True(payment.Payment > 0);

        Assert.True(payment.Principal > 0);

        Assert.True(payment.Interest > 0);

        Assert.True(payment.RemainingBalance < request.LoanAmount);
    }

    [Fact]
    public void Calculate_LastPayment_PaysOffLoan()
    {
        var request = new MortgageCalculationRequest
        {
            LoanAmount = 350000m,
            AnnualInterestRate = 6.25m,
            TermYears = 30
        };

        var result = service.Calculate(request);

        var finalPayment = result.PaymentSchedule.Last();

        var final = result.PaymentSchedule.Last();

        Assert.Equal(360, finalPayment.PaymentNumber);

        Assert.True(finalPayment.RemainingBalance <= 0.01m);
    }

    [Fact]
    public void Calculate_TotalPrincipal_EqualsOriginalLoan()
    {
        var request = new MortgageCalculationRequest
        {
            LoanAmount = 350000m,
            AnnualInterestRate = 6.25m,
            TermYears = 30
        };

        var result = service.Calculate(request);

        var totalPrincipal = result.PaymentSchedule.Sum(x => x.Principal);

        Assert.Equal(request.LoanAmount, Math.Round(totalPrincipal, 2));
    }

    [Fact]
    public void Calculate_ExtraPrincipal_ReducesNumberOfPayments()
    {
        var request = new MortgageCalculationRequest
        {
            LoanAmount = 350000m,
            AnnualInterestRate = 6.25m,
            TermYears = 30,
            ExtraMonthlyPrincipal = 200m
        };

        var result = service.Calculate(request);

        Assert.True(result.ActualNumberOfPayments < 360);
        Assert.Equal(360 - result.ActualNumberOfPayments, result.MonthsSaved);
    }
}
