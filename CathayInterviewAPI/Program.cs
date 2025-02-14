using CathayInterviewAPI.Extensions;
using CathayInterviewAPI.Middleware;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromFile("NLog.config").GetCurrentClassLogger();
try
{
    logger.Info("應用程式啟動中...");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureNLog();
    builder.Services.AddExternalApiHttpClient();
    builder.Services.AddEncryptedDbContext(builder.Configuration);
    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastructureRepositories();
    builder.Services.AddCustomAutoMapper();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    var app = builder.Build();
    app.UseRouting();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseMiddleware<ApiLoggingMiddleware>();

    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error($"應用程式啟動失敗,{ex.Message}");
}
finally
{
    LogManager.Shutdown();
}