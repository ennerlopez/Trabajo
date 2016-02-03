using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;
using MVC3AplicacionPrueba.Models.Modelado.ModelEstilo;
using MVC3AplicacionPrueba.Models.Repositorio;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class EstilosController : BaseController 
    {
        //
        // GET: /Estilos/

         private DataContextoDataContext contexto;// = new DataContextoDataContext();
        private RepositorioEstilo repositorio;
 
      public EstilosController()
        {
            contexto = new DataContextoDataContext();
            repositorio = new RepositorioEstilo(contexto);
        }

        public ActionResult Index()
        {
            return View(repositorio.listaEstilos());
        }

        //
        // GET: /Estilos/Details/5

        public ActionResult Details(int id)
        {
            return View(repositorio.obtenerEstilo(id));
        }

        //
        // GET: /Estilos/Create

        public ActionResult Create()
        {
           
            return View(repositorio.getModelPreguntaCrearEstilo());
        } 

        //
        // POST: /Estilos/Create

        [HttpPost]
        public ActionResult Create(FormCollection estilo)
        {
            try
            {
                var es = estilo["estilos"];

                var model = new ModelPreguntaCrearEstilo
                    {
                        estiloSeleccionado =es==string.Empty?0:Convert.ToInt32(es),
                        estilo = estilo["estilo"],
                        comentario = estilo["comentario"]
                    };

                // TODO: Add insert logic here
                repositorio.agregarEstilo(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Estilos/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(repositorio.obtenerEstilo(id));
        }

        //
        // POST: /Estilos/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, Estilo estilo)
        {
            try
            {
                // TODO: Add update logic here
                    repositorio.actualizarEstilo(id,estilo);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Estilos/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(repositorio.obtenerEstilo(id));
        }

        //
        // POST: /Estilos/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                repositorio.eliminarEstilo(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult AgregarOperaciones(int id)
        {
            Estilo estilo = repositorio.obtenerEstilo(id);
            ModelEstilo modelEstilo = new ModelEstilo();
            modelEstilo.codigo = estilo.codigoEstilo;
            modelEstilo.nombre = estilo.nombreEstilo;
            modelEstilo.comentario = estilo.comentario;
            modelEstilo.listaTodasLasOperaciones = repositorio.listarTodasLasOperaciones();
            modelEstilo.listaOperacionesPorEstilo = repositorio.listarOperacionesDeEstilo(id);
            
            return View(modelEstilo);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult asignarOperaciones(int[] op, int id)
        {
            string mensajeEnviado = "";
            bool isCreado = true;

            try
            {

                
                if (op != null)
                {
                    repositorio.guardarOperacionesAEstilo(op, id);

                }
                else
                {
                    repositorio.guardarOperacionesAEstilo(new int[0], id);
                }


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
    }
}
