using BlazorCodeChallenge.Components.Pages;
using BlazorCodeChallenge.Models;

namespace BlazorCodeChallenge.Helpers
{
    public static class LoanUtils
    {
        /// <summary>
        /// Calculate the monthly payment schedule for a loan
        /// </summary>
        /// <param name="loan"></param>
        /// <returns>A loan opject</returns>
        public static Loan GetPayments(Loan loan)
        {
            loan.Payments.Clear();

            // Calculate a payment.
            loan.Payment = CalculatePayment(loan.PurchaseAmount, loan.Rate, loan.Term);

            var loanMonths = (loan.Term * 12);

            // variables to hold the total interest and balance.
            double balance = loan.PurchaseAmount;
            double totalInterest = 0;
            double monthlyPrincipal = 0;
            double monthlyInterest = 0;
            double monthlyRate = CalculateMonthlyRate(loan.Rate);

            for (int month = 1; month <= loanMonths; month++)
            {
                monthlyInterest = CalculateMonthlyInterest(balance, monthlyRate);
                totalInterest += monthlyInterest;
                monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                LoanPayment loanPayment = new LoanPayment();

                loanPayment.Month = month;
                loanPayment.Payment = loan.Payment;
                loanPayment.MonthlyPrincipal = monthlyPrincipal;
                loanPayment.MonthlyInterest = monthlyInterest;
                loanPayment.TotalInterest = totalInterest;
                loanPayment.Balance = balance < 0 ? 0 : balance;

                // Add the payment to the list
                loan.Payments.Add(loanPayment);
            }
            loan.TotalInterest = totalInterest;
            loan.TotalCost = loan.PurchaseAmount + totalInterest;

            return loan;
        }
        
        /// <summary>
        /// Calculates a payment for a simple interest loan
        /// </summary>
        /// <param name="amount">Loan Amount</param>
        /// <param name="rate">Anualized Rate as a double</param>
        /// <param name="term">Term in years</param>
        /// <returns>A monthly payment as a double</returns>
        public static double CalculatePayment(double amount, double rate, double term)
        {
            var monthlyRate = CalculateMonthlyRate(rate);
            var months = term * 12;
            var payment = (amount * monthlyRate) / (1 - Math.Pow(1 + monthlyRate, -months));
            return payment;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rate"></param>
        /// <returns></returns>
        public static double CalculateMonthlyRate(double rate)
        {
            return rate / 1200;
        }

        public static double CalculateMonthlyInterest(double balance, double monthlyRate)
        {
            return balance * monthlyRate;
        }
    }
}
