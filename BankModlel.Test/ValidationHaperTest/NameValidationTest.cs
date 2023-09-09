using BankModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankModlel.Test.ValidationHaperTest
{
    public class NameValidationTest
    {
        [Theory]
        [InlineData("John", true)]        // Valid name
        [InlineData("Mary", true)]        // Valid name
        [InlineData("123", false)]        // Contains digits
        [InlineData("", false)]           // Empty string
        [InlineData(null, false)]         // Null input
        [InlineData("john", false)]       // Lowercase first letter
        [InlineData("John Smith", false)] // Contains spaces
        [InlineData("S", true)]           // Single uppercase letter
        public void IsValidName_ValidAndInvalidNames(string input, bool expectedResult)
        {
            // Act
            bool result = ValidationHelper.IsValidName(input);

            // Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
