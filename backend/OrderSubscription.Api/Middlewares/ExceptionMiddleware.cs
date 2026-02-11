using System.Net;
using System.Text.Json;
using OrderSubscription.Api.Responses;

namespace OrderSubscription.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
    {
        _next = next;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = _env.IsDevelopment()
            ? ApiResponse<object>.Fail(ex.Message)
            : ApiResponse<object>.Fail("Something went wrong. Please try again later.");

        var json = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(json);
    }
}
