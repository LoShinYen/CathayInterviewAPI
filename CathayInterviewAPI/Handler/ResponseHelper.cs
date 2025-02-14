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

            if (string.IsNullOrEmpty(responseModel.Message))
            {
                responseModel.Message = "Unknown Error";
            }
            if (responseModel.ResultCode == ResultEnums.Success.Code ||
                responseModel.Message.Contains(ResultEnums.Success.Message))
            {
                return new OkObjectResult(responseModel);
            }

            return new BadRequestObjectResult(responseModel);
        }
    }
}
