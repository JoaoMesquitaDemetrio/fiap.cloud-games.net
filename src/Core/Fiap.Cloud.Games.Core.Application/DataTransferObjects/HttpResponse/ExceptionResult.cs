using System.Net;

namespace Fiap.Cloud.Games.Core.Application.DataTransferObjects.HttpResponse;

public record ExceptionResult(HttpStatusCode StatusCode, object Response) { }
