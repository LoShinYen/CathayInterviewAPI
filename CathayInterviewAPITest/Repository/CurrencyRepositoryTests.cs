using AutoMapper;
using Castle.Components.DictionaryAdapter.Xml;
using CathayInterviewAPI.Mappings;
using CathayInterviewAPI.Models.Context;
using CathayInterviewAPI.Models.Dto;
using CathayInterviewAPI.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CathayInterviewAPITest.Repository
{
    public class CurrencyRepositoryTests
    {
        private readonly DbContextOptions<CathayInterviewDBContext> _dbContextOptions;
        private readonly IMapper _mapper;

        public CurrencyRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<CathayInterviewDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CurrencyProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetCurrenciesAsync_ShouldReturn_CurrencyList()
        {
            // Arrange
            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                context.Currencies.AddRange(new List<Currency>
                {
                    new Currency { CurrencyId = 1, CurrencyCode = "USD" },
                    new Currency { CurrencyId = 2, CurrencyCode = "EUR" }
                });
                await context.SaveChangesAsync();
            }

            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                var repository = new CurrencyRepositroy(context, _mapper);

                // Act
                var result = await repository.GetCurrenciesAsync();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(2, result.Count);
            }
        }

        [Fact]
        public async Task GetCurrencyByIdAsync_ShouldReturn_CorrectCurrency()
        {
            // Arrange
            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Currencies.Add(new Currency { CurrencyId = 1, CurrencyCode = "USD" });
                await context.SaveChangesAsync();
            }

            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                var repository = new CurrencyRepositroy(context, _mapper);

                // Act
                var result = await repository.GetCurrencyByIdAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("USD", result.CurrencyName);
            }
        }

        [Fact]
        public async Task GetCurrencyByCodeAsync_ShouldReturn_CorrectCurrency_WhenExists()
        {
            // Arrange
            int createId = 1;
            string code = "USDT";
            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Currencies.Add(new Currency { CurrencyId = createId, CurrencyCode = code });
                await context.SaveChangesAsync();
            }

            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                var repository = new CurrencyRepositroy(context, _mapper);

                // Act
                var result = await repository.GetCurrencyByCodeAsync(code);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(createId, result.CurrencyId);
                Assert.Equal(code, result.CurrencyName);
            }
        }

        [Fact]
        public async Task CreateCurrency_ShouldAdd_NewCurrency()
        {
            // Arrange
            var newCurrencyDto = new CreateCurrencyDto { CurrencyCode = "JPY" };

            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var repository = new CurrencyRepositroy(context, _mapper);

                // Act
                await repository.CreateCurrencyAsync(newCurrencyDto);
            }

            // Assert
            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                var currency = await context.Currencies.FirstOrDefaultAsync(c => c.CurrencyCode == "JPY");
                Assert.NotNull(currency);
            }
        }

        [Fact]
        public async Task UpdateCurrencyAsync_ShouldUpdate_Currency()
        {
            // Arrange
            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var existingCurrency = new Currency { CurrencyId = 1, CurrencyCode = "USD" };
                context.Currencies.Add(existingCurrency);
                await context.SaveChangesAsync();
            }

            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                var repository = new CurrencyRepositroy(context, _mapper);
                var updateDto = new CurrencyDto { CurrencyId = 1, CurrencyName = "US Dollar" };

                // Act
                await repository.UpdateCurrencyAsync(updateDto);
            }

            // Assert
            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                var updatedCurrency = await context.Currencies.FindAsync(1);
                Assert.NotNull(updatedCurrency);
                Assert.Equal("US Dollar", updatedCurrency.CurrencyCode);
            }
        }

        [Fact]
        public async Task DeleteCurrencyAsync_ShouldRemove_Currency()
        {
            // Arrange
            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var currency = new Currency { CurrencyId = 1, CurrencyCode = "USD" };
                context.Currencies.Add(currency);
                await context.SaveChangesAsync();
            }

            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                var repository = new CurrencyRepositroy(context, _mapper);

                // Act
                await repository.DeleteCurrencyAsync(1);
            }

            // Assert
            using (var context = new CathayInterviewDBContext(_dbContextOptions))
            {
                var deletedCurrency = await context.Currencies.FindAsync(1);
                Assert.Null(deletedCurrency);
            }
        }
    }
}
