using CathayInterviewAPI.Enums;
using CathayInterviewAPI.Helpers;

namespace CathayInterviewAPITest.Helpers
{
    public class ValidationHelperTests
    {
        [Fact]
        public void ValidateStringValue_ShouldReturnErrorResponse_WhenInputIsNull()
        {
            // Arrange
            string input = null;
            var expectedError = ResultEnums.CurrencyIsEmpty;

            // Act
            var result = ValidationHelper.ValidateStringValue(input, expectedError);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedError.Code, result.ResultCode);
            Assert.Equal(expectedError.Message, result.Message);
            Assert.Equal(expectedError.ResultLanguages.Ch, result.ResultLanguages.Ch);
            Assert.Equal(expectedError.ResultLanguages.En, result.ResultLanguages.En);
        }

        [Fact]
        public void ValidateStringValue_ShouldReturnErrorResponse_WhenInputIsEmpty()
        {
            // Arrange
            string input = "";
            var expectedError = ResultEnums.CurrencyIsEmpty;

            // Act
            var result = ValidationHelper.ValidateStringValue(input, expectedError);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedError.Code, result.ResultCode);
            Assert.Equal(expectedError.Message, result.Message);
            Assert.Equal(expectedError.ResultLanguages.Ch, result.ResultLanguages.Ch);
            Assert.Equal(expectedError.ResultLanguages.En, result.ResultLanguages.En);
        }

        [Fact]
        public void ValidateStringValue_ShouldReturnNull_WhenInputIsNotEmpty()
        {
            // Arrange
            string input = "USD";
            var expectedError = ResultEnums.CurrencyIsEmpty;

            // Act
            var result = ValidationHelper.ValidateStringValue(input, expectedError);

            // Assert
            Assert.Null(result);
        }
    }
}
