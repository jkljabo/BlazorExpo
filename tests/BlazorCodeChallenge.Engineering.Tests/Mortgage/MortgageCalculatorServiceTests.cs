using BlazorCodeChallenge.Contracts.Mortgage;
using BlazorCodeChallenge.Core.Mortgage;
using Xunit;

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
}
