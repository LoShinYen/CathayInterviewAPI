namespace CathayInterviewAPI.Models.BaseModels
{
    public class ResultBase
    {
        private int code;

        private string message;

        private string messageCh;

        public int Code { get { return code; } }

        public string Message { get { return message; } }

        public LanguageBase ResultLanguages { get { return new LanguageBase { Ch = messageCh, En = message }; } }

        public ResultBase(int code, string message, string ch = "")
        {
            this.code = code;
            this.message = message;
            this.messageCh = ch;
        }
    }
}
