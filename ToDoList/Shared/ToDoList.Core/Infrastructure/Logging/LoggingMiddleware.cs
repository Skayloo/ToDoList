using FluentValidation;
using Microsoft.AspNetCore.Http;
using Serilog.Events;
using System.Net;
using Newtonsoft.Json;

namespace ToDoList.Core.Infrastructure.Logging;

public class LoggingMiddleware
{
    private const string MessageTemplate = "{Host} {RequestMethod} {RequestPath} {StatusCode}";

    private readonly Serilog.ILogger _log = Serilog.Log.ForContext<LoggingMiddleware>();
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task Invoke(HttpContext httpContext)
    {
        if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

        try
        {
            await _next(httpContext);
            var statusCode = httpContext.Response?.StatusCode;
            if (statusCode != null)
            {
                var level = statusCode > 399 ? LogEventLevel.Error : LogEventLevel.Information;
                var log = level == LogEventLevel.Error
                    ? LogForErrorContext(httpContext)
                    : LogInfoContext(httpContext);
                log.Write(level, MessageTemplate, httpContext.Request.Host, httpContext.Request.Method,
                    httpContext.Request.Path, statusCode);
            }
            else
            {
                var log = LogForErrorContext(httpContext);
                log.Warning("Can't get Response status code!");
            }
        }
        catch (ValidationException ex)
        {
            await LogValidationException(httpContext, ex);
        }
        catch (Exception ex)
        {
            await LogApplicationException(httpContext, ex);
        }
    }

    private async Task<bool> LogValidationException(HttpContext httpContext, ValidationException ex)
    {
        if (httpContext.Response.HasStarted)
        {
            _log.Warning(ex, "The response has already started, the http status code middleware will not be executed.");
            return false;
        }

        var log = LogForErrorContext(httpContext);
        log.Error(ex, $"Validation error: {string.Join(";", ex.Errors)}");

        httpContext.Response.Clear();
        httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(ValidationErrors.Create(ex)));

        return false;
    }

    private async Task<bool> LogApplicationException(HttpContext httpContext, Exception ex)
    {
        if (httpContext.Response.HasStarted)
        {
            _log.Warning(ex, "The response has already started, the http status code middleware will not be executed.");
            return false;
        }

        var log = LogForErrorContext(httpContext);
        log.Fatal(ex, "Fatal error");

        httpContext.Response.Clear();
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        await httpContext.Response.WriteAsync("Internal Server Error");

        return false;
    }

    private Serilog.ILogger LogForErrorContext(HttpContext httpContext)
    {
        var request = httpContext.Request;
        var result = LogInfoContext(httpContext);
        return result;
    }

    private Serilog.ILogger LogInfoContext(HttpContext httpContext)
    {
        var request = httpContext.Request;
        var result = _log;
        return result;
    }
}