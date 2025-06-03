using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fiap.Cloud.Games.Core.Application.DataTransferObjects;
using Fiap.Cloud.Games.Core.Application.DataTransferObjects.HttpResponse;
using Fiap.Cloud.Games.Core.Application.Interfaces;

namespace Fiap.Cloud.Games.UI.Api.Controllers;

[ApiController]
[Route("games")]
[Produces("application/json")]
public class GameController(IGameApplicationService gameApplicationService) : BaseController, IDisposable
{
    /// <summary>
    /// Insere um game no sistema.
    /// </summary>
    /// <param name="model">Dados do game <see cref="GameInsert"</param>
    /// <returns>Dados do game persistido <see cref="GameUpdate"/></returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResult), StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ExceptionResult))]
    public async Task<object> InsertGame([FromBody] GameInsert model)
    {
        var result = await gameApplicationService.Insert(model);
        return ResponseResult(result);
    }

    /// <summary>
    /// Atualiza um game no sistema.
    /// </summary>
    /// <param name="model">Dados do game <see cref="GameUpdate"</param>
    /// <returns>Dados do game persistido <see cref="GameUpdate"/></returns>
    [HttpPut]
    [AllowAnonymous]
    [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResult), StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ExceptionResult))]
    public async Task<object> UpdateGame([FromBody] GameUpdate model)
    {
        var result = await gameApplicationService.Update(model);
        return ResponseResult(result);
    }

    [NonAction]
    public void Dispose()
    {
        gameApplicationService?.Dispose();

        GC.SuppressFinalize(this);
    }
}
