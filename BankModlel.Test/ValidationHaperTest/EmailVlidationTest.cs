using BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModlel.Test.ValidationHaperTest
{
    public class EmailVlidationTest
    {
        [Theory]
        [InlineData("test@example.com", true)]       // Valid email
        [InlineData("user@domain.co.uk", true)]      // Valid email
        [InlineData("invalid-email", false)]         // Invalid format
        [InlineData("user@domain", false)]           // Missing top-level domain
        [InlineData("user@.com", false)]             // Missing domain name
        [InlineData("user@domain.12", false)]        // Invalid characters in domain
        [InlineData("", false)]                      // Empty string
        [InlineData(null, false)]                    // Null input
        public void IsValidEmail_ValidAndInvalidEmails(string input, bool expectedResult)
        {
            // Act
            bool result = ValidationHelper.IsValidEmail(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
