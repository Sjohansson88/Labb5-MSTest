using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal class LoanManager
    {
        public static string RequestPersonalLoan(Customer customer, decimal loanAmount, int selectedAccountIndex, Func<decimal> getLoanInterestRate)
        {
            decimal originalBalance = CalculateOriginalBalance(customer);
            decimal totalLoanAmount = CalculateTotalLoanAmount(customer);

            decimal remainingBalance = originalBalance * 5 - totalLoanAmount;

            if (loanAmount > remainingBalance)
            {
                return "Loan request denied. The personal loan amount exceeds the limit (5 times the original balance excluding already borrowed money).";
            }

            decimal interestRate = getLoanInterestRate();
            Account selectedAccount = customer.Accounts[selectedAccountIndex];

            selectedAccount.Balance += loanAmount;
            selectedAccount.LoanAmount += loanAmount;

            string message = $"Personal loan request approved. The loan amount of {loanAmount.ToString("C", new CultureInfo("sv-SE"))} has been added to account {selectedAccount.AccountNumber}.\n" +
                             $"The loan will have an interest rate of: {interestRate.ToString("P", new CultureInfo("sv-SE"))}\n" +
                             $"You will need to repay: {CalculateRepaymentAmount(loanAmount, interestRate).ToString("C", new CultureInfo("sv-SE"))}";

            return message;
        }

        public static decimal CalculateRepaymentAmount(decimal loanAmount, decimal interestRate)
        {
            return loanAmount * (1 + interestRate);
        }

        public static decimal CalculateOriginalBalance(Customer customer)
        {
            return customer.Accounts.Sum(account => account.Balance - account.LoanAmount);
        }

        public static decimal CalculateTotalLoanAmount(Customer customer)
        {
            return customer.Accounts.Sum(account => account.LoanAmount);
        }

        public static void DisplayUserAccounts(Customer customer, StringWriter writer)
        {
            for (int i = 0; i < customer.Accounts.Count; i++)
            {
                writer.WriteLine($"{i + 1}. Account {customer.Accounts[i].AccountNumber}: {customer.Accounts[i].AccountName} - {customer.Accounts[i].Currency}");
            }
        }

    }
}

