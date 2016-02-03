using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class GradoController : BaseController 
    {
        //
        // GET: /Grado/
        private DataContextoDataContext contexto = new DataContextoDataContext(); 


        public ActionResult Index()
        {
            return View(contexto.Grados.ToList());
        }

        //
        // GET: /Grado/Details/5

        public ActionResult Details(int id)
        {
            return View(contexto.Grados.Single(x => x.codigoGrado == id));
        }

        //
        // GET: /Grado/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Grado/Create

        [HttpPost]
        public ActionResult Create(Grado grado)
        {
            try
            {
                contexto.Grados.InsertOnSubmit(grado);
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
        // GET: /Grado/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(contexto.Grados.Single(x=>x.codigoGrado == id));
        }

        //
        // POST: /Grado/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {       
                // TODO: Add update logic here
                Grado grado = contexto.Grados.Single(x => x.codigoGrado == id);
                UpdateModel(grado,collection);
                contexto.SubmitChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Grado/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(contexto.Grados.Single(x => x.codigoGrado == id));
        }

        //
        // POST: /Grado/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Grado grado = contexto.Grados.Single(x => x.codigoGrado == id);
                contexto.Grados.DeleteOnSubmit(grado);
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
