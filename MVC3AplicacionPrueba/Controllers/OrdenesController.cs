using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.Shared;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;
using MVC3AplicacionPrueba.Models.Modelado.ModelOrdenes;
using MVC3AplicacionPrueba.Models.Repositorio;
using MVC3AplicacionPrueba.Models.Util;
using MVC3AplicacionPrueba.Reportes;
using MVC3AplicacionPrueba.Reportes.Entidades;

namespace MVC3AplicacionPrueba.Controllers
{   [Authorize]
    public class OrdenesController : BaseController 
    {
        //
        // GET: /Ordenes/


        private DataContextoDataContext contexto;// = new DataContextoDataContext();
        private RepositorioOrden repositorio;
 
      public OrdenesController()
        {
            contexto = new DataContextoDataContext();
            repositorio = new RepositorioOrden(contexto);
        }



        public ActionResult Index()
        {
            return View(repositorio.listaDeOrdenesNoCortadas());
        }

        //
        // GET: /Ordenes/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Ordenes/Create

        public ActionResult CrearOrden()
        {
            ModelOrden modelOrden = new ModelOrden();
            modelOrden.listaCliente = repositorio.listaClientes();
            modelOrden.listaEstilos = repositorio.listaEstilos();

            return View(modelOrden);
        } 

        //
        // POST: /Ordenes/Create

        [HttpPost]
        public ActionResult CrearOrden(FormCollection collection)
        {
            try
            {
                
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Ordenes/Edit/5
 
        public ActionResult Edit(string id)
        {
            if(repositorio.OrdenYaFueProcesada(id))
            {
                return RedirectToAction("Index");
            }
           
            var orden = repositorio.ObtenerOrden(id);
            return View(orden);
        }


        //
        // POST: /Ordenes/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /Ordenes/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Ordenes/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
        public JsonResult obtenerTallasDeCliente(int id)
        {
            string mensajeEnviado = "";
            bool isCreado = true;
            List<Diccionario> listaTallasCliente=null;

            try
            {
                listaTallasCliente = repositorio.listaTallasCliente(id);
            }
            catch (Exception ex)
            {

                mensajeEnviado = "Error: " + ex.Message;
                isCreado = false;
            }
            finally
            {
                if (isCreado)
                    mensajeEnviado = "Transacción Realizada Correctamente";
            }

           
            return Json(new { success = isCreado, mensaje = mensajeEnviado,listaTallasCliente });
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult GuardarOrden(ModelResumenOrden resumenOrden, ModelDetalleOrden[] detalleOrden)
        {
            string mensajeEnviado = "";
            bool isCreado = true;
           // List<Diccionario> listaTallasCliente = null;

            try
            {
                repositorio.guardarOrden(resumenOrden,detalleOrden);
            }
            catch (Exception ex)
            {

                mensajeEnviado = "Error: " + ex.Message;
                isCreado = false;
            }
            finally
            {
                if (isCreado)
                    mensajeEnviado = "Transacción Realizada Correctamente";
            }


            return Json(new { success = isCreado, mensaje = mensajeEnviado });
        }

         [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult EditarOrden(ModelResumenOrden resumenOrden, ModelDetalleOrden[] detalleOrden)
        {
            string mensajeEnviado = "";
            bool isCreado = true;
           // List<Diccionario> listaTallasCliente = null;

            try
            {
                repositorio.EditarOrden(resumenOrden,detalleOrden);
            }
            catch (Exception ex)
            {

                mensajeEnviado = "Error: " + ex.Message;
                isCreado = false;
            }
            finally
            {
                if (isCreado)
                    mensajeEnviado = "Transacción Realizada Correctamente";
            }


            return Json(new { success = isCreado, mensaje = mensajeEnviado });
        }


         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult EliminarOrden(string codigoOrden)
         {
             string codigo = codigoOrden.Trim();
             
             string mensajeEnviado = "";
             bool isCreado = true;
             // List<Diccionario> listaTallasCliente = null;

             try
             {
                 repositorio.EliminarOrder(codigo);
             }
             catch (Exception ex)
             {

                 mensajeEnviado = "Error: " + ex.Message;
                 isCreado = false;
             }
             finally
             {
                 if (isCreado)
                     mensajeEnviado = "Transacción Realizada Correctamente";
             }


             return Json(new { success = isCreado, mensaje = mensajeEnviado });
         }

         public ActionResult imprimirReportHojaBultosTalla()
         {
             return View();
         }


        [HttpPost]
         public ActionResult reportHojaBultosTallas(ModelCapas model)
         {
             if (repositorio.existCorte(model.codigoCorte))
             {
                 var data = repositorio.getReporteImpresionHojaBultoTallas(model.codigoCorte);
                 var cupones = new HojaBultoTallasReporte();
                 cupones.SetDataSource(data);

                 var stream = cupones.ExportToStream(ExportFormatType.PortableDocFormat);

                 return File(stream, "application/pdf", string.Format("{0}-{1}{2}", "HojoBultosTallas", model.codigoCorte, ".pdf"));
             }
             return RedirectToAction("imprimirReportHojaBultosTalla");
         }
        


    }




}



