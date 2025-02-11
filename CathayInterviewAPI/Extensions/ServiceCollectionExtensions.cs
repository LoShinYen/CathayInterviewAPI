using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace CathayInterviewAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExternalApiHttpClient(this IServiceCollection services)
        {
            services.AddTransient<Handler.LoggingHttpHandler>(); // 註冊 LoggingHttpHandler

            services.AddHttpClient("ExternalAPI", client =>
            {
                client.BaseAddress = new Uri("https://api.coindesk.com/");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler())
            .AddHttpMessageHandler<Handler.LoggingHttpHandler>();

            return services;
        }
    }
}
