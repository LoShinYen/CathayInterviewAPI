using CathayInterviewAPI.Helpers;
using CathayInterviewAPI.Models.Requests;
using CathayInterviewAPI.Models.Responses;
using CathayInterviewAPI.Repositories;

namespace CathayInterviewAPI.Services
{
    public class CurrencyService(ICurrencyRepository currencyRepositroy) : ICurrencyService
    {

        public async Task<ResponseBase> GetCurrenciesAsync()
        {
            var response = new CurrenciesResponse();

            var query = await currencyRepositroy.GetCurrenciesAsync();
            response.Currencies = query;

            return response;
        }

        public async Task<ResponseBase> CreateCurrencyAsync(CreateCurrencyRequest currency)
        {
            var validateCurrencyCode = ValidationHelper.ValidateStringValue(currency.CurrencyCode, ResultEnums.CurrencyIsEmpty);

            if (validateCurrencyCode != null) return validateCurrencyCode;

            var dto = new CreateCurrencyDto { CurrencyCode = currency.CurrencyCode };

            await currencyRepositroy.CreateCurrency(dto);

            return new ResponseBase();
        }

        public async Task<ResponseBase> UpdateCurrencyAsync(UpdateCurrencyRequest model)
        {
            var existingCurrency = await ExistingCurrencyAsync(model.CurrencyId);
            if (existingCurrency.ResultCode != ResultEnums.Success.Code)
            {
                return existingCurrency;
            }

            var dto = new CurrencyDto { CurrencyId = model.CurrencyId, CurrencyName = model.UpdateCurrencyName };

            var validateCurrencyCode = ValidationHelper.ValidateStringValue(dto.CurrencyName, ResultEnums.CurrencyIsEmpty);

            if (validateCurrencyCode != null) return validateCurrencyCode;

            await currencyRepositroy.UpdateCurrencyAsync(dto);

            return new ResponseBase(); ;
        }

        public async Task<ResponseBase> DeleteCurrencyAsync(int currencyId) 
        {
            var existingCurrency = await ExistingCurrencyAsync(currencyId);
            if (existingCurrency.ResultCode != ResultEnums.Success.Code)
            {
                return existingCurrency;
            }

            await currencyRepositroy.DeleteCurrencyAsync(currencyId);

            return new ResponseBase();
        }

        private async Task<ResponseBase> ExistingCurrencyAsync(int currencyId)
        {
            var response = new ResponseBase();
            var existingCurrency = await currencyRepositroy.GetCurrencyByIdAsync(currencyId);
            if (existingCurrency == null)
            {
                response.setResult(ResultEnums.NotFindCurrecy);
                return response;
            }
            return response;
        }
    }
}
