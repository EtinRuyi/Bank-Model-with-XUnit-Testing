using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModel
{
    public class Dashboard
    {
        public void DisplayMenu(User loggedInUser)
        {
            var transact = new Transactions();
            var auth = new Auth();
            bool logout = false;

            while (!logout)
            {
                Console.Clear();
                Console.WriteLine("DASHBOARD");
                Console.Write($"Greetings, {loggedInUser.FullName}! Your account is securely logged in...\n\n");
                Console.WriteLine("Please select the option that suits your needs.");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer");
                Console.WriteLine("5. Check Balance");
                Console.WriteLine("6. Print Statement of Account");
                Console.WriteLine("7. Log Out");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {

                    switch (choice)
                    {
                        case 1:
                            transact.CreateAccount(loggedInUser);
                            DisplayMenu(loggedInUser);
                            break;
                        case 2:
                            transact.Deposit(loggedInUser);
                            break;
                        case 3:
                            transact.Withdraw(loggedInUser);
                            break;
                        case 4:
                            transact.Transfer(loggedInUser);
                            break;
                        case 5:
                            transact.GetBalance(loggedInUser);
                            break;
                        case 6:
                            transact.PrintStatementOfAccount(loggedInUser);
                            break;
                        case 7:
                            auth.LogOut();
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please select again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                Console.ReadKey();
            }
        }
    }
}
