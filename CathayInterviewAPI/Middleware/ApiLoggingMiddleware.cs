using System.Text;

namespace CathayInterviewAPI.Middleware
{
    public class ApiLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ApiLoggingMiddleware> _logger;

        public ApiLoggingMiddleware(RequestDelegate next, ILogger<ApiLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                // 記錄請求
                var requestBody = await ReadRequestBody(context.Request);
                _logger.LogInformation($"[API REQUEST] {context.Request.Method} {context.Request.Path} | Body: {requestBody}");

                // 記錄回應
                var originalBodyStream = context.Response.Body;
                using var responseBodyStream = new MemoryStream();
                context.Response.Body = responseBodyStream;

                await _next(context);

                var responseBody = await ReadResponseBody(context.Response);
                _logger.LogInformation($"[API RESPONSE] {context.Request.Method} {context.Request.Path} | Status: {context.Response.StatusCode} | Body: {responseBody}");

                // 把 Response Body 寫回原始 Stream，確保 API 能正常回應
                await responseBodyStream.CopyToAsync(originalBodyStream);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[ERROR] {context.Request.Method} {context.Request.Path} 發生錯誤");

                // 設定回應為 500 錯誤，並回傳錯誤訊息
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync($"{{\"error\": \"{ex.Message}\"}}");
            }
        }

        private async Task<string> ReadRequestBody(HttpRequest request)
        {
            request.EnableBuffering(); // 允許多次讀取 Request.Body

            using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            request.Body.Position = 0; // **重設位置，確保後續 Middleware 能讀取 Request.Body**

            return body;
        }

        private async Task<string> ReadResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            using var reader = new StreamReader(response.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);
            return body;
        }
    }
}
