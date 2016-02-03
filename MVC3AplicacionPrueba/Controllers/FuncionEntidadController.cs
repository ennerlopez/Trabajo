using System;
using System.Linq;
using System.Web.Mvc;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;
using MVC3AplicacionPrueba.Models.Modelado.ModelFunciones;
using MVC3AplicacionPrueba.Models.Repositorio;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class FuncionEntidadController : BaseController 
    {
        //
        // GET: /FuncionEntidad/
         private DataContextoDataContext contexto;
        private RepositorioFunciones repositorio;
        
        public FuncionEntidadController()
        {
            contexto = new DataContextoDataContext();
            repositorio = new RepositorioFunciones(contexto);
        }

        public ActionResult Index()
        {
            return View(contexto.FuncionEntidads);
        }

        //
        // GET: /FuncionEntidad/Details/5

        public ActionResult Details(int id)
        {
            return View(contexto.FuncionEntidads.Single(x => x.codigoFuncion == id));
        }

        //
        // GET: /FuncionEntidad/Create

        public ActionResult Create()
        {   var funciones= new ModelFunciones();
            funciones.listaMenu = repositorio.listaMenus();
            return View(funciones);
        } 

        //
        // POST: /FuncionEntidad/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var modelFunciones = new ModelFunciones();
           
            try
            {
                modelFunciones.descripcion = collection["descripcion"];
                modelFunciones.controlador = collection["controlador"];
                modelFunciones.accion = collection["accion"];
                string codigoMenu = collection["listaMenu"];
                modelFunciones.codigoMenu = int.Parse(codigoMenu);

                repositorio.insertar(modelFunciones);
             
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View();
            }
        }

        //
        // GET: /FuncionEntidad/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(repositorio.getFuncion(id));
        }

        //
        // POST: /FuncionEntidad/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            //try
            //{
            //    // TODO: Add update logic here
            //    FuncionEntidad funcion = contexto.FuncionEntidads.Single(x => x.codigoFuncion == id);

            //    TryUpdateModel(funcion, collection);
            //    contexto.SubmitChanges();
            var modelFunciones = new ModelFunciones();

            try
            {
                modelFunciones.descripcion = collection["descripcion"];
                modelFunciones.controlador = collection["controlador"];
                modelFunciones.accion = collection["accion"];
                string codigoMenu = collection["codigoMenu"];
                modelFunciones.codigoMenu = int.Parse(codigoMenu);

                repositorio.actualizar(id,modelFunciones);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /FuncionEntidad/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(contexto.FuncionEntidads.Single(x => x.codigoFuncion == id));
        }

        //
        // POST: /FuncionEntidad/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                FuncionEntidad funcion = contexto.FuncionEntidads.Single(x => x.codigoFuncion == id);
                contexto.FuncionEntidads.DeleteOnSubmit(funcion);
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
