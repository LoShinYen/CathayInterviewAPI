using Newtonsoft.Json;

namespace CathayInterviewAPI.Models.Dto
{
    public class CoindeskApiDto
    {
        [JsonProperty("time")]
        public CoindeskTime Time { get; set; }

        [JsonProperty("bpi")]
        public Dictionary<string, CoindeskCurrencyInfo> Bpi { get; set; }

        [JsonProperty("chartName")]
        public string ChartName { get; set; }

        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; }
    }

    public class CoindeskTime
    {
        [JsonProperty("updatedISO")]
        public DateTime UpdatedISO { get; set; }
    }

    public class CoindeskCurrencyInfo
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("rate")]
        public string Rate { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("rate_float")]
        public decimal RateFloat { get; set; }
    }
}
