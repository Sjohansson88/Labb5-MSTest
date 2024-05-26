using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetDynamosV2
{
    internal class ShowBalance
    {
        public static void ShowAccount(Customer loggedInCustomer, TextWriter writer)
        {
            writer.WriteLine("Choose an option:");
            writer.WriteLine("1. Show balance for a specific account");
            writer.WriteLine("2. Show balances for all accounts");

            int option = Validator.GetValidInt("Enter your choice: ", 1, 2);

            switch (option)
            {
                case 1:
                    ShowSpecificAccount(loggedInCustomer, writer);
                    break;
                case 2:
                    ShowAllAccounts(loggedInCustomer, writer);
                    break;
            }
            writer.WriteLine("Press enter to return to the menu.");
        }

        public static void ShowSpecificAccount(Customer loggedInCustomer, TextWriter writer)
        {
            writer.WriteLine("Choose an account to view the balance:");
            DisplayUserAccounts(loggedInCustomer, writer);
            int selectedAccountIndex = Validator.GetValidInt("Enter the account number: ", 1, loggedInCustomer.Accounts.Count) - 1;
            Account selectedAccount = loggedInCustomer.Accounts[selectedAccountIndex];

            // Uppdatering för att inkludera lånebelopp
            writer.WriteLine($"Balance for Account {selectedAccount.AccountNumber} ({selectedAccount.AccountName}): {selectedAccount.Balance} - Loanamount: {selectedAccount.LoanAmount}");
        }

        public static void ShowAllAccounts(Customer loggedInCustomer, TextWriter writer)
        {
            writer.WriteLine("Showing balances for all accounts:");
            foreach (Account account in loggedInCustomer.Accounts)
            {
                // Uppdatering för att inkludera lånebelopp
                writer.WriteLine($"Account {account.AccountNumber}: {account.AccountName} - {account.Currency} - Balance: {account.Balance} - Loanamount: {account.LoanAmount}");
            }
        }

        private static void DisplayUserAccounts(Customer loggedInCustomer, TextWriter writer)
        {
            for (int i = 0; i < loggedInCustomer.Accounts.Count; i++)
            {
                writer.WriteLine($"{i + 1}. Account {loggedInCustomer.Accounts[i].AccountNumber}: {loggedInCustomer.Accounts[i].AccountName} - {loggedInCustomer.Accounts[i].Currency}");
            }
        }
    }

    //internal class ShowBalance
    //{
    //    public static void ShowAccount(User loggedInUser)  // This is for User.This is almost same as ConvertManagement.ConvertMoney
    //    {
    //        if (loggedInUser is Customer customer)  // If the user does not have any account yet?
    //        {
    //            Console.WriteLine("Choose an option:");
    //            Console.WriteLine("1. Show balance for a specific account");
    //            Console.WriteLine("2. Show balances for all accounts");

    //            int option = Validator.GetValidInt("Enter your choice: ", 1, 2);

    //            switch (option)
    //            {
    //                case 1:
    //                    ShowSpecificAccount(customer);
    //                    break;
    //                case 2:
    //                    ShowAllAccounts(customer);
    //                    break;
    //            }
    //            Console.WriteLine("Press enter to return to the menu.");
    //            Console.ReadKey();
    //            Console.Clear();
    //        }
    //    }
    //    private static void ShowSpecificAccount(Customer customer)
    //    {
    //        Console.WriteLine("Choose an account to view the balance:");
    //        DisplayUserAccounts(customer);
    //        int selectedAccountIndex = Validator.GetValidInt("Enter the account number: ", 1, customer.Accounts.Count) - 1;
    //        Account selectedAccount = customer.Accounts[selectedAccountIndex];
    //        Console.WriteLine($"Balance for Account {selectedAccount.AccountNumber} ({selectedAccount.AccountName}): {selectedAccount.Balance}");

    //    }
    //    public static void ShowAllAccounts(Customer customer)
    //    {
    //        Console.WriteLine("Showing balances for all accounts:");
    //        foreach (Account account in customer.Accounts)
    //        {
    //            Console.WriteLine($"Account {account.AccountNumber}: {account.AccountName} - {account.Currency} - Balance: {account.Balance}");
    //        }
    //    }

    //    private static void DisplayUserAccounts(Customer customer)
    //    {
    //        for (int i = 0; i < customer.Accounts.Count; i++)
    //        {
    //            Console.WriteLine($"{i + 1}. Account {customer.Accounts[i].AccountNumber}: {customer.Accounts[i].AccountName} - {customer.Accounts[i].Currency}");
    //        }
    //    }

    //}
}
