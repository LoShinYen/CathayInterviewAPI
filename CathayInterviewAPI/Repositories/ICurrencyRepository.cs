namespace CathayInterviewAPI.Repositories
{
    public interface ICurrencyRepository
    {
        Task<List<CurrencyDto>> GetCurrenciesAsync();

        Task<CurrencyDto> GetCurrencyByIdAsync(int id);

        Task CreateCurrency(CreateCurrencyDto currency);

        Task UpdateCurrencyAsync(CurrencyDto currency);

        Task DeleteCurrencyAsync(int currencyId);
    }
}
