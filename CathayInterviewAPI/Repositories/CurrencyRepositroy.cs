using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace CathayInterviewAPI.Repositories
{
    public class CurrencyRepositroy(CathayInterviewDBContext context , IMapper mapper) : ICurrencyRepository
    {

        public async Task<List<CurrencyDto>> GetCurrenciesAsync()
        {
            var query = await context.Currencies.OrderBy(c => c.CurrencyCode).ToListAsync();

            var result = mapper.Map<List<CurrencyDto>>(query);

            return result;
        }

        public async Task<CurrencyDto> GetCurrencyByIdAsync(int id)
        {
            var query = await context.Currencies.Where(c => c.CurrencyId == id).FirstOrDefaultAsync();

            var response = mapper.Map<CurrencyDto>(query);

            return response;
        }

        public async Task<CurrencyDto> GetCurrencyByCodeAsync(string code)
        {
            var query = await context.Currencies.Where(c => c.CurrencyCode == code).FirstOrDefaultAsync();

            var response = mapper.Map<CurrencyDto>(query);

            return response;
        }

        public async Task CreateCurrencyAsync(CreateCurrencyDto currency)
        {
            var newCurrency = mapper.Map<Currency>(currency);

            context.Currencies.Add(newCurrency);
            await context.SaveChangesAsync();
        }

        public async Task UpdateCurrencyAsync(CurrencyDto currency)
        {
            var existingCurrency = await ExistingCurrencyAsync(currency.CurrencyId);
            if (existingCurrency != null)
            { 
                existingCurrency.CurrencyCode = currency.CurrencyName;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteCurrencyAsync(int currencyId)
        {
            var existingCurrency = await ExistingCurrencyAsync(currencyId);
            if (existingCurrency != null)
            { 
                context.Remove(existingCurrency);
                await context.SaveChangesAsync();
            }
        }

        private async Task<Currency?> ExistingCurrencyAsync(int currencyId)
        {
            var existingCurrency = await context.Currencies.Where(c => c.CurrencyId == currencyId).FirstOrDefaultAsync();
            return existingCurrency;
        }

    }
}
