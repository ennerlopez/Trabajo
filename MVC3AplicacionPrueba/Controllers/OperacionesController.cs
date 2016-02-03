using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using  MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;
using MVC3AplicacionPrueba.Models.Modelado.ModelOperaciones;
using MVC3AplicacionPrueba.Models.Repositorio;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class OperacionesController : BaseController 
    {
        private readonly DataContextoDataContext _contexto;
        private readonly RepositorioOperaciones _repositorio;
        
        public OperacionesController()
        {
            _contexto = new DataContextoDataContext();
            _repositorio = new RepositorioOperaciones(_contexto);
        }

        //
        // GET: /Operaciones/
        
 

        public ActionResult Index()
        {
            return View(_repositorio.getListaOperaciones());
        }

        //
        // GET: /Operaciones/Details/5                          

        public ActionResult Details(int id)
        {
           
         
            return View(_repositorio.getOperacion(id));
        }

        //
        // GET: /Operaciones/Create

        public ActionResult Create()
        {
          
            ModelOperaciones operaciones = new ModelOperaciones();
           // operaciones.listaDepartamentos = contexto.Departamentos.ToList();
            operaciones.listaGrado = _repositorio.getListaGrado();
         
         

            return View(operaciones);
        } 

        //
        // POST: /Operaciones/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
             
                _repositorio.guardarOperacion(
                                            collection["nombre"],
                                            Convert.ToInt32(collection["listaGrado"]),
                                              collection["duracionSegundos"],
                                              collection["tiempoManejoBultos"],
                                            Convert.ToDecimal(collection["valorPieza"]) 
                                            );

                
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch(Exception)
            {


                //ModelOperaciones operaciones = new ModelOperaciones();
                //// operaciones.listaDepartamentos = contexto.Departamentos.ToList();
                //operaciones.listaGrado = _repositorio.getListaGrado();

                return Create();
            }
        }
        
        //
        // GET: /Operaciones/Edit/5
 
        public ActionResult Edit(int id)
        {
         
            return View(_repositorio.getModelOperacion(id));
        }

        //
        // POST: /Operaciones/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                _repositorio.UpdateOperacion(id,
                                            collection["nombre"],
                                            Convert.ToInt32(collection["codigoGrado"]),
                                           collection["duracionSegundos"],
                                            collection["tiempoManejoBultos"],
                                            Convert.ToDecimal(collection["valorPieza"])
                                            );
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Operaciones/Delete/5
 
        public ActionResult Delete(int id)
        {

            return View(_repositorio.getOperacion(id));
        }

        //
        // POST: /Operaciones/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                _repositorio.eliminarOperacion(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(_repositorio.getOperacion(id));
            }
        }
    }
}
