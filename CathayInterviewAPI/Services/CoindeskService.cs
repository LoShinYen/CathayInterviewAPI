using CathayInterviewAPI.Models.Responses;
using Newtonsoft.Json;

namespace CathayInterviewAPI.Services
{
    public class CoindeskService(IHttpClientFactory httpClientFactory) : ICoindeskService
    {
        const string queryUrl = "v1/bpi/currentprice.json";

        public async Task<ResponseBase> QueryCoinDeskInfo()
        {
            var response = new ResponseBase();
            try
            {
                var client = httpClientFactory.CreateClient("ExternalAPI");
                var apiResponse = await client.GetAsync(queryUrl);

                // API 異常改走假資料
                if (apiResponse.IsSuccessStatusCode)
                {
                    
                }
            }
            catch (HttpRequestException)
            {
                response = await GetMockResponse();
            }
            catch (Exception ex)
            {
                response.setException(ex);
            }

            return response;
        }

        private async Task<ResponseBase> GetMockResponse()
        {
            var response = new CoindeskResponse();
            try
            {
                string projectRoot = Directory.GetCurrentDirectory();
                string mockFilePath = Path.Combine(projectRoot, "mock_response.json");
                string chineseNameFilePath = Path.Combine(projectRoot, "currencies_chinese_name.json");

                if (!File.Exists(mockFilePath))
                {
                    response.setResult(ResultEnums.NotFindDocument);
                }

                string mockJson = await File.ReadAllTextAsync(mockFilePath);
                var coindeskData = JsonConvert.DeserializeObject<CoindeskApiDto>(mockJson);

                Dictionary<string, string> currencyMappings = await MappingChineseNameAsync(chineseNameFilePath);

                if (coindeskData != null)
                {
                    response = new CoindeskResponse
                    {
                        UpdateTime = coindeskData.Time.UpdatedISO,
                        Currencies = coindeskData.Bpi.Select(x => new CurrencyInfoDto
                        {
                            Code = x.Value.Code,
                            Name = currencyMappings.ContainsKey(x.Value.Code)
                                ? currencyMappings[x.Value.Code] 
                                : x.Value.Description, 
                            Rate = x.Value.RateFloat
                        }).ToList()
                    };
                }
            }
            catch (Exception ex)
            {
                var responseBase = new ResponseBase();
                responseBase.setException(ex);
                return responseBase;
            }

            return response;
        }

        private static async Task<Dictionary<string, string>> MappingChineseNameAsync(string chineseNameFilePath)
        {
            var currencyMappings = new Dictionary<string, string>();
            if (File.Exists(chineseNameFilePath))
            {
                string chineseJson = await File.ReadAllTextAsync(chineseNameFilePath);
                var mappingData = JsonConvert.DeserializeObject<CurrencyMappingDto>(chineseJson);
                if (mappingData != null)
                {
                    currencyMappings = mappingData.Currencies.ToDictionary(c => c.Code, c => c.ChineseName);
                }
            }
            return currencyMappings;
        }
    }
}
