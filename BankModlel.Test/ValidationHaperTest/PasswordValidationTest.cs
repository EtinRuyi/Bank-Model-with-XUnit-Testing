using BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModlel.Test.ValidationHaperTest
{
    public class PasswordValidationTest
    {
        [Theory]
        [InlineData("Password1@", true)]     // Valid password
        [InlineData("P@ssw0", true)]          // Valid password
        [InlineData("LongPassword12345@", true)] // Valid long password
        [InlineData("nopassword", false)]     // Missing uppercase letter and special character
        [InlineData("12345678@", false)]      // Missing lowercase letter
        [InlineData("P@ssw", false)]          // Missing digit
        [InlineData("", false)]               // Empty string
        public void IsValidPassword_ValidAndInvalidPasswords(string input, bool expectedResult)
        {
            // Act
            bool result = ValidationHelper.IsValidPassword(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
