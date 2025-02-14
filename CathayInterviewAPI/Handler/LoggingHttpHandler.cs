using System.Diagnostics;

namespace CathayInterviewAPI.Handler
{
    public class LoggingHttpHandler : DelegatingHandler
    {
        private readonly ILogger<LoggingHttpHandler> _logger;

        public LoggingHttpHandler(ILogger<LoggingHttpHandler> logger)
        {
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                // 記錄請求
                string requestBody = request.Content != null ? await request.Content.ReadAsStringAsync() : "";
                _logger.LogInformation($"[OUTBOUND REQUEST] {request.Method} {request.RequestUri} | Body: {requestBody}");

                var stopwatch = Stopwatch.StartNew();
                var response = await base.SendAsync(request, cancellationToken);
                stopwatch.Stop();

                // 記錄回應
                string responseBody = response.Content != null ? await response.Content.ReadAsStringAsync() : "";
                _logger.LogInformation($"[OUTBOUND RESPONSE] {request.Method} {request.RequestUri} | Status: {response.StatusCode} | Time: {stopwatch.ElapsedMilliseconds}ms | Body: {responseBody}");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"[API Error]{ex.Message}");
                throw;
            }
        }
    }
}
