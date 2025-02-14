using Newtonsoft.Json;

namespace CathayInterviewAPI.Models.Dto
{
    public class CurrencyMappingDto
    {
        [JsonProperty("currencies")]
        public List<CurrencyMapping> Currencies { get; set; }
    }

    public class CurrencyMapping
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("chinese_name")]
        public string ChineseName { get; set; }
    }
}
