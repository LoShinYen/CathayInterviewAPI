using CathayInterviewAPI.Models.Dto;
using CathayInterviewAPI.Models.Responses;
using CathayInterviewAPI.Services;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace CathayInterviewAPITest.Services
{
    public class CoindeskServiceTests
    {
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
        private readonly CoindeskService _coindeskService;

        public CoindeskServiceTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            var httpClient = new HttpClient(_httpMessageHandlerMock.Object)
            {
                BaseAddress = new Uri("https://api.coindesk.com/")
            };

            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _httpClientFactoryMock.Setup(_ => _.CreateClient("ExternalAPI")).Returns(httpClient);

            _coindeskService = new CoindeskService(_httpClientFactoryMock.Object);
        }

        [Fact]
        public async Task QueryCoinDeskInfo_ShouldReturnData_WhenApiSuccess()
        {
            // Arrange
            var expectedResponse = new CoindeskApiDto
            {
                Time = new CoindeskTime { UpdatedISO = DateTime.Parse("2025-02-15T10:00:00+00:00") },
                Bpi = new Dictionary<string, CoindeskCurrencyInfo>
                {
                    { "USD", new CoindeskCurrencyInfo { Code = "USD", Description = "US Dollar", RateFloat = 45000.50m } }
                }
            };

            var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(jsonResponse, Encoding.UTF8, "application/json")
                });

            // Act
            var result = await _coindeskService.QueryCoinDeskInfo();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.ResultCode);
            var coindeskResponse = (CoindeskResponse)result;
            Assert.NotNull(coindeskResponse.Currencies);
            Assert.Single(coindeskResponse.Currencies);
            Assert.Equal("USD", coindeskResponse.Currencies[0].Code);
            Assert.Equal("US Dollar", coindeskResponse.Currencies[0].Name);
            Assert.Equal(45000.50m, coindeskResponse.Currencies[0].Rate);
        }

        [Fact]
        public async Task QueryCoinDeskInfo_ShouldUseMockData_WhenApiFails()
        {
            // Arrange - API 失敗時回傳錯誤
            _httpMessageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError
                });

            // Act
            var result = await _coindeskService.QueryCoinDeskInfo();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(601, result.ResultCode); 
        }
    }
}
