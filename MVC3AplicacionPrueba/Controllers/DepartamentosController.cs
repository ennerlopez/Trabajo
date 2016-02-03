using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;
using MVC3AplicacionPrueba.Models.Modelado.ModelDepartamentos;
using MVC3AplicacionPrueba.Models.Repositorio;

namespace MVC3AplicacionPrueba.Controllers
{
      [Authorize()]
    public class DepartamentosController : BaseController 
    {
        //
        // GET: /Departamentos/
        private DataContextoDataContext contexto;// = new DataContextoDataContext();
        private RepositorioDepartamentos repositorio;
 
      public DepartamentosController()
        {
            contexto = new DataContextoDataContext();
            repositorio = new RepositorioDepartamentos(contexto);
        }




        public ActionResult Index()
        {
           

            return View(repositorio.getListaDepartamentos());
        }

        //
        // GET: /Departamentos/Details/5

        public ActionResult Details(int id)
        {
            return View(repositorio.getDepartamento(id));
        }

        //
        // GET: /Departamentos/Create

        public ActionResult Create()
        {
            var departamento = new ModelDepartamentos
                                   {
                                       listaLineasProduccion = repositorio.getListaLineasProduccion(),
                                       listaGrupos = repositorio.getListaGrupos()
                                   };

            return View(departamento);
        } 

        //
        // POST: /Departamentos/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                //// TODO: Add insert logic here
                var departamento = new Departamento();
                departamento.nombreDepartamento = collection["nombre"];
                departamento.comentarioDepartamento = collection["comentario"];
                departamento.codigoLinea = Convert.ToInt32(collection["listaLineasProduccion"]);
                departamento.codigoGrupo = Convert.ToInt32(collection["listaGrupos"]);
                contexto.Departamentos.InsertOnSubmit(departamento);
                contexto.SubmitChanges();


                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                return View();
            }
        }
        
        //
        // GET: /Departamentos/Edit/5
 
        public ActionResult Edit(int id)
        {
            var ModelDepartamento = new ModelDepartamentos();
            var departamento =repositorio.getDepartamento(id);

            ModelDepartamento.codigo = departamento.codigoDepartamento;
            ModelDepartamento.nombre = departamento.nombreDepartamento;
            ModelDepartamento.comentario = departamento.comentarioDepartamento;
            ModelDepartamento.codigoLinea = departamento.codigoLinea;
            ModelDepartamento.codigoGrupo = (int) departamento.codigoGrupo;
            ModelDepartamento.listaLineasProduccion = repositorio.getListaLineasProduccion();
            ModelDepartamento.listaGrupos = repositorio.getListaGrupos();
            


            return View(ModelDepartamento);
        }

        //
        // POST: /Departamentos/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                Departamento departamento = repositorio.getDepartamento(id);
                departamento.nombreDepartamento = collection["nombre"];
                departamento.comentarioDepartamento = collection["comentario"];
                departamento.codigoLinea = Convert.ToInt32(collection["codigoLinea"]);
                departamento.codigoGrupo = Convert.ToInt32(collection["codigoGrupo"]);
                contexto.SubmitChanges();
                
                
               
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Departamentos/Delete/5
 
        public ActionResult Delete(int id)
        {

            return View(repositorio.getDepartamento(id));
        }

        //
        // POST: /Departamentos/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Departamento departamento = repositorio.getDepartamento(id);
                contexto.Departamentos.DeleteOnSubmit(departamento);
                contexto.SubmitChanges();
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult agregarOperaciones(int id)
        {
            Departamento departamento = repositorio.getDepartamento(id); 
            ModelDepartamentos modelDepartamentos = new ModelDepartamentos();
            modelDepartamentos.codigo = departamento.codigoDepartamento;
            modelDepartamentos.nombre = departamento.nombreDepartamento;
            modelDepartamentos.codigoLinea = departamento.codigoLinea;
            modelDepartamentos.nombreLinea = departamento.LineasProduccion.nombreLinea;
            modelDepartamentos.listaTodasOperaciones = repositorio.getListaTodasOperaciones();
            modelDepartamentos.listaOperacionesPorDepartamento = repositorio.getListaOperacionesPorDepartamento(id);
           
            return View(modelDepartamentos);
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
                    repositorio.guardarOperacionesADepartamento(op, id);
                }
                else
                {
                    repositorio.guardarOperacionesADepartamento(new int[0], id);
                }
           

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
