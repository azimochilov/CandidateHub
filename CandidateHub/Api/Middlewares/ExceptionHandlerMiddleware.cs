using CandidateHub.Service.Exceptions;

namespace CandidateHub.Api.Middlewares;
public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlerMiddleware> logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (CandidateHubException exception)
        {
            context.Response.StatusCode = exception.Code;
            await context.Response.WriteAsJsonAsync(new Response<Object>
            {
                Code = exception.Code,
                Message = exception.Message
            });
        }
        catch (Exception exception)
        {
            this.logger.LogError($"{exception}\n\n");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Response<Object>
            {
                Code = 500,
                Message = exception.Message
            });
        }
    }
}
