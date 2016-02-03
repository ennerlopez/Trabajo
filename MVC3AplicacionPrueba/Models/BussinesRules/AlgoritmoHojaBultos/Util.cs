using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos.Entidades;

namespace MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos
{
    public static class Util
    {
        public static List<TallaCantidad> ordenarDescendente(List<TallaCantidad> lista)
        {
            return lista.OrderByDescending(cantidad => cantidad.cantidad).ToList();
        }

        public static List<TallaCantidad> ordenarAscendente(List<TallaCantidad> lista)
        {
            return lista.OrderBy(cantidad => cantidad.cantidad).ToList();
        }

        public static CantidaAgrupada[] getListaAgrupaPorCantidad(List<TallaCantidad> listaTallaCantida)
        {
            var listaAgrupada = (from a in listaTallaCantida
                                 group a by new
                                                {
                                                    cantidad = a.cantidad
                                                }
                                 into g
                                 orderby g.Key.cantidad descending
                                 select new CantidaAgrupada()
                                            {
                                                cantidad = g.Key.cantidad
                                            }).ToArray();

            return listaAgrupada;
        }

        public static int Redondeo(int cantidad, int maximoCapasPorTalla)
        {
            int repeticiones = cantidad / maximoCapasPorTalla;

            if (cantidad % maximoCapasPorTalla != 0)
            {
                repeticiones = repeticiones + 1;
            }

            return repeticiones;


        }
    }
}