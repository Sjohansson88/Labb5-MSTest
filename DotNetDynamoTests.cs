using System.Globalization;
using System.Security.Principal;
using DotNetDynamosV2;

namespace TestProject1
{
    [TestClass]
    public class DotNetDynamoTests
    {
        [TestMethod]
        [TestCategory("LoanRequest")]
        public void RequestPersonalLoan_ValidLoan_Approved()
        {
            // Arrange
            var customer = new Customer
            {
                UserName = "User2",
                IDNumber = 5002,
                FirstName = "Anna",
                LastName = "Andersson",
                PassWord = "Password2!",
                Birthday = "1988-01-01",
                Accounts = new List<Account>
        {
            new Account(12344556, "MainAccount", "SEK", 2345M, 1)
        }
            };

            decimal loanAmount = 1000;
            int selectedAccountIndex = 0;
            Func<decimal> getLoanInterestRate = () => 0.05m;

            // Act
            var result = LoanManager.RequestPersonalLoan(customer, loanAmount, selectedAccountIndex, getLoanInterestRate);

            // Assert
            
           
            Assert.AreEqual(3345M, customer.Accounts[selectedAccountIndex].Balance);
            Assert.AreEqual(1000M, customer.Accounts[selectedAccountIndex].LoanAmount);
        }

        [TestMethod]
        [TestCategory("LoanRequest")]
        public void RequestPersonalLoan_LoanExceedsLimit_Denied()
        {
            // Arrange
            var customer = new Customer
            {
                UserName = "User2",
                IDNumber = 5002,
                FirstName = "Anna",
                LastName = "Andersson",
                PassWord = "Password2!",
                Birthday = "1988-01-01",
                Accounts = new System.Collections.Generic.List<Account>
                {
                    new Account(12344556, "MainAccount", "SEK", 2345M, 1),
                    new Account(23455678, "SavingAccount", "EUR", 2345M, 2)
                }
            };

            decimal loanAmount = 20000;
            int selectedAccountIndex = 0;
            Func<decimal> getLoanInterestRate = () => 0.05m;

            // Act
            var result = LoanManager.RequestPersonalLoan(customer, loanAmount, selectedAccountIndex, getLoanInterestRate);

            // Assert
           
            
            Assert.AreEqual(2345M, customer.Accounts[selectedAccountIndex].Balance);
            Assert.AreEqual(0M, customer.Accounts[selectedAccountIndex].LoanAmount);
           
        }

        









        [TestClass]
        [TestCategory("ShowBalance")]
        public class ShowBalanceTests
        {
            [TestMethod]
            public void ShowAllAccounts_PrintsAllAccounts()
            {
                // Arrange
                var customer = new Customer
                {
                    UserName = "User2",
                    IDNumber = 5002,
                    FirstName = "Anna",
                    LastName = "Andersson",
                    PassWord = "Password2!",
                    Birthday = "1988-01-01",
                    Accounts = new System.Collections.Generic.List<Account>
                {
                    new Account(12344556, "MainAccount", "SEK", 2345M, 1),
                    new Account(23455678, "SavingAccount", "EUR", 2345M, 2)
                }
                };

                var writer = new StringWriter();

                // Act
                ShowBalance.ShowAllAccounts(customer, writer);

                // Assert
                var output = writer.ToString();
                var expectedOutput = "Showing balances for all accounts:\r\n" +
                                     "Account 12344556: MainAccount - SEK - Balance: 2345 - Loanamount: 0\r\n" +
                                     "Account 23455678: SavingAccount - EUR - Balance: 2345 - Loanamount: 0\r\n";
                Assert.AreEqual(expectedOutput, output);
            }

            [TestMethod]
            [TestCategory("ShowBalance")]
            public void ShowSpecificAccount_PrintsSelectedAccountBalance()
            {
                // Arrange
                var customer = new Customer
                {
                    UserName = "User2",
                    IDNumber = 5002,
                    FirstName = "Anna",
                    LastName = "Andersson",
                    PassWord = "Password2!",
                    Birthday = "1988-01-01",
                    Accounts = new List<Account>
        {
            new Account(12344556, "MainAccount", "SEK", 2345M, 1),
            new Account(23455678, "SavingAccount", "EUR", 2345M, 2)
        }
                };

                var writer = new StringWriter();
                var reader = new StringReader("2"); 

                Console.SetIn(reader);

                // Act
                ShowBalance.ShowSpecificAccount(customer, writer);

                // Assert
                var output = writer.ToString().Trim(); 
                var expectedOutput = "Choose an account to view the balance:\r\n" +
                                     "1. Account 12344556: MainAccount - SEK\r\n" +
                                     "2. Account 23455678: SavingAccount - EUR\r\n" +
                                     "Balance for Account 23455678 (SavingAccount): 2345 - Loanamount: 0";

                Assert.AreEqual(expectedOutput, output);
            }



        }






            [TestClass]
        [TestCategory ("Withdraws")]
        public class WithdrawTests
        {
            [TestMethod]
            public void Withdraw_ValidAmount_UpdateBalance()
            {
                // Arrange
                var customer = new Customer
                {
                    UserName = "User2",
                    IDNumber = 5002,
                    FirstName = "Anna",
                    LastName = "Andersson",
                    PassWord = "Password2!",
                    Birthday = "1988-01-01",
                    Accounts = new List<Account>
            {
                new Account(12344556, "MainAccount", "SEK", 2345M, 1),
               
            }
                };
                decimal withdrawAmount = 500;

                // Act
                Withdraw(customer, withdrawAmount);

                // Assert
                Assert.AreEqual(1845, customer.Accounts[0].Balance);
            }

            [TestMethod]
            [TestCategory("Withdraws")]
            public void Withdraw_InvalidAmount_NoUpdateBalance()
            {
                // Arrange
                var customer = new Customer
                {
                    UserName = "User2",
                    IDNumber = 5002,
                    FirstName = "Anna",
                    LastName = "Andersson",
                    PassWord = "Password2!",
                    Birthday = "1988-01-01",
                    Accounts = new List<Account>
            {
                new Account(12344556, "MainAccount", "SEK", 2345M, 1),
                
            }
                };
                decimal withdrawAmount = 1500;

                // Act
                Withdraw(customer, withdrawAmount);

                // Assert
                Assert.AreEqual(2345, customer.Accounts[0].Balance);
            }




            [TestMethod]
            [TestCategory("Withdraws")]
            public void Withdraw_InsufficientBalance_NoUpdateBalance()
            {
                // Arrange
                var customer = new Customer
                {
                    UserName = "User2",
                    IDNumber = 5002,
                    FirstName = "Anna",
                    LastName = "Andersson",
                    PassWord = "Password2!",
                    Birthday = "1988-01-01",
                    Accounts = new List<Account>
        {
            new Account(12344556, "MainAccount", "SEK", 1000M, 1), // Balansen är 1000 kr
            
        }
                };
                decimal withdrawAmount = 1500; // Försök att ta ut 1500 kr från kontot som bara har 1000 kr

                // Act
                Withdraw(customer, withdrawAmount);

                // Assert
                // Balansen ska fortfarande vara densamma eftersom det inte finns tillräckligt med pengar på kontot för att göra uttaget
                Assert.AreEqual(1000, customer.Accounts[0].Balance);
            }















            private void Withdraw(Customer loggedInCustomer, decimal withdrawAmount)
            {

                if (withdrawAmount <= loggedInCustomer.Accounts[0].Balance)
                {
                    loggedInCustomer.Accounts[0].Balance -= withdrawAmount;
                }
                
            }


        }
    }
}

