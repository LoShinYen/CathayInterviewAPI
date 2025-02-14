namespace CathayInterviewAPI.Repositories
{
    public interface ICurrencyRepository
    {
        Task<List<CurrencyDto>> GetCurrenciesAsync();

        Task<CurrencyDto> GetCurrencyByIdAsync(int id);

        Task<CurrencyDto> GetCurrencyByCodeAsync(string code);

        Task CreateCurrencyAsync(CreateCurrencyDto currency);

        Task UpdateCurrencyAsync(CurrencyDto currency);

        Task DeleteCurrencyAsync(int currencyId);
    }
}
