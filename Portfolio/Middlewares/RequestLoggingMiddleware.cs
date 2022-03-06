namespace Portfolio.Middlewares

{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    ">>> Method: {method} \n" +
                    ">>> Headers: {headers} \n" +
                    ">>> Body: {body} \n" +
                    ">>> Url: {url} \n" +
                    "!!! StatuseCode: {statusCode} \n" +
                    "!!! Headers: {headers}\n",
                    context.Request?.Method,
                    context.Request?.Headers,
                    context.Request?.Body,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode,
                    context.Response?.Headers);
            }
        }
    }
}
