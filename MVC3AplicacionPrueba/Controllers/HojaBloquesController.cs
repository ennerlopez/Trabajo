using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CrystalDecisions.Shared;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Dto;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Reglas;
using MVC3AplicacionPrueba.Models.Repositorio;
using MVC3AplicacionPrueba.Models.Util;
using MVC3AplicacionPrueba.Reportes;

namespace MVC3AplicacionPrueba.Controllers
{
    public class HojaBloquesController : Controller
    {
        private DataContextoDataContext _contexto;
        private HojaBloquesRepositorio _repositorio;

        public HojaBloquesController()
        {
            _contexto = new DataContextoDataContext();

            var listaReglas = new List<IStrategy>()
                {
                    new CantidadParaCompletarBloqueIgualACantidadBulto(),
                    new CantidadACompletarMayorQueCantidadDeBulto(),
                    new CantidadParaCompletarBloqueMenorACantidadDeBulto()
                };


            _repositorio = new HojaBloquesRepositorio(_contexto, listaReglas);
        }


        public ActionResult GenerarReporteDeBloques()
        {
            var model= new ModelPreguntaReporteHojaBloque(){lineas =  _repositorio.getLineas()};
          
            return View(model);
        }

        public ActionResult BuscarParaEditarHojaBloques()
        {
            var model = new ModelPreguntaReporteHojaBloque {lineas = _repositorio.getLineas()};
            return View(model);
 
        }

     
        public ActionResult EditarHojaBloques(ModelPreguntaReporteHojaBloque model)
        {
            bool existe = _repositorio.existePlanificacion(model);
            if (existe)
            {
                return View(_repositorio.getModelHojaBloques(model));
            
            }

            return View("BuscarParaEditarHojaBloques", new ModelPreguntaReporteHojaBloque(){lineas = _repositorio.getLineas()});
        }

        //
        // GET: /HojaBloques/

        public ActionResult Index()
        {
            List<Diccionario> dd = new List<Diccionario>();
            dd.Add(new Diccionario() {text = "a", value = 1});
            return View(dd);
        }

        //
        // GET: /HojaBloques/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /HojaBloques/Create

        public ActionResult Create()
        {
            return View(_repositorio.getModelHojaBloques());
        }


        //
        // GET: /HojaBloques/Edit/5

        public ActionResult Edit(string id,int variable,int otra)
        {
            return View();
        }

        //
        // POST: /HojaBloques/Edit/5

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




        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult agregarAPlanificacion(ModelHojaBloques model, IEnumerable<MyHojaBloques> bloques)
        {
            string mensajeEnviado = "";
            bool isCreado = true;
           

            IEnumerable<MyHojaBloques> nuevosBloques = null;
            var planificacion = new Planificacion();
            planificacion.bloques = bloques == null ? new List<MyHojaBloques>() : bloques.ToList();
            planificacion.datos = model;
            try
            {
                nuevosBloques = _repositorio.getPlanificacion(planificacion);
            }
            catch (Exception ex)
            {

                mensajeEnviado = "Error: " + ex.Message;
                isCreado = false;
            }
            finally
            {
                if (isCreado)
                    mensajeEnviado = planificacion.mensaje;
            }


            return Json(new {success = isCreado, mensaje = mensajeEnviado, listaBloques = nuevosBloques});
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult guardarPlanificacion(IEnumerable<MyHojaBloques> bloques)
        {
            string mensajeEnviado = "";
            bool isCreado = true;



            try
            {
                _repositorio.guardarPlanificacion(bloques);
            }
            catch (Exception ex)
            {

                mensajeEnviado = "Error: " + ex.Message;
                isCreado = false;
            }
            finally
            {
                if (isCreado)
                    mensajeEnviado = "Guardado Exitosamente";
            }


            return Json(new {success = isCreado, mensaje = mensajeEnviado});
        }




        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult editarPlanificacion(IEnumerable<MyHojaBloques> bloques, ModelHojaBloques model)
        {
            var mensajeEnviado = "";
            var isCreado = true;


            try
            {
                _repositorio.editarPlanificacion(bloques,model);
            }
            catch (Exception ex)
            {

                mensajeEnviado = "Error: " + ex.Message;
                isCreado = false;
            }
            finally
            {
                if (isCreado)
                    mensajeEnviado = "Guardado Exitosamente";
            }


            return Json(new { success = isCreado, mensaje = mensajeEnviado });
        }



        public ActionResult hojaBloquesReport(FormCollection model)
        {
            var semana = Convert.ToInt32(model["semana"]);
            var year = Convert.ToInt32(model["year"]);
            var color = model["colores"];
            var linea =Convert.ToInt32(model["lineas"]);

            if (_repositorio.existHojaBloque(semana, year, color,linea))
            {
                var hojaBloques = _repositorio.getBloques(semana,year,color,linea);
                var reporte = new HojaBloquesReporte();
                reporte.SetDataSource(hojaBloques);

                var stream = reporte.ExportToStream(ExportFormatType.PortableDocFormat);

                return File(stream, "application/pdf",
                            string.Format("{0}-{1}-{2}-{3}{4}", "HojaBloque", semana,year,color, ".pdf"));
            }
            return RedirectToAction("GenerarReporteDeBloques");
        }

    }
}


