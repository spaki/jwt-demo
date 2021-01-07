using JWTDemoClient.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace JWTDemoClient.Attributes
{
    public class AuthorizeOnJwtSourceAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated) // -> first, we need to check if we have some toeken
            {
                var authorizationService = context.HttpContext.RequestServices.GetRequiredService<AuthorizationService>();

                string token = context.HttpContext.Request.Headers["Authorization"];
                bool isTokenValid = await authorizationService.IsTokenValid(token);

                if(!isTokenValid)
                    context.Result = new UnauthorizedResult();
            }
        }
    }
}
