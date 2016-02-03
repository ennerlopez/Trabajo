using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos.Entidades;

namespace MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos
{
    public class DefinirSecciones
    {

        public List<TallaSeccion> definir(List<TallaCantidad> lista, int maximoRepeticionesDeCuerpoPermitido)
        {

            List<TallaSeccion> listaSeccion = new List<TallaSeccion>();
            var entidadDesdeListaRedifinida = Util.getListaAgrupaPorCantidad(lista)[0];
            var listaOrdenada = Util.ordenarDescendente(lista);


            int contadorSeccion = 1;
            int cantidadMaximaDelaTallaCantidadDos = (int)entidadDesdeListaRedifinida.cantidad;
            int contadorCuerpos = 0;

            TallaCantidad paraBanderaTallaCantidad = listaOrdenada.First();

            string banderaTallaCantidad = "" + paraBanderaTallaCantidad.tallaCuello + paraBanderaTallaCantidad.tallaManga;

            foreach (var tallaCantidad in listaOrdenada)
            {

                if (cantidadMaximaDelaTallaCantidadDos != tallaCantidad.cantidad)
                {
                    contadorCuerpos = 0;
                    contadorSeccion = contadorSeccion + 1;
                    cantidadMaximaDelaTallaCantidadDos = tallaCantidad.cantidad;

                }

                string valorACompararConBandera = "" + tallaCantidad.tallaCuello + tallaCantidad.tallaManga;

                if (banderaTallaCantidad != valorACompararConBandera)
                {
                    banderaTallaCantidad = valorACompararConBandera;
                    contadorCuerpos = 0;

                }


                if (contadorCuerpos < maximoRepeticionesDeCuerpoPermitido)
                {

                    TallaSeccion seccion = new TallaSeccion();
                    seccion.tallCuello = tallaCantidad.tallaCuello;
                    seccion.tallaManga = tallaCantidad.tallaManga;
                    seccion.tallaLetra = tallaCantidad.tallaLetra;
                    seccion.cantidad = tallaCantidad.cantidad;
                    seccion.seccion = contadorSeccion;
                    listaSeccion.Add(seccion);
                    contadorCuerpos = contadorCuerpos + 1;
                }
                else
                {
                    contadorSeccion = contadorSeccion + 1;

                    TallaSeccion seccion = new TallaSeccion();
                    seccion.tallCuello = tallaCantidad.tallaCuello;
                    seccion.tallaManga = tallaCantidad.tallaManga;
                    seccion.tallaLetra = tallaCantidad.tallaLetra;
                    seccion.cantidad = tallaCantidad.cantidad;
                    seccion.seccion = contadorSeccion;
                    listaSeccion.Add(seccion);

                    contadorCuerpos = 1;
                }


            }

            return listaSeccion;
        }

    }
}