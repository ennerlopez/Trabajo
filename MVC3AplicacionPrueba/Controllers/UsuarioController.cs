using System;
using System.Linq;
using System.Web.Mvc;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class UsuarioController : BaseController 
    {
        //
        // GET: /Usuario/
        private DataContextoDataContext contexto = new DataContextoDataContext();

        public ActionResult Index()
        {
            return View(contexto.Usuarios);
        }

        //
        // GET: /Usuario/Details/5

        public ActionResult Details(int id)
        {
            return View(contexto.Usuarios.Single(x=>x.codigoUsuario==id));
        }

        //
        // GET: /Usuario/Create

        public ActionResult Create()
        {
            ModelUsuario usuario = new ModelUsuario();
            usuario.listaCambioPassword = contexto.siNos.ToList();
            
            usuario.listaPerfiles = (from i in contexto.PerfilEntidads
                                        select i).ToList();
          
            return View(usuario);
        } 

        //
        // POST: /Usuario/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                Usuario usuario = new Usuario();
                usuario.nombreUsuario = collection["nickUsuario"];
                usuario.passwordUsuario = collection["password"];
                usuario.cambioPasswordUsuario = Convert.ToInt32(collection["listaCambioPassword"]);
                usuario.codigoPerfil = Convert.ToInt32(collection["listaPerfiles"]);

                contexto.Usuarios.InsertOnSubmit(usuario);
                contexto.SubmitChanges();
          
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Usuario/Edit/5
 
        public ActionResult Edit(int id)
        {
         

            return View(contexto.Usuarios.Single(x=>x.codigoUsuario==id));
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                
                // TODO: Add update logic here
                Usuario usuario = contexto.Usuarios.Single(x => x.codigoUsuario == id);

                TryUpdateModel(usuario, collection);
                contexto.SubmitChanges();
                
             
                contexto.SubmitChanges();



                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Usuario/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(contexto.Usuarios.Single(x => x.codigoUsuario == id));
        }

        //
        // POST: /Usuario/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Usuario usuario = contexto.Usuarios.Single(x => x.codigoUsuario == id);
                contexto.Usuarios.DeleteOnSubmit(usuario);
                contexto.SubmitChanges();
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /*----------------------------------------------------------------------------*/

        public ActionResult Editar(int id)
        {
            Usuario usuario = contexto.Usuarios.Single(x => x.codigoUsuario == id);
            ModelUsuario modelUsuario = new ModelUsuario();

            modelUsuario.codigoUsuario = usuario.codigoUsuario;
            modelUsuario.codigoPerfil = usuario.codigoPerfil;
            modelUsuario.codigoCambioPassword = usuario.cambioPasswordUsuario;
            modelUsuario.nickUsuario = usuario.nombreUsuario;
            modelUsuario.password = usuario.passwordUsuario;
            modelUsuario.listaCambioPassword = contexto.siNos.ToList();
            modelUsuario.listaPerfiles = contexto.PerfilEntidads.ToList();


            return View(modelUsuario);
        }

        //
        // POST: /Usuario/Edit/5

        [HttpPost]
        public ActionResult Editar(int id, FormCollection collection)
        {
            try
            {

                // TODO: Add update logic here
                Usuario usuario = contexto.Usuarios.Single(x => x.codigoUsuario == id);
                
                usuario.nombreUsuario = collection["nickUsuario"];
                usuario.passwordUsuario = collection["password"];
                usuario.cambioPasswordUsuario = Convert.ToInt32(collection["codigoCambioPassword"]);
                usuario.codigoPerfil = Convert.ToInt32(collection["codigoPerfil"]);

            
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
