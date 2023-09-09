using BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM.Core.Presentation
{
    public class WelcomePage
    {
        public static void RunBankController()
        {
            var auth = new Auth();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
                Console.WriteLine("                             WELCOME TO RUYI BANK                             ");
                Console.WriteLine("==============================================================================");
                Console.WriteLine("                Discover the difference of banking with Ruyi Bank.            ");
                Console.WriteLine("                 Your financial aspirations are our mission.                  ");
                Console.WriteLine("                 Welcome to a world of financial excellence.                \n");
                Console.WriteLine("Click '1' if you're new or '2' if you're already a part of Ruyi Bank.");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Log in");
                Console.WriteLine("3. Exit");
                Console.WriteLine("------------------------------------------------------------------------------");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            auth.RegisterMe();
                            break;

                        case 2:
                            auth.Login();
                            break;

                        case 3:
                            exit = true;
                            Console.WriteLine("Thank you for choosing RUYI BANK!");
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
