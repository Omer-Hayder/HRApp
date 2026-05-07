using Serilog;

namespace API.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (FluentValidation.ValidationException ex)
            {
                logger.LogWarning(ex, "Validation error");
                Log.Logger.Warning(ex, "Validation error");

                context.Response.StatusCode = 400;

                await context.Response.WriteAsJsonAsync(new
                {
                    errors = ex.Errors.Select(e => e.ErrorMessage)
                });
            }



            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception");
                Log.Logger.Error(ex, "Unhandled exception");

                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    status = context.Response.StatusCode,
                    message = "Something went wrong",
                    traceId = context.TraceIdentifier,
                    timestamp = DateTime.UtcNow,
                    path = context.Request.Path.Value
                });
            }
        }
    }
}
