using Newtonsoft.Json;

namespace CathayInterviewAPI.Models.Dto
{
    public class CoindeskApiDto
    {
        [JsonProperty("time")]
        public CoindeskTime Time { get; set; } = new CoindeskTime();

        [JsonProperty("bpi")]
        public Dictionary<string, CoindeskCurrencyInfo> Bpi { get; set; } = new Dictionary<string, CoindeskCurrencyInfo>();

        [JsonProperty("chartName")]
        public string ChartName { get; set; } = string.Empty;

        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; } = string.Empty;
    }

    public class CoindeskTime
    {
        [JsonProperty("updatedISO")]
        public DateTime UpdatedISO { get; set; }
    }

    public class CoindeskCurrencyInfo
    {
        [JsonProperty("code")]
        public string Code { get; set; } = string.Empty;

        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;

        [JsonProperty("rate")]
        public string Rate { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("rate_float")]
        public decimal RateFloat { get; set; }
    }
}
