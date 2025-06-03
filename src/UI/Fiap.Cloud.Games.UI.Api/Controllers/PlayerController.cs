using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fiap.Cloud.Games.Core.Application.DataTransferObjects;
using Fiap.Cloud.Games.Core.Application.DataTransferObjects.HttpResponse;
using Fiap.Cloud.Games.Core.Application.Interfaces;

namespace Fiap.Cloud.Games.UI.Api.Controllers;

[ApiController]
[Route("players")]
[Produces("application/json")]
public class PlayerController(IPlayerApplicationService playerApplicationService) : BaseController, IDisposable
{
    /// <summary>
    /// Insere um player usuário no sistema.
    /// </summary>
    /// <param name="model">Dados do player <see cref="PlayerInsert"</param>
    /// <returns>ok <see cref="StatusCodes"/></returns>
    [HttpPost, Route("common")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResult), StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ExceptionResult))]
    public async Task<object> InsertPlayerAsUser([FromBody] PlayerInsert model)
    {
        await playerApplicationService.InsertAsUser(model);
        return ResponseResult();
    }

    /// <summary>
    /// Insere um player administrator no sistema.
    /// </summary>
    /// <param name="model">Dados do player <see cref="PlayerInsert"</param>
    /// <returns>ok <see cref="StatusCodes"/></returns>
    [HttpPost, Route("administrator")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(StatusCodes), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResult), StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ExceptionResult))]
    public async Task<object> InsertAsAdministrator([FromBody] PlayerInsert model)
    {
        await playerApplicationService.InsertAsAdministrator(model);
        return ResponseResult();
    }

    [NonAction]
    public void Dispose()
    {
        playerApplicationService?.Dispose();

        GC.SuppressFinalize(this);
    }
}
