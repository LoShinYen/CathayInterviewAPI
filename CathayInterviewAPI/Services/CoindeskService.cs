using CathayInterviewAPI.Models.Responses;
using Newtonsoft.Json;

namespace CathayInterviewAPI.Services
{
    public class CoindeskService(IHttpClientFactory httpClientFactory) : ICoindeskService
    {
        const string queryUrl = "v1/bpi/currentprice.json";
        private string _mockFilePath = string.Empty;
        private string _chineseNameFilePath = string.Empty;

        public async Task<ResponseBase> QueryCoinDeskInfo()
        {
            var response = new ResponseBase();
            try
            {
                FethFilePath();

                var client = httpClientFactory.CreateClient("ExternalAPI");
                var apiResponse = await client.GetAsync(queryUrl);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var jsonResponse = await apiResponse.Content.ReadAsStringAsync();
                    response = await ProcessResponseAsync(jsonResponse);
                }
                else
                {
                    response.setResult(ResultEnums.CoindeskApiError);
                }
            }
            catch (HttpRequestException)
            {
                response = await GetMockResponseAsync();
            }
            catch (Exception ex)
            {
                response.setException(ex);
            }

            return response;
        }

        private async Task<CoindeskResponse> ProcessResponseAsync(string jsonResponse)
        {
            var response = new CoindeskResponse();

            try
            {
                var coindeskData = JsonConvert.DeserializeObject<CoindeskApiDto>(jsonResponse);
                Dictionary<string, string> currencyMappings = await MappingChineseNameAsync(_chineseNameFilePath);

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
                response.setException(ex);
            }

            return response;
        }

        private void FethFilePath()
        {
            string projectRoot = "/app"; // 在 Docker 容器中 JSON 存放的目錄

            _mockFilePath = Path.Combine(projectRoot, "mock_response.json");
            _chineseNameFilePath = Path.Combine(projectRoot, "currencies_chinese_name.json");
        }

        private async Task<ResponseBase> GetMockResponseAsync()
        {
            if (!File.Exists(_mockFilePath))
            {
                var errorResponse = new ResponseBase();
                errorResponse.setResult(ResultEnums.NotFindDocument);
                return errorResponse;
            }

            string mockJson = await File.ReadAllTextAsync(_mockFilePath);
            return await ProcessResponseAsync(mockJson);
        }


        private async Task<Dictionary<string, string>> MappingChineseNameAsync(string chineseNameFilePath)
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
