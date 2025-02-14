using CathayInterviewAPI.Enums;
using CathayInterviewAPI.Helpers;
using CathayInterviewAPI.Models.BaseModels;
using Microsoft.AspNetCore.Mvc;

namespace CathayInterviewAPITest.Helpers
{
    public class ResponseHelperTests
    {
        [Fact]
        public void CreateResponse_ShouldReturn_BadRequest_WhenResponseIsNull()
        {
            // Arrange
            ResponseBase? response = null;

            // Act
            var result = ResponseHelper.CreateResponse(response);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var responseBody = Assert.IsType<ResponseBase>(badRequestResult.Value);
            Assert.Equal(ResultEnums.ElseError.Code, responseBody.ResultCode);
            Assert.Equal("No Data", responseBody.Message);
        }

        [Fact]
        public void CreateResponse_ShouldReturn_Ok_WhenSuccess()
        {
            // Arrange
            var response = new ResponseBase
            {
                ResultCode = ResultEnums.Success.Code,
                Message = ResultEnums.Success.Message
            };

            // Act
            var result = ResponseHelper.CreateResponse(response);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseBody = Assert.IsType<ResponseBase>(okResult.Value);
            Assert.Equal(ResultEnums.Success.Code, responseBody.ResultCode);
        }

        [Theory]
        [InlineData(200, "Success")] // 測試成功
        [InlineData(500, "Unknown Error")] // 測試失敗
        [InlineData(400, "Bad Request")] // 測試失敗
        public void CreateResponse_ShouldReturn_CorrectResponseCode(int resultCode, string message)
        {
            // Arrange
            var response = new ResponseBase
            {
                ResultCode = resultCode,
                Message = message
            };

            // Act
            var result = ResponseHelper.CreateResponse(response);

            // Assert
            if (resultCode == ResultEnums.Success.Code || message.Contains("Success"))
            {
                var okResult = Assert.IsType<OkObjectResult>(result);
                var responseBody = Assert.IsType<ResponseBase>(okResult.Value);
                Assert.Equal(resultCode, responseBody.ResultCode);
            }
            else
            {
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                var responseBody = Assert.IsType<ResponseBase>(badRequestResult.Value);
                Assert.Equal(resultCode, responseBody.ResultCode);
            }
        }
    }
}
