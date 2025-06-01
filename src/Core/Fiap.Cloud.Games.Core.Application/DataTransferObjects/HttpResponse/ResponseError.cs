namespace Fiap.Cloud.Games.Core.Application.DataTransferObjects.HttpResponse;

public record ResponseError(ResponseErrorContent Error)
{
    public override string ToString()
        => Error?.ToString() ?? "";
}