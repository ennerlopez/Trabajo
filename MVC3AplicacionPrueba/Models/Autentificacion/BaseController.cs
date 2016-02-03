using System.Linq;
using System.Web.Mvc;

namespace MVC3AplicacionPrueba.Models.Autentificacion
{
    public class BaseController : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
   
            var rd = this.RouteData;
          
            var currentAction = rd.GetRequiredString("action");
            var currentController = rd.GetRequiredString("controller");


            if (SimpleSessionPersister.funciones != null)
            {
                if (SimpleSessionPersister.funciones.Any())
                {

                    var principal = filterContext.HttpContext.User;
                 
                    if (principal.Identity.Name.ToUpper() == "ROOT" && principal.Identity.IsAuthenticated)
                    {
                    }
                    else
                    {
                        filterContext.HttpContext.User =
                            new CustomPrincipal(
                                new CustomIdentity(principal.Identity.Name, currentController, currentAction));

                    }
                }
            }
            base.OnAuthorization(filterContext);
        }
    }
}