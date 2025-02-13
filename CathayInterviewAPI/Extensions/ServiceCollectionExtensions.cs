using CathayInterviewAPI.Helpers;
using CathayInterviewAPI.Mappings;
using CathayInterviewAPI.Repositories;
using CathayInterviewAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace CathayInterviewAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExternalApiHttpClient(this IServiceCollection services)
        {
            services.AddTransient<Handler.LoggingHttpHandler>();

            services.AddHttpClient("ExternalAPI", client =>
            {
                client.BaseAddress = new Uri("https://api.coindesk.com/");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler())
            .AddHttpMessageHandler<Handler.LoggingHttpHandler>();

            return services;
        }

        public static IServiceCollection AddEncryptedDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var encryptedConnectionString = configuration.GetConnectionString("DefaultConnection");
            if (!string.IsNullOrEmpty(encryptedConnectionString))
            {
                var decryptedConnectionString = EncryptionHelper.Decrypt(encryptedConnectionString);

                services.AddDbContext<CathayInterviewDBContext>(options =>
                    options.UseSqlServer(decryptedConnectionString));
            }
            else
            { 
                throw new Exception("Connection string is empty.");
            }

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyService, CurrencyService>();

            return services;
        }

        public static IServiceCollection AddInfrastructureRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyRepository, CurrencyRepositroy>();

            return services;
        }

        public static IServiceCollection AddCustomAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CurrencyProfile));

            return services;
        }

    }
}
