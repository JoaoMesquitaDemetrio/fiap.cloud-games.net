using System.Text;
using Fiap.Cloud.Games.Core.Domain.Interfaces.Infra.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Sample.Utils.Extensions;

namespace Fiap.Cloud.Games.Core.Infra.Middlewares;

public class MessageLogger
{
    private readonly RequestDelegate _next;

    public MessageLogger(RequestDelegate next)
        => _next = next;

    public async Task Invoke(HttpContext context, IBaseLogger<MessageLogger> logger)
    {
        await LogRequest(context.Request, logger);
        await _next(context);
    }

    public async Task LogRequest(HttpRequest request, IBaseLogger<MessageLogger> logger)
    {
        var body = await ReadBodyStreamAsStringFromRequest(request);
        var requestLine = string.Format("{0} {1} {2} {3}",
            request.Method,
            request.Path,
            request.Headers["Authorization"],
            request.Headers.GetCustomHeader()
        ).Trim();

        logger.LogInformation(string.Format("Request {0} {1}", requestLine, body));
    }

    private async Task<string> ReadBodyStreamAsStringFromRequest(HttpRequest request)
    {
        try
        {
            using (var streamReader = new StreamReader(request.Body))
            {
                if (streamReader.IsNull())
                {
                    return string.Empty;
                }
                else
                {
                    var json = await streamReader.ReadToEndAsync();
                    var trimmedJson = json.TrimJson();
                    var byteArray = Encoding.UTF8.GetBytes(trimmedJson);

                    request.Body = new MemoryStream(byteArray);

                    return trimmedJson;
                }
            }
        }
        catch
        {
            return string.Empty;
        }
    }
}

public static class MessageLoggerExtensions
{
    public static IApplicationBuilder UseMessageLogger(this IApplicationBuilder app)
        => app.UseMiddleware<MessageLogger>();
        
    public static string GetCustomHeader(this IHeaderDictionary header)
    {
        try
        {
            var startWith = header.Where(h => h.Key.StartsWith("X-"));
            var extraHeaders = string.Join(" ", startWith.Select(h => string.Format("{0}={1}", h.Key, string.Join(" ", h.Value).Trim())));

            return extraHeaders.Trim();
        }
        catch
        {
            // Always return all zeroes for any failure (my calling code expects it)
        }

        return string.Empty;
    }
}
