using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fiap.Cloud.Games.Core.Domain.Enums;
using Fiap.Cloud.Games.Core.Domain.Extensions;
using Fiap.Cloud.Games.Core.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Sample.Utils.Extensions;

namespace Fiap.Cloud.Games.Core.Infra.Filters;

public static class JWTAthentication
{
    public static void SetupJWT(this IServiceCollection services, string JwtIssuer, string JwtKey)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey))
            };
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppConstants.Policies.ADMINISTRATOR, policy => policy.RequireRole(AppConstants.Roles.ADMINISTRATOR));
            options.AddPolicy(AppConstants.Policies.ADMINISTRATOR, policy => policy.RequireRole(AppConstants.Roles.USER));
        });
    }

    public static string GenerateToken(this string username, TypePlayer role, AppSettings appSettings)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(ClaimTypes.Role, role.GetDescription()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: appSettings.Jwt.Issuer,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

