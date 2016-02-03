using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelGrupo;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class RepositorioGrupo
    {
        private readonly DataContextoDataContext _contexto;


        public RepositorioGrupo(DataContextoDataContext contexto)
        {
            _contexto = contexto;
        }

        public void saveGrupo(GrupoModel grupo)
        {   
            var grupoBd = new Grupo();
            grupoBd.nombreGrupo = grupo.nombre;
            grupoBd.descripcionGrupo = grupo.descripcion;

            _contexto.Grupos.InsertOnSubmit(grupoBd);
            _contexto.SubmitChanges();

        }

        public IEnumerable<GrupoModel> getAll()
        {
            return
                _contexto.Grupos.Select(
                    x =>
                    new GrupoModel {codigo = x.codigoGrupo, nombre = x.nombreGrupo, descripcion = x.descripcionGrupo});
        }


        public void edit(GrupoModel grupo)
        {
            var entidadAEditar = _contexto.Grupos.FirstOrDefault(x => x.codigoGrupo == grupo.codigo);
            entidadAEditar.nombreGrupo = grupo.nombre;
            entidadAEditar.descripcionGrupo = grupo.descripcion;
          
            _contexto.SubmitChanges();
           
        }

        public GrupoModel getGrupo(int codigo)
        {
            var grupo = _contexto.Grupos.FirstOrDefault(x => x.codigoGrupo == codigo);
          
            var grupoModel = new GrupoModel {nombre = grupo.nombreGrupo, descripcion = grupo.descripcionGrupo};

            return grupoModel;
        }

        public void delete(int codigo)
        {
            var entidadAEliminar = _contexto.Grupos.FirstOrDefault(x => x.codigoGrupo == codigo);
            _contexto.Grupos.DeleteOnSubmit(entidadAEliminar);
            _contexto.SubmitChanges();
        }

     
    }
}