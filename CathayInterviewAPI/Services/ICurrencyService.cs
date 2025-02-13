using CathayInterviewAPI.Models.Requests;

namespace CathayInterviewAPI.Services
{
    public interface ICurrencyService
    {
        Task<ResponseBase> GetCurrenciesAsync();

        Task<ResponseBase> CreateCurrencyAsync(CreateCurrencyRequest model);

        Task<ResponseBase> UpdateCurrencyAsync(UpdateCurrencyRequest model);

        Task<ResponseBase> DeleteCurrencyAsync(int currencyId);
    }
}
