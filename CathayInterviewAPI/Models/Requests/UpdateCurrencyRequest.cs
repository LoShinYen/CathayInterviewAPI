using System.ComponentModel.DataAnnotations;

namespace CathayInterviewAPI.Models.Requests
{
    public class UpdateCurrencyRequest
    {
        public int CurrencyId { get; set; }

        [Required, StringLength(3)]
        public string UpdateCurrencyName { get; set; } = string.Empty;

    }
}
