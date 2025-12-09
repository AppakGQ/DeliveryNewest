namespace DeliveryNew.Middleware
{
    // Middleware to log details of every HTTP request and response.
    // This intercepts the request pipeline.
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
            
        // The core logic of the middleware.
        public async Task InvokeAsync(HttpContext context)
        {
            // 1. Log the incoming request details (Method and Path) BEFORE passing to the next middleware.
            _logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
            
            // 2. Pass control to the next middleware in the pipeline.
            await _next(context);
            
            // 3. Log the response status code AFTER the rest of the pipeline has finished processing.
            _logger.LogInformation("Response: {StatusCode}", context.Response.StatusCode);
        }
    }

    public static class RequestLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}
