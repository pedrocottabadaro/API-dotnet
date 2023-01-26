using DDDBasico.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace DDDBasico.Application.Middleware
{
    public class AuthorizeByUserIdAttribute : ActionFilterAttribute
    {

        public AuthorizeByUserIdAttribute()
        {
       
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var tokenService = context.HttpContext.RequestServices.GetService<ITokenService>();
            var userId = tokenService.ReturnIdToken(context.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", ""));
            var routeId = context.RouteData.Values["id"].ToString();

            if (userId != routeId)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }



}
