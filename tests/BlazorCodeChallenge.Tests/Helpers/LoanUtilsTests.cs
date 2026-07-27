using BlazorCodeChallenge.Helpers;
using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Tests.Helpers;

public class LoanUtilsTests
{
    [Fact]
    public void CalculateMonthlyRate_ReturnsMonthlyDecimalRate()
    {
        // Act
        var result = LoanUtils.CalculateMonthlyRate(6.0);

        // Assert
        Assert.Equal(0.005, result, 10);
    }

    [Fact]
    public void CalculateMonthlyInterest_ReturnsInterestForBalance()
    {
        // Arrange
        const double balance = 100000;
        const double monthlyRate = 0.005;

        // Act
        var result =
            LoanUtils.CalculateMonthlyInterest(balance, monthlyRate);

        // Assert
        Assert.Equal(500, result, 10);
    }

    [Fact]
    public void CalculatePayment_ReturnsExpectedMonthlyPayment()
    {
        // Act
        var result =
            LoanUtils.CalculatePayment(
                amount: 25000,
                rate: 5.0,
                term: 5);

        // Assert
        Assert.Equal(471.78, result, 2);
    }

    [Fact]
    public void GetPayments_CreatesPaymentForEveryLoanMonth()
    {
        // Arrange
        var loan = new Loan
        {
            PurchaseAmount = 25000,
            Rate = 5.0,
            Term = 5
        };

        // Act
        var result = LoanUtils.GetPayments(loan);

        // Assert
        Assert.Equal(60, result.Payments.Count);
        Assert.Equal(1, result.Payments.First().Month);
        Assert.Equal(60, result.Payments.Last().Month);
    }

    [Fact]
    public void GetPayments_CalculatesLoanTotals()
    {
        // Arrange
        var loan = new Loan
        {
            PurchaseAmount = 25000,
            Rate = 5.0,
            Term = 5
        };

        // Act
        var result = LoanUtils.GetPayments(loan);

        // Assert
        Assert.True(result.Payment > 0);
        Assert.True(result.TotalInterest > 0);

        Assert.Equal(
            result.PurchaseAmount + result.TotalInterest,
            result.TotalCost,
            10);

        Assert.Equal(0, result.Payments.Last().Balance, 10);
    }

    [Fact]
    public void GetPayments_WhenRecalculated_ReplacesPaymentSchedule()
    {
        // Arrange
        var loan = new Loan
        {
            PurchaseAmount = 25000,
            Rate = 5.0,
            Term = 5
        };

        LoanUtils.GetPayments(loan);

        // Act
        var result = LoanUtils.GetPayments(loan);

        // Assert
        Assert.Equal(60, result.Payments.Count);
    }

    [Fact]
    public void CalculatePayment_WithZeroInterest_ReturnsPrincipalDividedByMonths()
    {
        // Arrange
        const double amount = 12000;
        const double rate = 0;
        const double term = 1;

        // Act
        var result =
            LoanUtils.CalculatePayment(amount, rate, term);

        // Assert
        Assert.Equal(1000, result, 10);
    }

    [Fact]
    public void GetPayments_WithZeroInterest_CreatesPrincipalOnlySchedule()
    {
        // Arrange
        var loan = new Loan
        {
            PurchaseAmount = 12000,
            Rate = 0,
            Term = 1
        };

        // Act
        var result = LoanUtils.GetPayments(loan);

        // Assert
        Assert.Equal(1000, result.Payment, 10);
        Assert.Equal(0, result.TotalInterest, 10);
        Assert.Equal(12000, result.TotalCost, 10);
        Assert.Equal(12, result.Payments.Count);

        Assert.All(
            result.Payments,
            payment =>
            {
                Assert.Equal(1000, payment.Payment, 10);
                Assert.Equal(1000, payment.MonthlyPrincipal, 10);
                Assert.Equal(0, payment.MonthlyInterest, 10);
            });

        Assert.Equal(0, result.Payments.Last().Balance, 10);
    }

    [Fact]
    public void GetPayments_WithInterest_FullyAmortizesLoan()
    {
        // Arrange
        var loan = new Loan
        {
            PurchaseAmount = 25000,
            Rate = 5.0,
            Term = 5
        };

        // Act
        var result = LoanUtils.GetPayments(loan);

        // Assert
        var finalPayment = result.Payments.Last();

        Assert.Equal(60, finalPayment.Month);
        Assert.Equal(0, finalPayment.Balance, 10);

        var totalPrincipal =
            result.Payments.Sum(payment => payment.MonthlyPrincipal);

        Assert.Equal(
            loan.PurchaseAmount,
            totalPrincipal,
            8);

        Assert.Equal(
            result.TotalInterest,
            result.Payments.Sum(payment => payment.MonthlyInterest),
            8);
    }
}
