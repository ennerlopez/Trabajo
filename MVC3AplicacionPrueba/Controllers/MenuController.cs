using System.Linq;
using System.Web.Mvc;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class MenuController : BaseController 
    {
        //
        // GET: /Menu/

      

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult menuModulos(int idUsuario)
        {
            DataContextoDataContext d = new DataContextoDataContext();

            var query = from a in d.VistaModulos
                        where a.codigoUsuario == idUsuario
                        select a;


            return Json(query.ToList());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult menuMenus(int idModulo)
        {
            return Json(Data.obtenerLista(idModulo,2));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult menuFunciones(int idMenu)
        {
            return Json(null);
        }

    }
}
