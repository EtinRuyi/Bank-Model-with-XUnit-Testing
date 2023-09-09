using BM.Core.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace BankModel
{
    public class Auth
    {
        public static List<User> Users = new List<User>() { };

        public void RegisterMe()
        {
            string FirstName = "";
            string LastName = "";
            string Email = "";
            string Password = "";

            Console.Clear();
            Console.WriteLine("REGISTRATION PORTAL \n");
            Console.WriteLine("Please fill in your details...");
            FirstName = ValidationHelper.GetValidFirstName();

            Console.Clear();
            Console.WriteLine("REGISTRATION PORTAL \n");
            Console.WriteLine("Please fill in your details...");
            LastName = ValidationHelper.GetValidLastName();

            Console.Clear();
            Console.WriteLine("REGISTRATION PORTAL \n");
            Console.WriteLine("Please fill in your details...");
            Email = ValidationHelper.GetValidEmail();

            Console.Clear();
            Console.WriteLine("REGISTRATION PORTAL \n");
            Console.WriteLine("Please fill in your details...");
            Password = ValidationHelper.GetValidPassword();

            Console.Clear();
            Console.WriteLine("REGISTRATION PORTAL \n");
            var createdUser = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = FirstName,
                LastName = LastName,
                Email = Email,
                Password = Password,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            Users.Add(createdUser);
            Console.WriteLine();
            Console.WriteLine("Registration successful!");
            Console.WriteLine("Press Enter to Login");
            Console.ReadKey();
            Login();
        }

        public void Login()
        {
            Console.Clear();
            Console.WriteLine("LOGIN PORTAL");
            Console.WriteLine("IMPORTANT NOTICE: Keep your account information confidential. Your security is our top priority. \n");
            Console.WriteLine("Please fill in your correct details...");
            string Email = ValidationHelper.GetValidEmail();
            Console.Clear();
            Console.WriteLine("LOGIN PORTAL");
            Console.WriteLine("IMPORTANT NOTICE: Keep your account information confidential. Your security is our top priority. \n");
            Console.WriteLine("Please fill in your correct details...");
            string Password = ValidationHelper.GetValidPassword();

            User userExist = Users.FirstOrDefault(user => user.Email == Email && user.Password == Password);

            if (userExist != null)
            {
                Console.WriteLine("Login successful.");
                Session.LoggedInUser = userExist;
                var dash = new Dashboard();
                dash.DisplayMenu(Session.LoggedInUser);
            }
            else
            {
                Console.WriteLine("Login failed. Invalid email or password.");
                Console.ReadKey();
                Login();
            }
        }

        public void LogOut()
        {
            Console.Clear();
            Console.WriteLine("SESSION-OUT \n");
            Session.LoggedInUser = null;
            Console.WriteLine("Logged Out!");
            Console.ReadKey();
            WelcomePage.RunBankController();
            Environment.Exit(0);
            Console.ReadKey();

        }
    }
}
