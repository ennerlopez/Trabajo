using System;
using System.Web.Mvc;
using CrystalDecisions.Shared;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;
using MVC3AplicacionPrueba.Models.Repositorio;
using MVC3AplicacionPrueba.Reportes;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize()]
    public class CuponController : BaseController 
    {
        private readonly DataContextoDataContext _contexto;
        private readonly RepositorioCupon _repositorio;
        public CuponController()
        {
            _contexto = new DataContextoDataContext();
            _repositorio = new RepositorioCupon(_contexto);
        }


        public ActionResult creacionCupones()
        {
            return View();
        }

       
        public ActionResult report(string codigo)
        {
           if( _repositorio.existCorte(codigo)){
            var cuponModels = _repositorio.getReportCupones(codigo);
             var cupones = new CuponesReporte();
            cupones.SetDataSource(cuponModels);

            var stream = cupones.ExportToStream(ExportFormatType.PortableDocFormat);
           
            return  File(stream, "application/pdf",string.Format("{0}-{1}{2}","Cupones",codigo,".pdf"));
           }
            return RedirectToAction("creacionCupones");
        }

      
     
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult generarCupones(string codigoCorte)
        {
            var mensajeEnviado = "";
            var isCreado = true;
            // List<Diccionario> listaTallasCliente = null;

            try
            {
              mensajeEnviado=  _repositorio.saveCuponesCorte(codigoCorte);
            }
            catch (Exception ex)
            {

                mensajeEnviado = "Error: " + ex.Message;
                isCreado = false;
            }
             


            return Json(new { success = isCreado, mensaje = mensajeEnviado });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult eliminarCupones(string codigoCorte)
        {
            var mensajeEnviado = "";
            var isCreado = true;
            // List<Diccionario> listaTallasCliente = null;

            try
            {
                mensajeEnviado = _repositorio.eliminarCupones(codigoCorte);
            }
            catch (Exception ex)
            {

                mensajeEnviado = "Error: " + ex.Message;
                isCreado = false;
            }



            return Json(new { success = isCreado, mensaje = mensajeEnviado });
        }
    }
}
