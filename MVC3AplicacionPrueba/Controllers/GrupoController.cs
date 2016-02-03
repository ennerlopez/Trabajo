using System.Collections.Generic;
using System.Web.Mvc;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Autentificacion;
using MVC3AplicacionPrueba.Models.Modelado.ModelGrupo;
using MVC3AplicacionPrueba.Models.Repositorio;

namespace MVC3AplicacionPrueba.Controllers
{
    [Authorize]
    public class GrupoController : BaseController 
    {
        private RepositorioGrupo _repositorioGrupo;
        //
        // GET: /Grupo/

        public GrupoController()
        {
            var   contexto = new DataContextoDataContext();
           _repositorioGrupo = new RepositorioGrupo(contexto);
        }


        public ActionResult Index()
        {
            List<GrupoModel> lista= new List<GrupoModel>();
            lista.Add(new GrupoModel(){codigo = 1,descripcion = "hola",nombre = "hola"});
            lista.Add(new GrupoModel() { codigo = 2, descripcion = "prueba", nombre = "hola" });
            

            return View(_repositorioGrupo.getAll());
        }

        //
        // GET: /Grupo/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Grupo/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Grupo/Create

        [HttpPost]
        public ActionResult Create(GrupoModel grupo)
        {
            try
            {
                _repositorioGrupo.saveGrupo(grupo);
            
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Grupo/Edit/5
 
        public ActionResult Edit(int id)
        {


            return View(_repositorioGrupo.getGrupo(id));
        }

        //
        // POST: /Grupo/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, GrupoModel grupo)
        {
            try
            {
                grupo.codigo = id;
                _repositorioGrupo.edit(grupo);
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Grupo/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_repositorioGrupo.getGrupo(id));
        }

        //
        // POST: /Grupo/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                _repositorioGrupo.delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
