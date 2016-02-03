using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize()]
    public class ModuloEntidadController : BaseController 
    {
        //
        // GET: /ModuloEntidad/
        private DataContextoDataContext contexto = new DataContextoDataContext();

        public ActionResult Index()
        {
            
            return View(contexto.ModuloEntidads);
        }

        //
        // GET: /ModuloEntidad/Details/5

        public ActionResult Details(int id)
        {
            return View(contexto.ModuloEntidads.Single(x=> x.codigoModulo ==id));
        }

        //
        // GET: /ModuloEntidad/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ModuloEntidad/Create

        [HttpPost]
        public ActionResult Create(ModuloEntidad moduloEntidad)
        {
            try
            {
                contexto.ModuloEntidads.InsertOnSubmit(moduloEntidad);
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
        // GET: /ModuloEntidad/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(contexto.ModuloEntidads.Single(x=> x.codigoModulo ==id));
        }

        //
        // POST: /ModuloEntidad/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection  collection)
        {
            try
            {
                // TODO: Add update logic here
                ModuloEntidad modulo = contexto.ModuloEntidads.Single(x => x.codigoModulo == id);

                TryUpdateModel(modulo, collection);
                contexto.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ModuloEntidad/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(contexto.ModuloEntidads.Single(x => x.codigoModulo == id));
        }

        //
        // POST: /ModuloEntidad/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                ModuloEntidad modulo = contexto.ModuloEntidads.Single(x => x.codigoModulo == id);
                contexto.ModuloEntidads.DeleteOnSubmit(modulo);
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
