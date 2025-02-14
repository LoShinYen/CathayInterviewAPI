using Newtonsoft.Json;

namespace CathayInterviewAPI.Models.Responses
{
    public class CoindeskResponse : ResponseBase
    {
        [JsonProperty("updateTime")]
        public DateTime UpdateTime { get; set; }

        [JsonProperty("currencies")]
        public List<CurrencyInfoDto> Currencies { get; set; } = new List<CurrencyInfoDto>();
    }

    public class CurrencyInfoDto
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }
    }
}
