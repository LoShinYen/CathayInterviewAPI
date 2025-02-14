using Microsoft.AspNetCore.Mvc;

namespace CathayInterviewAPI.Helpers
{
    public static class ResponseHelper
    {
        public static IActionResult CreateResponse(ResponseBase? responseModel)
        {
            if (responseModel == null)
            {
                responseModel = new ResponseBase
                {
                    Message = "No Data",
                    ResultCode = ResultEnums.ElseError.Code
                };
                return new BadRequestObjectResult(responseModel);
            }

            // 確保 Message 不為空
            if (string.IsNullOrEmpty(responseModel.Message))
            {
                responseModel.Message = "Unknown Error";
            }

            // 成功回應
            if (responseModel.ResultCode == ResultEnums.Success.Code ||
                responseModel.Message.Contains(ResultEnums.Success.Message))
            {
                return new OkObjectResult(responseModel);
            }

            // 失敗回應
            return new BadRequestObjectResult(responseModel);
        }
    }
}
