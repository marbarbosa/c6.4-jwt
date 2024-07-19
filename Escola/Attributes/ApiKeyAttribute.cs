using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Escola.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        //localhost:5001?api_key=CHAVE
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //esse IF tenta pegar o parametro com o mesmo nome do ApiKeyName, e se achar preenche a variavel key
            if(context.HttpContext.Request.Query.TryGetValue(Configuration.ApiKeyName, out var ValorKey) == false) 
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "ApiKey não encontrada"
                };
                return;
            }

            if(Configuration.ApiKey.Equals(ValorKey) == false)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 403,
                    Content = "Acesso não autorizado"
                };
                return;
            }

            await next();
        }
    }
}
