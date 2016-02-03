using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using  MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;
using MVC3AplicacionPrueba.Models.Modelado.ModelCliente;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Dto;
using MVC3AplicacionPrueba.Models.Repositorio;


namespace MVC3AplicacionPrueba.Controllers
{
      [Authorize()]
    public class ClienteController : BaseController 
    {
        //
        // GET: /Cliente/



        private DataContextoDataContext contexto;// = new DataContextoDataContext();
        private RepositorioCliente repositorio;
 
      public ClienteController()
        {
            contexto = new DataContextoDataContext();
            repositorio = new RepositorioCliente(contexto);
        }




        public ActionResult Index()
        {
            



            return View(repositorio.getListaClientes());
        }

        //
        // GET: /Cliente/Details/5

        public ActionResult Details(int id)
        {
            return View(repositorio.getCliente(id));
        }

        //
        // GET: /Cliente/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Cliente/Create

        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                // TODO: Add insert logic here
                contexto.Clientes.InsertOnSubmit(cliente);
                contexto.SubmitChanges();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Cliente/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View(repositorio.getCliente(id));
        }

        //
        // POST: /Cliente/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Cliente cliente = repositorio.getCliente(id);
                TryUpdateModel(cliente, collection);
                contexto.SubmitChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Cliente/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(repositorio.getCliente(id));
        }

        //
        // POST: /Cliente/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Cliente cliente = repositorio.getCliente(id);
                     
                contexto.Clientes.DeleteOnSubmit(cliente);
                contexto.SubmitChanges();

                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult ingresarTallas(int id)
        {
            Cliente cliente = repositorio.getCliente(id);
 
            ModelCliente modelCliente = new ModelCliente();
            modelCliente.codigo = cliente.codigoCliente;
            modelCliente.nombre = cliente.nombreCliente;
            modelCliente.tallasCuello = repositorio.getListaTallaCuello();
            modelCliente.tallasManga = repositorio.getListaTallaManga();
            modelCliente.tallasLetra = repositorio.getListaTallaLetra();
            modelCliente.listaTallas = repositorio.getListaTallasCliente(id);

            return View(modelCliente);
        }

        /*==============================JSON INGRESAR, EDITAR Y ELIMINAR*/

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult asignarTallas(int idCliente,string cuello, string manga,string letra)
        {
            string mensajeEnviado ="";
            bool isCreado = true;
            try
            {
               repositorio.guardarTalla(idCliente,cuello,manga,letra);

            }
            catch (Exception ex)
            {

                mensajeEnviado = "Error: " + ex.Message;
                  isCreado = false;

            }finally
            {
                if(isCreado)
                mensajeEnviado = "Transacción Realizada Correctamente";

            }

            return Json(new { success = isCreado, mensaje = mensajeEnviado });

        }

          [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult editarTallas(int idCliente, string cuello, string manga, string letra)
        {

          

          string mensajeEnviado = "";
          bool isCreado = true;
          try
          {
              repositorio.editarCliente(idCliente, int.Parse(cuello), int.Parse(manga), letra);
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
          public JsonResult eliminarTallas(int idCliente, string cuello, string manga)
          {



              string mensajeEnviado = "";
              bool isCreado = true;
              try
              {
                  repositorio.eliminarTallaDeCliente(idCliente,int.Parse(cuello),int.Parse(manga));
                 
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
