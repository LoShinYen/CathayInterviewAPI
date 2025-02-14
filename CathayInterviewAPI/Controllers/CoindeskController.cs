using CathayInterviewAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CathayInterviewAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoindeskController(ICoindeskService coindeskService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetCoindeskInfo()
        {
            var result = await coindeskService.QueryCoinDeskInfo();
            return Ok(result);
        }
    }
}
