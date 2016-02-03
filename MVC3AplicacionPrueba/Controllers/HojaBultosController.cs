using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos.Entidades;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBultos;
using MVC3AplicacionPrueba.Models.Repositorio;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class HojaBultosController : BaseController 
    {
        //
        // GET: /HojaBultos/
        private readonly DataContextoDataContext _contexto;
        private readonly RepositorioHojaDeBultos _repositorio;

        public HojaBultosController ()
        {
            _contexto = new DataContextoDataContext();
            _repositorio = new RepositorioHojaDeBultos(_contexto);
            
        }



        public ActionResult Index()
        {
            DataContextoDataContext contexto = new DataContextoDataContext();
           
            return View(contexto.HojaBultos);
        }

        //
        // GET: /HojaBultos/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /HojaBultos/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /HojaBultos/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
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
        // GET: /HojaBultos/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }



        public ActionResult EditHojaBulto(string id)
        {
            if (!_repositorio.tieneHojaBulto(id)){            
                return RedirectToAction("Index","Ordenes");
            }
            var hojaDeBultosParaCreacion = _repositorio.getHojaDeBultosParaEditar(id);

            
            return View(hojaDeBultosParaCreacion);
        }


        //
        // POST: /HojaBultos/Edit/5

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
        // GET: /HojaBultos/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /HojaBultos/Delete/5

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



        public ActionResult BusquedaHojaBulto()
        {
            return View();
        }



        public ActionResult GenerarHojaBulto(string id)
        {
            var codigo = id;
            if (_repositorio.tieneHojaBulto(codigo))
            {
                return RedirectToAction("Index", "Ordenes");
            }
            var orden = _repositorio.getHojaDeBultosParaCreacion(codigo);

            return View(orden);

        }



        // GET: /HojaBultos/Edit/5

        public ActionResult CrearHojaBultos(string id)
        {
            var codigo = id;
            if (_repositorio.tieneHojaBulto(codigo))
            {
                return RedirectToAction("Index","Ordenes");
            }
            var orden = _repositorio.getHojaDeBultosParaCreacion(codigo);

            return View(orden);

        }





        public ActionResult ConfirmarHojaBultos(ModelSolicitudDePedido solicitud)
        {   
            var codigo = solicitud.codigoPedido;
            if(!_repositorio.existeCorte(codigo))
            {
                return RedirectToAction("BusquedaHojaBulto");
            }
            var orden = _repositorio.getHojaDeBultosSegunOrden(codigo);
            
            return View(orden);

        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult confirmarOden(ModelResumenHojaBultos resumenOrden, ModelDetalleHojaBulto[] detalleOrden)
        {
            string mensajeEnviado = "";
            bool isCreado = true;
            // List<Diccionario> listaTallasCliente = null;

            try
            {
                _repositorio.confirmarOrden(resumenOrden, detalleOrden);
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
        public JsonResult guardarHojaBulto(ModelResumenHojaBultos resumenOrden, ModelDetalleHojaBulto[] detalleOrden)
        {
            string mensajeEnviado = "";
            bool isCreado = true;
           

            try
            {
                _repositorio.guardarHojaBultosQueSeCreo(resumenOrden,detalleOrden);
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


            return Json(new { success = isCreado, mensaje=mensajeEnviado, redirectToUrl = Url.Action("Index", "Ordenes") });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult guardarHojaBultoGenerada(ModelResumenHojaBultos resumenOrden, ModelDetalleHojaBulto[] detalleOrden)
        {
            string mensajeEnviado = "";
            bool isCreado = true;


            try
            {
                _repositorio.guardarHojaBultosQueSeGenero(resumenOrden, detalleOrden);
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


            return Json(new { success = isCreado, mensaje = mensajeEnviado, redirectToUrl = Url.Action("Index", "Ordenes") });
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult obtenerGeneracionDeHojaBulto(int serieComienza,string idOrden,int splitCuerpo,int capasPorTalla,int repeticionesDeCuerpo)
        {
            string mensajeEnviado = "";
            bool isCreado = true;
            IList<ModelDetalleHojaBulto> listaHojaBultos = null;

            try
            {
                listaHojaBultos = _repositorio.getListaGenerada(serieComienza,idOrden,capasPorTalla,repeticionesDeCuerpo,splitCuerpo);
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


            return Json(new { success = isCreado, mensaje = mensajeEnviado, listaHojaBultos });
        }


    

      

       
    }
}
