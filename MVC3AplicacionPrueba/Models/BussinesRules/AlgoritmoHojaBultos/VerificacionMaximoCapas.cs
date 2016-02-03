using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos.Entidades;

namespace MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos
{
    public class VerificacionMaximoCapas
    {
        public List<TallaCantidad> redefinir(List<TallaCantidad> lista, int cantidadPorTalla)
        {
           
            var redefinicionDeLaListaEnQueSeAgrupanYDividenCantidadesDeLaTallas = new List<TallaCantidad>();

            var listaOrdenada = Util.ordenarDescendente(lista);



            foreach (var tallaCantidad in listaOrdenada)
            {

                if (tallaCantidad.cantidad > cantidadPorTalla)
                {

                    int repeticionesDeCuerpoExistente = Util.Redondeo(tallaCantidad.cantidad, cantidadPorTalla);

                    int cantidadQueTraeTalla = tallaCantidad.cantidad;



                    for (int i = 0; i < repeticionesDeCuerpoExistente; i++)
                    {
                        int cantidadAGuardar = 0;

                        if (cantidadQueTraeTalla > cantidadPorTalla)
                        {
                            cantidadQueTraeTalla = cantidadQueTraeTalla - cantidadPorTalla;
                            cantidadAGuardar = cantidadPorTalla;
                        }
                        else
                        {
                            cantidadAGuardar = cantidadQueTraeTalla;
                        }
                        TallaCantidad nuevaTallaCantiadd = new TallaCantidad();
                        nuevaTallaCantiadd.tallaCuello = tallaCantidad.tallaCuello;
                        nuevaTallaCantiadd.tallaManga = tallaCantidad.tallaManga;
                        nuevaTallaCantiadd.tallaLetra = tallaCantidad.tallaLetra;
                        nuevaTallaCantiadd.cantidad = cantidadAGuardar;


                        redefinicionDeLaListaEnQueSeAgrupanYDividenCantidadesDeLaTallas.Add(nuevaTallaCantiadd);

                    }


                }
                else
                {

                    redefinicionDeLaListaEnQueSeAgrupanYDividenCantidadesDeLaTallas.Add(tallaCantidad);


                }





            }

            return redefinicionDeLaListaEnQueSeAgrupanYDividenCantidadesDeLaTallas;

        }
    }
}