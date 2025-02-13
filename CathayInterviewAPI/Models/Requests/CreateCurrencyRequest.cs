using System.ComponentModel.DataAnnotations;

namespace CathayInterviewAPI.Models.Request
{
    public class CreateCurrencyRequest
    {
        [Required, StringLength(3)]
        public string CurrencyCode { get; set; } = string.Empty;

    }
}
