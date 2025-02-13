namespace CathayInterviewAPI.Models.BaseModels
{
    public class ResponseBase
    {
        public int ResultCode { get; set; }

        public string Message { get; set; }

        public LanguageBase ResultLanguages { get; set; }

        public ResponseBase()
        {
            ResultCode = ResultEnums.Success.Code;
            Message = ResultEnums.Success.Message;
            ResultLanguages = new LanguageBase  
            {
                En = ResultEnums.Success.ResultLanguages.En,
                Ch = ResultEnums.Success.ResultLanguages.Ch,
            };
        }

        public void setResult(ResultBase r)
        {
            ResultCode = r.Code;
            Message = r.Message;
            ResultLanguages = new LanguageBase
            {
                En = r.ResultLanguages.En,
                Ch = r.ResultLanguages.Ch
            };
        }

        public void setException(Exception ex)
        {
            ResultCode = ResultEnums.ElseError.Code;
            if (ex.InnerException != null)
                Message = ex.InnerException.Message;
            else
                Message = ex.Message;
        }

    }
}
