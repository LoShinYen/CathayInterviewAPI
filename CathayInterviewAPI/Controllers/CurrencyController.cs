using CathayInterviewAPI.Helpers;
using CathayInterviewAPI.Models.Requests;
using CathayInterviewAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CathayInterviewAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CurrencyController(ICurrencyService currencyService) : ControllerBase
    {

        [HttpGet(Name = "GetCurrencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            var result = await currencyService.GetCurrenciesAsync();
            return ResponseHelper.CreateResponse(result);
        }

        [HttpPost(Name = "CreateCurrency")]
        public async Task<IActionResult> CreateCurrency([FromBody]CreateCurrencyRequest request)
        {
            var result = await currencyService.CreateCurrencyAsync(request);
            return ResponseHelper.CreateResponse(result);
        }

        [HttpPut(Name = "UpdateCurrency")]
        public async Task<IActionResult> UpdateCurrency([FromBody] UpdateCurrencyRequest request)
        {
            var result = await currencyService.UpdateCurrencyAsync(request);
            return ResponseHelper.CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var result = await currencyService.DeleteCurrencyAsync(id);
            return ResponseHelper.CreateResponse(result);
        }

    }
}
