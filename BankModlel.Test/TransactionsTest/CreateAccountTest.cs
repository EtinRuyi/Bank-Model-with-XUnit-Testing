using BankModel;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace BankModlel.Test.TransactionsTest
{
    public class CreateAccountTest
    {
        [Fact]
        public void CreateAccount_Successful()
        {
            // Arrange
            var loggedInUser = new User { Id = Guid.NewGuid() };
            var accountManager = new Transactions();

            // Act
            accountManager.CreateAccount(loggedInUser);

            // Assert
            // You can add assertions here to verify the expected behavior.
            // For example, you can check if the account was created in the AllAccounts list.
            Assert.Single(Transactions.AllAccounts);
            Assert.Equal(loggedInUser.Id, Transactions.AllAccounts[0].UserId);
            Assert.Equal(AccountType.Current, Transactions.AllAccounts[0].AccountType);
            // Add more assertions as needed.
        }

        [Fact]
        public void CreateAccount_AlreadyHasAccount()
        {
            // Arrange
            var loggedInUser = new User { Id = Guid.NewGuid() };
            var existingAccount = new Account
            {
                Id = Guid.NewGuid(),
                UserId = loggedInUser.Id,
                AccountType = AccountType.Current // Assuming an existing account of the same type.
            };
            var accountManager = new Transactions();
            Transactions.AllAccounts.Add(existingAccount);

            // Act
            accountManager.CreateAccount(loggedInUser);

            // Assert
            // Verify that the method does not create a new account when one already exists.
            Assert.Single(Transactions.AllAccounts); // No new accounts were added.
            // Add more assertions as needed.
        }

        [Fact]
        public void CreateAccount_InvalidChoice()
        {
            // Arrange
            var loggedInUser = new User { Id = Guid.NewGuid() };
            var accountManager = new Transactions();

            var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);
            // Act
            // Simulate an invalid choice (e.g., "3").
            // This should display an error message.

            accountManager.CreateAccount(loggedInUser);
            var output = consoleOutput.ToString();

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));


            // Assert
            Assert.Contains("Invalid choice. Please select either 1 for Current or 2 for Savings.", output);

        }

    }

}
