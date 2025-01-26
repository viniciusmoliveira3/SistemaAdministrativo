using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;


namespace Colex.Configs
{
    public class ValidacaoAutenticacao : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var rotas = context.ActionDescriptor.RouteValues.Values.ToList();
            string actions = rotas[0].ToLower();
            string controller = rotas[1].ToLower();

            if (context.HttpContext.Session.GetInt32("IdUsuario") == null && controller != "login")
            {
                context.Result = new RedirectToRouteResult
                (
                    new RouteValueDictionary
                    {
                        {"controller", "login" },
                        {"action", "Login" }

                    }
                );
            }
            else if (controller == "login" && context.HttpContext.Session.GetInt32("IdUsuario") != null)
            {
                context.Result = new RedirectToRouteResult
                  (
                        new RouteValueDictionary
                        {
                           {"controller", "Home" },
                           {"action", "Index" }

                        }
                   );
            }
        }

    }
}
