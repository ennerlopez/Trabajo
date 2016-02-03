using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class PerfilEntidadController : BaseController 
    {
        //
        // GET: /PerfilEntidad/
        private DataContextoDataContext contexto = new DataContextoDataContext();

        public ActionResult Index()
        {
            return View(contexto.PerfilEntidads);
        }

        //
        // GET: /PerfilEntidad/Details/5

        public ActionResult Details(int id)
        {
            return View(contexto.PerfilEntidads.Single(x => x.codigoPerfil == id));
        }

        //
        // GET: /PerfilEntidad/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /PerfilEntidad/Create

        [HttpPost]
        public ActionResult Create(PerfilEntidad perfil)
        {
            try
            {
                // TODO: Add insert logic here
                contexto.PerfilEntidads.InsertOnSubmit(perfil);
                contexto.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /PerfilEntidad/Edit/5
 
        public ActionResult Edit(int id)
        {


            return View(contexto.PerfilEntidads.Single(x => x.codigoPerfil == id));
        }

        //
        // POST: /PerfilEntidad/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                PerfilEntidad funcion = contexto.PerfilEntidads.Single(x => x.codigoPerfil == id);

                TryUpdateModel(funcion, collection);
                contexto.SubmitChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /PerfilEntidad/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(contexto.PerfilEntidads.Single(x => x.codigoPerfil == id));
        }

        //
        // POST: /PerfilEntidad/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                PerfilEntidad entidad = contexto.PerfilEntidads.Single(x => x.codigoPerfil == id);
                contexto.PerfilEntidads.DeleteOnSubmit(entidad);
                 contexto.SubmitChanges();
 
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View();
            }
        }

         public ActionResult AgregarFunciones(int id)
        {
            
              

                var model = new ModelPerfil();
                model.codigo = id;
                model.nombre = contexto.PerfilEntidads.Single(x => x.codigoPerfil == id).nombrePerfil;

             
             var miquery = (from q in contexto.VistaPerfilFuncions
                           where q.codigoPerfil == id
                           select new Diccionario{value = q.codigoFuncion,text = q.descripcionFuncion}).ToList();
                         
             model.listaFuncionesPorPerfil = miquery;

             model.listaTodasLasFunciones = contexto.FuncionEntidads.ToList();
          
             
            
                return View(model);
            
        }

        [HttpPost]
        public ActionResult AgregarFunciones(int id,FormCollection formCollection)
        {
            try
            {
                // TODO: Add delete logic here
                   
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

       [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult asignarFunciones(int[] fun,int id)//int[] funciones,int num)
        {
                // TODO: Add delete logic here
            string mensajeEnviado = "";
            bool isCreado = true;

           try
           {
               var funcionesActuales = from p in contexto.PerfilFuncions
                                       where p.codigoPerfil == id
                                       select p;

               contexto.PerfilFuncions.DeleteAllOnSubmit(funcionesActuales);
               contexto.SubmitChanges();

               List<PerfilFuncion> lista = new List<PerfilFuncion>();
               foreach (var codigoFuncion in fun)
               {
                   PerfilFuncion perfilFuncion = new PerfilFuncion();
                   perfilFuncion.codigoPerfil = id;
                   perfilFuncion.codigoFuncion = codigoFuncion;
                   lista.Add(perfilFuncion);
               }

               contexto.PerfilFuncions.InsertAllOnSubmit(lista);
               contexto.SubmitChanges();
           }
           catch (Exception ex)
           {
               mensajeEnviado = "Error: " + ex.Message;
               isCreado = false;
               
           }finally
           {
               if (isCreado)
               mensajeEnviado = "Transacción Realizada Correctamente";
           }



           return Json(new { success = isCreado, mensaje = mensajeEnviado });
            
        }
    }
    }

