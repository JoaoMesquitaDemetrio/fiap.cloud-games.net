using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Fiap.Cloud.Games.Core.Infra.Filters;

public class ValidateModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        try
        {
            if (context.ModelState.IsValid == false)
            {
                var exceptions = new List<Exception>();
                var errors = context.ModelState.Values
                    .Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();

                foreach (var error in errors)
                    exceptions.Add(new Exception(error));

                throw new AggregateException(exceptions);
            }
        }
        catch (AggregateException ex)
        {
            Debug.Write(ex.ToString());
            throw;
        }
        catch (Exception ex)
        {
            Debug.Write(ex.ToString());
            throw new Exception("Erro não esperado");
        }
    }
}

public static class ValidateModelStateAttributeExtensions
{
    public static void AddValidateModelState(this FilterCollection filters)
        => filters.Add(typeof(ValidateModelStateAttribute));
}
