using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UserManagementAPI.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request method and path
            var method = context.Request.Method;
            var path = context.Request.Path;

            await _next(context);

            // Log response status code
            var statusCode = context.Response.StatusCode;

            _logger.LogInformation("HTTP {Method} {Path} responded {StatusCode}", method, path, statusCode);
        }
    }
}