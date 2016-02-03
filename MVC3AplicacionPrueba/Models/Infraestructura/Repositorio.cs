using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Dominio;
using System.Linq;

namespace MVC3AplicacionPrueba.Models.Infraestructura
{
    public class Repositorio<T>:IRepositorio<T> where T:class
    {
        private readonly DataContextoDataContext _contexto;

        public Repositorio(DataContextoDataContext contexto)
        {
            _contexto = contexto;
        }


        public IList<T> ObtnernLista()
        {
            return _contexto.GetTable<T>().ToList();
        }

        public T ObtenerEntidad(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Guardar(T entidad)
        {
            throw new System.NotImplementedException();
        }

        public void Guardar(IList<T> rango)
        {
            throw new System.NotImplementedException();
        }

        public void Actualizar(T entidad)
        {
            throw new System.NotImplementedException();
        }

        public void Eliminar(T entidad)
        {
            throw new System.NotImplementedException();
        }

        public void Eliminar(IList<T> rango)
        {
            throw new System.NotImplementedException();
        }
    }
}