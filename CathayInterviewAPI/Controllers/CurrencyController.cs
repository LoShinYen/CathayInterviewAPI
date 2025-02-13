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
            return Ok(result);
        }

        [HttpPost(Name = "CreateCurrency")]
        public async Task<IActionResult> CreateCurrency([FromBody]CreateCurrencyRequest request)
        {
            var result = await currencyService.CreateCurrencyAsync(request);
            return Ok(result);
        }

        [HttpPut(Name = "UpdateCurrency")]
        public async Task<IActionResult> UpdateCurrency([FromBody] UpdateCurrencyRequest request)
        {
            var reuslt = await currencyService.UpdateCurrencyAsync(request);
            return Ok(reuslt);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency(int id)
        {
            var result = await currencyService.DeleteCurrencyAsync(id);
            return Ok(result);
        }

    }
}
