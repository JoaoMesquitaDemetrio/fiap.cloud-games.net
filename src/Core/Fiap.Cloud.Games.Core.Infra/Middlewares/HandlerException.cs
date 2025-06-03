using System.Net;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Filters;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Fiap.Cloud.Games.Core.Infra.Middlewares;

public class HandlerException
{
    private readonly ICorrelationIdGenerator _correlationIdGenerator;

    public HandlerException(ICorrelationIdGenerator correlationIdGenerator)
        => _correlationIdGenerator = correlationIdGenerator;

    public ExceptionResult ResponseException(Exception exception)
    {
        switch (exception)
        {
            case NotImplementedException _:
                return ResponseException(HttpStatusCode.Unauthorized, "Sem permissão para esta operação");

            case AggregateException _:
                var aggrEx = (AggregateException)exception;
                return ResponseException(HttpStatusCode.BadRequest, aggrEx.InnerExceptions.ToArray());

            case ArgumentException _:
                return ResponseException(HttpStatusCode.BadRequest, exception.Message);

            case TaskCanceledException _:
            case OperationCanceledException _:
                return ResponseException(HttpStatusCode.BadRequest, "Operação cancelada pelo usuário.");

            default:
                return ResponseException(HttpStatusCode.InternalServerError, exception.Message);
        }
    }

    public ExceptionResult ResponseException(HttpStatusCode statusCode, string message)
    {
        var error = new ResponseErrorContent((int)statusCode, message);
        var responseError = new ResponseError(error);

        return new ExceptionResult(_correlationIdGenerator.Get(), statusCode, responseError);
    }

    public ExceptionResult ResponseException(HttpStatusCode statusCode, Exception[] exceptions)
    {
        var responseError = new ResponseErrorAggregate();
        foreach (var exception in exceptions)
            responseError.Errors.Add(new ResponseErrorContent((int)statusCode, exception.Message));

        return new ExceptionResult(_correlationIdGenerator.Get(), statusCode, responseError);
    }
}

public static class HandlerExceptionExtensions
{
    public const string X_CORRELATION_ID_HEADER = "X-Correlation-Id";

    public static void SetupHandlerException(this IServiceCollection services)
        => services.AddTransient<HandlerException>();
    
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var logger = context.RequestServices.GetService<IBaseLogger<HandlerException>>();
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    var handler = context.RequestServices.GetService<HandlerException>();
                    var result = handler.ResponseException(contextFeature.Error);
                    var serializedResponse = JsonConvert.SerializeObject(result.Response);
                    logger.LogError(serializedResponse);

                    context.Response.Headers[X_CORRELATION_ID_HEADER] = result.CorrelationId;
                    context.Response.StatusCode = (int)result.StatusCode;
                    await context.Response.WriteAsync(serializedResponse);
                }
                else
                {
                    var correlationIdGenerator = context.RequestServices.GetService<ICorrelationIdGenerator>();
                    context.Response.Headers[X_CORRELATION_ID_HEADER] = correlationIdGenerator.Get();
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync("Internal Server Error");
                }
            });
        });
    }
}

public record ResponseErrorContent(int Code, string Message)
{
    public override string ToString()
        => $"{Code} - {Message}";
}

public record ResponseErrorAggregate(IList<ResponseErrorContent> Errors = default)
{
    public ResponseErrorAggregate() : this(new List<ResponseErrorContent>()) { }
}

public record ResponseError(ResponseErrorContent Error)
{
    public override string ToString()
        => Error?.ToString() ?? "";
}

public record ExceptionResult(string CorrelationId, HttpStatusCode StatusCode, object Response) { }