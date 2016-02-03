using System;
using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos.Entidades;

namespace MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos
{
    public class DefinirHojaBultos
    {
        private int MaximoSplitPorCapa;


        public List<NumeroBultos> getHojaBultos(List<TallaSeccion> lista, string codigoCorte,int maximoSplitCapa =36)
        {
            MaximoSplitPorCapa = maximoSplitCapa;

            var listaHojaBultos = new List<NumeroBultos>();
            string listaCapas = "A,B,C,D,E,F";

            var listaOrdenada = from lis in lista//listaSeccion
                                orderby lis.seccion, lis.tallCuello, lis.tallaManga ascending
                                select lis;


            int maximaCantidadCapa = lista.AsQueryable().Max(seccion => seccion.cantidad);
            int lineaSeccion = lista.AsEnumerable().Min(seccion => seccion.seccion);//listaPorSeccion.First().cantidad;
            int contadorBloque = 1;

            foreach (var entidad in listaOrdenada)
            {
                decimal ultimaCapa = 0;

                if (maximaCantidadCapa > MaximoSplitPorCapa)
                {
                    ultimaCapa = decimal.Ceiling(maximaCantidadCapa / (decimal)MaximoSplitPorCapa);
                }

                if (maximaCantidadCapa == maximoSplitCapa)
                {
                    ultimaCapa = 1;
                }


                if (lineaSeccion != entidad.seccion)
                {
                    lineaSeccion = entidad.seccion;
                    contadorBloque = 1;
                }



                if (entidad.cantidad < MaximoSplitPorCapa)
                {

                    var i = decimal.Ceiling((decimal)entidad.cantidad / MaximoSplitPorCapa);
                    // var i = Math.Round((double)entidad.cantidad/MaximoSplitPorCapa,MidpointRounding.AwayFromZero);//
                    var capa = ultimaCapa - i;
                    sacarCapas(listaHojaBultos, listaCapas, contadorBloque, codigoCorte, entidad,(int) capa, entidad.cantidad);
                }
                else
                {
                    var i = decimal.Ceiling((decimal)entidad.cantidad / MaximoSplitPorCapa);
                  // var i = Math.Round((double)entidad.cantidad/MaximoSplitPorCapa,MidpointRounding.AwayFromZero);//
                    var capa = ultimaCapa - i;

                    sacarCapas(listaHojaBultos, listaCapas, contadorBloque, codigoCorte, entidad,(int) capa, entidad.cantidad);
                }



                if (lineaSeccion == entidad.seccion)
                {
                    contadorBloque = contadorBloque + 1;
                }






            }

            return listaHojaBultos;
        }



        private void sacarCapas(List<NumeroBultos> listaNumerosDeBultos, string listaCapas, int numeroBulto, string codigoCorte, TallaSeccion cantidad, int capa, int cantidadActualCapa)
        {
            var ArrayCapas = listaCapas.Split(',');
            string letraDeCapa = "";

          
            letraDeCapa = ArrayCapas[capa];

           

            if (cantidadActualCapa <= MaximoSplitPorCapa)
            {

                var hojaBulto = new NumeroBultos();
                hojaBulto.numeroBulto = numeroBulto;
                hojaBulto.codigoCorte = codigoCorte;
                hojaBulto.numeroSeccion = cantidad.seccion;
                hojaBulto.capa = letraDeCapa;
                hojaBulto.tallCuello = cantidad.tallCuello;
                hojaBulto.tallaManga = cantidad.tallaManga;
                hojaBulto.tallaCompleta = "(" + cantidad.tallCuello + "-" + cantidad.tallaManga + ")" + cantidad.tallaLetra;
                hojaBulto.cantidadPorCapa = cantidadActualCapa;
                listaNumerosDeBultos.Add(hojaBulto);
            }
            else
            {
                int cantidaSeguienteCapa = cantidadActualCapa - MaximoSplitPorCapa;

                var hojaBulto = new NumeroBultos();
                hojaBulto.numeroBulto = numeroBulto;
                hojaBulto.codigoCorte = codigoCorte;
                hojaBulto.numeroSeccion = cantidad.seccion;
                hojaBulto.capa = letraDeCapa;
                hojaBulto.tallCuello = cantidad.tallCuello;
                hojaBulto.tallaManga = cantidad.tallaManga;
                hojaBulto.tallaCompleta = "(" + cantidad.tallCuello + "-" + cantidad.tallaManga + ")" + cantidad.tallaLetra;
                hojaBulto.cantidadPorCapa = MaximoSplitPorCapa;

                listaNumerosDeBultos.Add(hojaBulto);
                int siguienteCapa = capa + 1;

                sacarCapas(listaNumerosDeBultos, listaCapas, numeroBulto, codigoCorte, cantidad, siguienteCapa, cantidaSeguienteCapa);

            }
        }
    }
}