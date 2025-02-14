using Newtonsoft.Json;

namespace CathayInterviewAPI.Models.Dto
{
    public class CurrencyMappingDto
    {
        [JsonProperty("currencies")]
        public List<CurrencyMapping> Currencies { get; set; } = new List<CurrencyMapping>();
    }

    public class CurrencyMapping
    {
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("chinese_name")]
        public string ChineseName { get; set; } = string.Empty;
    }
}
