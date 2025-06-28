using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace UserManagementAPI.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TokenValidationMiddleware> _logger;
        private const string AUTH_HEADER = "Authorization";
        private const string BEARER_PREFIX = "Bearer ";
        // For demonstration, use a hardcoded valid token. Replace with real validation in production.
        private const string VALID_TOKEN = "your-secure-token";

        public TokenValidationMiddleware(RequestDelegate next, ILogger<TokenValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                if (!context.Request.Headers.TryGetValue(AUTH_HEADER, out var authHeader) ||
                    !authHeader.ToString().StartsWith(BEARER_PREFIX) ||
                    authHeader.ToString()[BEARER_PREFIX.Length..] != VALID_TOKEN)
                {
                    _logger.LogWarning("Unauthorized request to {Path}", context.Request.Path);
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsJsonAsync(new { error = "Unauthorized" });
                    return;
                }
            }

            await _next(context);
        }
    }
}