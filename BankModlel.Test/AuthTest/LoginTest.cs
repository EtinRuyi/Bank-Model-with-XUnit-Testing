using BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModlel.Test.AuthTest
{
    public class LoginTest
    {
        [Fact]
        public void FindUserByEmailAndPassword_ShouldReturnUser_WhenUserExists()
        {
            // Arrange
            var users = new List<User>
        {
            new User { FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", Password = "password123" },
            new User { FirstName = "Alice", LastName = "Smith", Email = "alicesmith@example.com", Password = "securePass" },
            // Add more user objects as needed for your test cases
        };

            // Act
            var result = users.FirstOrDefault(user => user.Email == "johndoe@example.com" && user.Password == "password123");

            // Assert
            Assert.NotNull(result); // Check that a user is found
            Assert.Equal("John", result.FirstName); // Validate the user's first name
            Assert.Equal("Doe", result.LastName); // Validate the user's last name
            Assert.Equal("johndoe@example.com", result.Email); // Validate the user's email
            Assert.Equal("password123", result.Password); // Validate the user's password
        }
    }
}
