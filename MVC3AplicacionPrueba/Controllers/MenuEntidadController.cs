using System.Linq;
using System.Web.Mvc;
using  MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class MenuEntidadController : BaseController 
    {
        //
        // GET: /MenuEntidad/
        private DataContextoDataContext contexto = new DataContextoDataContext();

        public ActionResult Index()
        {
            return View(contexto.MenuEntidads);
        }

        //
        // GET: /MenuEntidad/Details/5

        public ActionResult Details(int id)
        {
            return View(contexto.MenuEntidads.Single(x => x.codigoMenu == id));
        }

        //
        // GET: /MenuEntidad/Create

        public ActionResult Create()
        {
            
            return View();
        } 

        //
        // POST: /MenuEntidad/Create

        [HttpPost]
        public ActionResult Create(MenuEntidad menu)
        {
            try
            {
                contexto.MenuEntidads.InsertOnSubmit(menu);
                contexto.SubmitChanges();
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /MenuEntidad/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(contexto.MenuEntidads.Single(x=>x.codigoMenu ==id));
        }

        //
        // POST: /MenuEntidad/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                MenuEntidad menu = contexto.MenuEntidads.Single(x => x.codigoMenu == id);

                TryUpdateModel(menu, collection);
                contexto.SubmitChanges();
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /MenuEntidad/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(contexto.MenuEntidads.Single(x => x.codigoMenu == id));
        }

        //
        // POST: /MenuEntidad/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                MenuEntidad menu = contexto.MenuEntidads.Single(x => x.codigoMenu == id);

               contexto.MenuEntidads.DeleteOnSubmit(menu);
                contexto.SubmitChanges();
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
