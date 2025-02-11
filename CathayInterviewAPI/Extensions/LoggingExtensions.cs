using NLog.Web;

namespace CathayInterviewAPI.Extensions
{
    public static class LoggingExtensions
    {
        public static IHostBuilder ConfigureNLog(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureLogging(logging =>
            {
                logging.ClearProviders();
                logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
            })
            .UseNLog(); // 設定 NLog

            return hostBuilder;
        }
    }
}
