namespace CathayInterviewAPI.Services
{
    public interface ICoindeskService
    {
        Task<ResponseBase> QueryCoinDeskInfo();
    }
}
