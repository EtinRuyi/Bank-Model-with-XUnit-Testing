﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankModel
{
    public class Transactions
    {
        public static List<Account> AllAccounts = new List<Account>();

        public void CreateAccount(User loggedInUser)
        {
            Console.Clear();
            Console.WriteLine("CREATE ACCOUNT \n");
            Console.WriteLine("Select Account Type");
            Console.WriteLine("1. Current");
            Console.WriteLine("2. Savings");

            string choice = Console.ReadLine();

            var selectedAccountType = (AccountType)Enum.Parse(typeof(AccountType), choice);

            var existingAccountOfType = AllAccounts.Find(acc => acc.UserId == loggedInUser.Id && acc.AccountType == selectedAccountType);


            if (choice == "1" || choice == "2")
            {
               
                if (choice == "1")
                {
                    if (existingAccountOfType != null)
                    {
                        Console.WriteLine($"You already have a {(selectedAccountType == AccountType.Current ? "Savings" : "Current")} account.");
                        Console.WriteLine("You can't have multiple accounts of the same type.");
                        Console.ReadKey();
                        return;
                    }

                    var newAccount = new Account()
                    {
                        Id = Guid.NewGuid(),
                        AccountNo = ValidationHelper.GenerateAccountNumber(),
                        AccountType = AccountType.Current,
                        Balance = 0,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        UserId = loggedInUser.Id
                    };
                    AllAccounts.Add(newAccount);
                    Console.WriteLine($"Your {AccountType.Current} account was successfully created.");
                    Console.WriteLine("Find your Account details and Balance below:");
                    PrintAllAccounts(loggedInUser);
                    Console.ReadKey();
                }
                else if (choice == "2")
                {
                    if (existingAccountOfType != null)
                    {
                        Console.WriteLine($"You already have a {(selectedAccountType == AccountType.Savings ? "Current" : "Savings")} account.");
                        Console.WriteLine("You can't have multiple accounts of the same type.");
                        Console.ReadKey();
                        return;
                    }

                    var newAccount = new Account()
                    {
                        Id = Guid.NewGuid(),
                        AccountNo = ValidationHelper.GenerateAccountNumber(),
                        AccountType = AccountType.Savings,
                        Balance = 0,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                        UserId = loggedInUser.Id
                    };
                    AllAccounts.Add(newAccount);
                    Console.WriteLine($"Your {AccountType.Savings} account was successfully created.");
                    Console.WriteLine("Find your Account details and Balance below:");
                    PrintAllAccounts(loggedInUser);
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Invalid choice. Please select either 1 for Current or 2 for Savings.");
            }
            Console.ReadKey();
        }

        public void Deposit(User loggedInUser)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("DEPOSIT \n");
                Console.WriteLine("Please fill in your details correctly");
                Console.WriteLine("Enter the account number ");
                if (!int.TryParse(Console.ReadLine(), out int AccountNo))
                {
                    Console.WriteLine("Invalid account number. Please enter a valid number.");
                    Console.ReadKey();
                    return;
                }

                var accountExist = AllAccounts.Find(acc => acc.UserId == loggedInUser.Id && acc.AccountNo == AccountNo);

                if (accountExist != null)
                {
                    decimal Amount;
                    while (true)
                    {
                        Console.WriteLine("Enter amount to deposit");
                        if (!decimal.TryParse(Console.ReadLine(), out Amount) || Amount <= 0)
                        {
                            Console.WriteLine("Invalid amount. Please enter an amount greater than 0.");
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.Write("Enter a note for this deposit: ");
                    string depositNote = Console.ReadLine();
                    accountExist.Balance += Amount;
                    accountExist.Note = depositNote;
                    Console.WriteLine("Deposit was successful");
                    Console.WriteLine("Your new balance is displayed below");
                    PrintAllAccounts(loggedInUser);
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Error, Incorrect data supplied!");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.ReadKey();
            }
        }

        public void Withdraw(User loggedInUser)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("WITHDRAWAL \n");
                Console.WriteLine("Please fill in your details correctly");
                Console.WriteLine("Enter the account number");
                if (!int.TryParse(Console.ReadLine(), out int AccountNo))
                {
                    Console.WriteLine("Invalid account number. Please enter a valid number.");
                    Console.ReadKey();
                    return;
                }

                var accountExist = AllAccounts.Find(acc => acc.UserId == loggedInUser.Id && acc.AccountNo == AccountNo);

                if (accountExist != null)
                {
                    decimal Amount;
                    while (true)
                    {
                        Console.WriteLine("Enter amount to withdraw");
                        if (!decimal.TryParse(Console.ReadLine(), out Amount))
                        {
                            Console.WriteLine("Invalid amount. Please enter a valid number.");
                        }
                        else if (Amount < 0)
                        {
                            Console.WriteLine("Withdrawal amount cannot be negative. Please enter a valid amount.");
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (accountExist.AccountType == AccountType.Savings && accountExist.Balance - Amount < 1000)
                    {
                        Console.WriteLine("Savings Account must have a minimum balance of 1000.");
                    }
                    else if (Amount > accountExist.Balance)
                    {
                        Console.WriteLine("Insufficient Balance");
                    }
                    else
                    {
                        Console.Write("Enter a note for this withdrawal: ");
                        string withdrawalNote = Console.ReadLine();
                        accountExist.Balance -= Amount;
                        accountExist.Note = withdrawalNote;
                        Console.WriteLine("Withdraw was successful!");
                    }
                    Console.WriteLine("Your new balance is displayed below");
                    PrintAllAccounts(loggedInUser);
                    Console.ReadKey();

                }
                else
                {
                    Console.WriteLine("Error, Incorrect data supplied");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.ReadKey();
            }
        }

        public void Transfer(User loggedInUser)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("TRANSFER FUNDS \n");
                Console.WriteLine("Please fill in your details correctly");
                Console.WriteLine("Enter your account number ");
                if (!int.TryParse(Console.ReadLine(), out int userAccountNo))
                {
                    Console.WriteLine("Invalid account number. Please enter a valid number.");
                    Console.ReadKey();
                    return;
                }

                var userAccount = AllAccounts.Find(acc => acc.UserId == loggedInUser.Id && acc.AccountNo == userAccountNo);

                if (userAccount == null)
                {
                    Console.WriteLine("Error, Incorrect data supplied!");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Enter recipient's account number ");
                if (!int.TryParse(Console.ReadLine(), out int RecipientAccountNo))
                {
                    Console.WriteLine("Invalid recipient account number. Please enter a valid number.");
                    Console.ReadKey();
                    return;
                }

                if (RecipientAccountNo == userAccountNo)
                {
                    Console.WriteLine("You cannot transfer funds to the same account number.");
                    Console.ReadKey();
                    return;
                }

                var recipientAccount = AllAccounts.Find(acc => acc.AccountNo == RecipientAccountNo);

                if (recipientAccount == null)
                {
                    Console.WriteLine("Recipient account not found.");
                    Console.ReadKey();
                    return;
                }

                decimal Amount;
                while (true)
                {
                    Console.WriteLine("Enter amount to transfer");
                    if (!decimal.TryParse(Console.ReadLine(), out Amount))
                    {
                        Console.WriteLine("Invalid amount. Please enter a valid number.");
                    }
                    else if (Amount < 0)
                    {
                        Console.WriteLine("Transfer amount cannot be negative. Please enter a valid amount.");
                    }
                    else
                    {
                        break;
                    }
                }

                if (userAccount.AccountType == AccountType.Savings && userAccount.Balance - Amount < 1000)
                {
                    Console.WriteLine("Savings Account must have a minimum balance of 1000.");
                    Console.WriteLine("Insufficient balance.");
                }
                else if (Amount > userAccount.Balance)
                {
                    Console.WriteLine("Insufficient balance.");
                }
                else
                {
                    Console.Write("Enter a note for this transfer: ");
                    string transferNote = Console.ReadLine();
                    userAccount.Balance -= Amount;
                    recipientAccount.Balance += Amount;
                    recipientAccount.Note = transferNote;
                    Console.WriteLine("Transfer was successful");
                }
                Console.WriteLine("Your new balance is displayed below");
                PrintAllAccounts(loggedInUser);
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.ReadKey();
            }
        }

        public void GetBalance(User loggedInUser)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("ACCOUNT BALANCE \n");
                Console.WriteLine("Please fill in your details correctly to get your balance");
                Console.WriteLine("Enter the account number ");
                if (!int.TryParse(Console.ReadLine(), out int AccountNo))
                {
                    Console.WriteLine("Invalid account number. Please enter a valid number.");
                    Console.ReadKey();
                    return;
                }

                var accountExist = AllAccounts.Find(acc => acc.UserId == loggedInUser.Id && acc.AccountNo == AccountNo);

                if (accountExist != null)
                {
                    Console.WriteLine("Account Details");
                    Console.WriteLine(new string('-', 20));
                    Console.WriteLine("Account Name: {0}", loggedInUser.FullName);
                    Console.WriteLine("Account Number: {0}", accountExist.AccountNo);
                    Console.WriteLine("Account Type: {0}", accountExist.AccountType);
                    Console.WriteLine("Account Balance: #{0}", accountExist.Balance.ToString("N2"));
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Error, Incorrect data supplied!");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                Console.ReadKey();
            }
        }

        public void PrintStatementOfAccount(User loggedInUser)
        {
            Console.Clear();
            Console.WriteLine("ACCOUNT STATEMENT \n");
            var userAccounts = AllAccounts.Where(user => user.UserId == loggedInUser.Id).ToList();

            Console.WriteLine("Find your Account Statement below");
            PrintAllAccounts(loggedInUser);
            Console.ReadKey();
        }

        public void PrintAllAccounts(User loggedInUser)
        {
            var userAccounts = AllAccounts.Where(user => user.UserId == loggedInUser.Id).ToList();

            Console.WriteLine("=============================================================================================================");
            Console.WriteLine("|      FULL NAME       |      ACCOUNT NUMBER      |      ACCOUNT TYPE      |      AMOUNT BAL(#)     |    REMARKS       ");
            Console.WriteLine("=============================================================================================================");

            foreach (Account account in userAccounts)
            {
                Console.WriteLine($"| {loggedInUser.FullName,-21} | {account.AccountNo,-23} | {account.AccountType,-24} | #{account.Balance.ToString("N2"),-21}  | {account.Note,-15} ");
            }

            Console.WriteLine("=============================================================================================================");
            Console.WriteLine("\n Thanks for banking with us...");
        }
    }
}
