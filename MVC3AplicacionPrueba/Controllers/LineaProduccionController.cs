using System.Linq;
using System.Web.Mvc;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class LineaProduccionController : BaseController 
    {
        //
        // GET: /LineaProduccion/
        private  DataContextoDataContext contexto = new DataContextoDataContext();
        public ActionResult Index()
        {
            return View(contexto.LineasProduccions.ToList());
        }

        //
        // GET: /LineaProduccion/Details/5

        public ActionResult Details(int id)
        {
            return View(contexto.LineasProduccions.Single(x=>x.codigoLinea ==id ));
        }

        //
        // GET: /LineaProduccion/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /LineaProduccion/Create

        [HttpPost]
        public ActionResult Create(LineasProduccion lineas)
        {
            try
            {       
                // TODO: Add insert logic here
                contexto.LineasProduccions.InsertOnSubmit(lineas);
                contexto.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /LineaProduccion/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(contexto.LineasProduccions.Single(x => x.codigoLinea == id));
        }

        //
        // POST: /LineaProduccion/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                LineasProduccion lineas = contexto.LineasProduccions.Single(x => x.codigoLinea == id);    

                UpdateModel(lineas,collection);
                contexto.SubmitChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /LineaProduccion/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(contexto.LineasProduccions.Single(x => x.codigoLinea == id));
        }

        //
        // POST: /LineaProduccion/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                LineasProduccion lineasProduccion = contexto.LineasProduccions.Single(x => x.codigoLinea == id);
                // TODO: Add delete logic here
                contexto.LineasProduccions.DeleteOnSubmit(lineasProduccion);
                contexto.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
