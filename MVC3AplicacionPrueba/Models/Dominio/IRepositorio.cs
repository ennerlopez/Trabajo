using System.Collections.Generic;

namespace MVC3AplicacionPrueba.Models.Dominio
{
    public interface IRepositorio<T>
    {
        IList<T> ObtnernLista();
        T ObtenerEntidad(int id);
        void Guardar(T entidad);
        void Guardar(IList<T> rango);
        void Actualizar(T entidad);
        void Eliminar(T entidad);
        void Eliminar(IList<T> rango);

    }
}