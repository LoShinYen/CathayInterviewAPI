namespace CathayInterviewAPI.Models.Responses
{
    public class CurrenciesResponse : ResponseBase
    {
        public List<CurrencyDto> Currencies { get; set; } = new List<CurrencyDto>();
    }

}
