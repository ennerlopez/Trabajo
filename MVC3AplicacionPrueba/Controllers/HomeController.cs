using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC3AplicacionPrueba.Models;

namespace MVC3AplicacionPrueba.Controllers
{
    public class HomeController : Controller
    {
        [Authorize()]
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult menu()
        {
            DataContextoDataContext d = new DataContextoDataContext();

            var query = from a in d.VistaModulos
                        where a.codigoUsuario == 2 
                        select a;
            
            
            return View(query);
        }
    }
}
