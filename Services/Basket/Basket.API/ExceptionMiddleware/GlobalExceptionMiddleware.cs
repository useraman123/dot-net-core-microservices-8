﻿using System.Net;
using System.Text.Json;
namespace Basket.API.ExceptionMiddleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Something went wrong: {ex.Message}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorDetails = new
        {
            StatusCode = context.Response.StatusCode,
            Message = exception.Message,
            StackTrace = exception.StackTrace,
            ExceptionType = exception.GetType().Name
        };

        var result = JsonSerializer.Serialize(errorDetails);

        return context.Response.WriteAsync(result);
    }
}
