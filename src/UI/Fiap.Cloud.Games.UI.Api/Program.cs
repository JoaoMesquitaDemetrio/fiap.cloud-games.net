using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using FluentValidation.AspNetCore;
using Fiap.Cloud.Games.Core.Ioc;
using Fiap.Cloud.Games.Core.Domain.Settings;
using Fiap.Cloud.Games.UI.Api.Extensions;
using Fiap.Cloud.Games.UI.Api.Components;
using Fiap.Cloud.Games.Core.Infra.Filters;
using Fiap.Cloud.Games.Core.Infra.Middlewares;
using Fiap.Cloud.Games.Core.Infra.Repositories.EF;
using Fiap.Cloud.Games.Core.Application.Validations;
using Sample.Utils.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.SetupSettings(builder.Configuration);
builder.Services.SetupIoC();
builder.Services.SetupSQLDatabase();
builder.Services.AddControllers(options =>
{
    options.AllowEmptyInputInBodyModelBinding = true;
    options.SetupGlobalRoutePrefix(new RouteAttribute("api"));
    options.Filters.AddHttpInterceptionCorrelation();
    options.Filters.AddValidateModelState();
})
.AddNewtonsoftJson(options => options.SerializerSettings.SetDefaultJsonSerializerSettings());

builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
builder.Services.AddFluentValidationAutoValidation(options => options.DisableDataAnnotationsValidation = true);
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<PlayerInsertValidation>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SchemaFilter<SnakeCaseSchemaFilter>());
builder.WebHost.UseDefaultServiceProvider(options => options.ValidateScopes = false);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => c.RoutePrefix = "swagger");

app.UseCustomExceptionHandler();
app.UseMessageLogger();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.UseCors();

app.MapControllers();

app.Run();

