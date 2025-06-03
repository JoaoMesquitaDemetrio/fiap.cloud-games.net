using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Fiap.Cloud.Games.Core.Application.DataTransferObjects;
using Fiap.Cloud.Games.Core.Application.DataTransferObjects.HttpResponse;
using Microsoft.Extensions.Options;
using Fiap.Cloud.Games.Core.Domain.Settings;
using Fiap.Cloud.Games.Core.Infra.Filters;

namespace Fiap.Cloud.Games.UI.Api.Controllers;

[ApiController]
[Route("auth")]
[Produces("application/json")]
public class AuthController(IOptionsSnapshot<AppSettings> options) : BaseController, IDisposable
{
    private readonly AppSettings _appSettings = options.Value;

    /// <summary>
    /// Gera um token válido .
    /// </summary>
    /// <param name="model">Dados para gerar o token <see cref="PlayerAuthentication"</param>
    /// <returns>Beare token</returns>
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ExceptionResult), StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ExceptionResult))]
    public IActionResult Login([FromBody] PlayerAuthentication model)
    {
        if (string.IsNullOrEmpty(model.Username))
            return BadRequest("Username and password are required.");

        var token = model.Username.GenerateToken(model.TypePlayer, _appSettings);
        return Ok(new { token });
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
