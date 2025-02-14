namespace CathayInterviewAPI.Helpers
{
    public static class ValidationHelper
    {
        public static ResponseBase? ValidateStringValue(string inputStr, ResultBase errorResult)
        {
            if (string.IsNullOrEmpty(inputStr))
            {
                var respone = new ResponseBase();
                respone.setResult(errorResult);
                return respone;
            }
            return null;
        }
    }
}
