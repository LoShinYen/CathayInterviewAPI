using CathayInterviewAPI.Enums;
using CathayInterviewAPI.Models.Dto;
using CathayInterviewAPI.Models.Request;
using CathayInterviewAPI.Models.Requests;
using CathayInterviewAPI.Models.Responses;
using CathayInterviewAPI.Repositories;
using CathayInterviewAPI.Services;
using Moq;

namespace CathayInterviewAPITest.Services
{
    public class CurrencyServiceTests
    {
        private readonly Mock<ICurrencyRepository> _currencyRepositoryMock;
        private readonly ICurrencyService _currencyService;

        public CurrencyServiceTests()
        {
            _currencyRepositoryMock = new Mock<ICurrencyRepository>();
            _currencyService = new CurrencyService(_currencyRepositoryMock.Object);
        }

        [Fact]
        public async Task GetCurrenciesAsync_ShouldReturn_CurrencyList()
        {
            // Arrange
            var currencies = new List<CurrencyDto>
            {
                new CurrencyDto { CurrencyId = 1, CurrencyName = "USD" },
                new CurrencyDto { CurrencyId = 2, CurrencyName = "EUR" }
            };

            _currencyRepositoryMock.Setup(repo => repo.GetCurrenciesAsync()).ReturnsAsync(currencies);

            // Act
            var result = await _currencyService.GetCurrenciesAsync();

            // Assert
            var response = Assert.IsType<CurrenciesResponse>(result);
            Assert.NotNull(response.Currencies);
            Assert.Equal(2, response.Currencies.Count);
        }

        [Fact]
        public async Task CreateCurrencyAsync_ShouldAdd_NewCurrency()
        {
            // Arrange
            var request = new CreateCurrencyRequest { CurrencyCode = "JPY" };

            _currencyRepositoryMock
                .Setup(repo => repo.CreateCurrency(It.IsAny<CreateCurrencyDto>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _currencyService.CreateCurrencyAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.ResultCode); // 預設成功回應
        }

        [Fact]
        public async Task CreateCurrencyAsync_ShouldReturnError_WhenCurrencyCodeIsEmpty()
        {
            // Arrange
            var request = new CreateCurrencyRequest { CurrencyCode = "" };

            // Act
            var result = await _currencyService.CreateCurrencyAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResultEnums.CurrencyIsEmpty.Code, result.ResultCode);
        }

        [Fact]
        public async Task UpdateCurrencyAsync_ShouldUpdate_Currency()
        {
            // Arrange
            var request = new UpdateCurrencyRequest { CurrencyId = 1, UpdateCurrencyName = "US Dollar" };
            var existingCurrency = new CurrencyDto { CurrencyId = 1, CurrencyName = "USD" };

            _currencyRepositoryMock
                .Setup(repo => repo.GetCurrencyByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(existingCurrency);

            _currencyRepositoryMock
                .Setup(repo => repo.UpdateCurrencyAsync(It.IsAny<CurrencyDto>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _currencyService.UpdateCurrencyAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.ResultCode); // 預設成功回應
        }

        [Fact]
        public async Task UpdateCurrencyAsync_ShouldReturnError_WhenCurrencyNotFound()
        {
            // Arrange
            var request = new UpdateCurrencyRequest { CurrencyId = 999, UpdateCurrencyName = "New Name" };

            _currencyRepositoryMock
                .Setup(repo => repo.GetCurrencyByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((CurrencyDto?)null);

            // Act
            var result = await _currencyService.UpdateCurrencyAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResultEnums.NotFindCurrecy.Code, result.ResultCode);
        }

        [Fact]
        public async Task DeleteCurrencyAsync_ShouldRemove_Currency()
        {
            // Arrange
            var existingCurrency = new CurrencyDto { CurrencyId = 1, CurrencyName = "USD" };

            _currencyRepositoryMock
                .Setup(repo => repo.GetCurrencyByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(existingCurrency);

            _currencyRepositoryMock
                .Setup(repo => repo.DeleteCurrencyAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _currencyService.DeleteCurrencyAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.ResultCode); // 預設成功回應
        }

        [Fact]
        public async Task DeleteCurrencyAsync_ShouldReturnError_WhenCurrencyNotFound()
        {
            // Arrange
            _currencyRepositoryMock
                .Setup(repo => repo.GetCurrencyByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((CurrencyDto)null);

            // Act
            var result = await _currencyService.DeleteCurrencyAsync(999);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(ResultEnums.NotFindCurrecy.Code, result.ResultCode);
        }
    }
}
