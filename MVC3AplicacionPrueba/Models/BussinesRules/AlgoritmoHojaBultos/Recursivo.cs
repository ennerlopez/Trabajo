using System;
using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos.Entidades;

namespace MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos
{
    public class Recursivo
    {
        private const int maximoCuerposPorSeccion = 12;

        public List<TallaCantidad> agrupar(List<TallaCantidad> lista)
        {
            var arrayAgrupadoPorCantidad = getListaAgrupaPorCantidad(lista);
            int contador = arrayAgrupadoPorCantidad.Count();

            var listaYaEsta = new List<int>();
            var listaEnQueSeAgrupanYDividenLasCantidadesDeLaTalla = new List<TallaCantidad>();

            if (contieneAlgunaCantidadDivisible(arrayAgrupadoPorCantidad))
            {

                #region Procedimiento Normal

                for (int i = 0; i < contador; i++)
                {
                    #region Se Verifica que si la 'cantidadAVerificar'(arrayAgrupado) de una talla fue divisible en entre otra ambas tallas no se vuelven a evaluar

                    var cantidadAVerificar = arrayAgrupadoPorCantidad[i].cantidad;

                    if (!yaEsta((int) cantidadAVerificar, listaYaEsta))
                    {

                        for (int j = 0; j < contador; j++)
                        {
                            #region Se Verifica que si la 'cantidadAVerificar'(arrayAgrupado) y la 'cantidadConLaQueSeVerifica'(arrayAgrupado) de una talla fue divisible en entre otra ambas tallas no se vuelven a evaluar

                            var cantidaConLaQueSeVerifica = arrayAgrupadoPorCantidad[j].cantidad;
                            
                            if (!yaEsta((int) cantidadAVerificar, listaYaEsta) &&
                                (!yaEsta((int) cantidaConLaQueSeVerifica, listaYaEsta)))
                            {
                                #region Se Verifica que no se evalue la misma cantidad

                                if (i != j)
                                {


                                    if ((cantidadAVerificar%cantidaConLaQueSeVerifica) == 0)
                                    {
                                        #region Agrega las tallas para que no se vuelvan a evaluar


                                        listaYaEsta.Add((int) cantidadAVerificar);
                                        listaYaEsta.Add((int) cantidaConLaQueSeVerifica);

                                        #endregion

                                        var cantidadDeVecesQueSeVaAPartir = cantidadAVerificar/
                                                                            cantidaConLaQueSeVerifica;
                                        var cantidadEnQueSePatio = cantidadAVerificar/cantidadDeVecesQueSeVaAPartir;


                                        foreach (var tallaCantidad in lista)
                                        {

                                            #region Parte las tallas cuya cantidad sean iguales a la cantidadAVerificar

                                            if (tallaCantidad.cantidad == cantidadAVerificar)
                                            {
                                                #region agrega a la listaTallasCantidad las tallas segun maximo de cuerpos o cantidadDeVecesQueSeVaAPartir

                                                for (int l = 0; l < cantidadDeVecesQueSeVaAPartir; l++)
                                                {
                                                    TallaCantidad cantidaNuevaA = new TallaCantidad();
                                                    cantidaNuevaA.tallaCuello = tallaCantidad.tallaCuello;
                                                    cantidaNuevaA.tallaManga = tallaCantidad.tallaManga;
                                                    cantidaNuevaA.tallaLetra = tallaCantidad.tallaLetra;
                                                    cantidaNuevaA.cantidad = Convert.ToInt32(cantidadEnQueSePatio);

                                                    listaEnQueSeAgrupanYDividenLasCantidadesDeLaTalla.Add(
                                                        cantidaNuevaA);

                                                }

                                                #endregion

                                            }
                                            if (tallaCantidad.cantidad == cantidadEnQueSePatio)
                                            {
                                                TallaCantidad cantidadNuevaB = new TallaCantidad();
                                                cantidadNuevaB.tallaCuello = tallaCantidad.tallaCuello;
                                                cantidadNuevaB.tallaManga = tallaCantidad.tallaManga;
                                                cantidadNuevaB.tallaLetra = tallaCantidad.tallaLetra;
                                                cantidadNuevaB.cantidad = Convert.ToInt32(cantidadEnQueSePatio);

                                                listaEnQueSeAgrupanYDividenLasCantidadesDeLaTalla.Add(cantidadNuevaB);
                                            }


                                        }

                                        #endregion

                                    }

                                }

                                #endregion
                            }

                            #endregion
                        }
                    }

                    #endregion

                }


                foreach (var tallaCantidad in lista)
                {
                    if (yaEsta((int)tallaCantidad.cantidad, listaYaEsta))
                    {

                    }
                    else
                    {
                        var nuevaCantidad = new TallaCantidad();
                        nuevaCantidad.tallaCuello = tallaCantidad.tallaCuello;
                        nuevaCantidad.tallaManga = tallaCantidad.tallaManga;
                        nuevaCantidad.tallaLetra = tallaCantidad.tallaLetra;
                        nuevaCantidad.cantidad = (int)tallaCantidad.cantidad;

                        listaEnQueSeAgrupanYDividenLasCantidadesDeLaTalla.Add(nuevaCantidad);

                    }

                }

                #endregion

                return agrupar(listaEnQueSeAgrupanYDividenLasCantidadesDeLaTalla);
            }
            else
            {
                return lista;
            }



        }

        
        public List<TallaCantidad> agrupar(List<TallaCantidad> lista,int maximoSplitPorCapa)
        {
            var arrayAgrupadoPorCantidad = getListaAgrupaPorCantidad(lista);
            int contador = arrayAgrupadoPorCantidad.Count();

            var listaYaEsta = new List<int>();
            var listaEnQueSeAgrupanYDividenLasCantidadesDeLaTalla = new List<TallaCantidad>();

            if (contieneAlgunaCantidadDivisible(arrayAgrupadoPorCantidad,maximoSplitPorCapa))
            {

                #region Procedimiento Normal
                for (int i = 0; i < contador; i++)
                {
                    #region Se Verifica que si la 'cantidadAVerificar'(arrayAgrupado) de una talla fue divisible en entre otra ambas tallas no se vuelven a evaluar

                    var cantidadAVerificar = arrayAgrupadoPorCantidad[i].cantidad;

                    if (!yaEsta((int)cantidadAVerificar, listaYaEsta))
                    {

                        for (int j = 0; j < contador; j++)
                        {
                            #region Se Verifica que si la 'cantidadAVerificar'(arrayAgrupado) y la 'cantidadConLaQueSeVerifica'(arrayAgrupado) de una talla fue divisible en entre otra ambas tallas no se vuelven a evaluar

                            var cantidaConLaQueSeVerifica = arrayAgrupadoPorCantidad[j].cantidad;

                            if (!yaEsta((int)cantidadAVerificar, listaYaEsta) && (!yaEsta((int)cantidaConLaQueSeVerifica, listaYaEsta)))
                            {
                                #region Se Verifica que no se evalue la misma cantidad
                                if (i != j)
                                {


                                    if ((cantidadAVerificar % cantidaConLaQueSeVerifica) == 0 && (cantidadAVerificar >= maximoSplitPorCapa) && (cantidaConLaQueSeVerifica >= maximoSplitPorCapa)) //Cambio por rony mayores a eso
                                     {
                                        #region Agrega las tallas para que no se vuelvan a evaluar


                                        listaYaEsta.Add((int)cantidadAVerificar);
                                        listaYaEsta.Add((int)cantidaConLaQueSeVerifica);
                                        #endregion

                                        var cantidadDeVecesQueSeVaAPartir = cantidadAVerificar / cantidaConLaQueSeVerifica;
                                        var cantidadEnQueSePatio = cantidadAVerificar / cantidadDeVecesQueSeVaAPartir;


                                        foreach (var tallaCantidad in lista)
                                        {

                                            #region Parte las tallas cuya cantidad sean iguales a la cantidadAVerificar
                                            if (tallaCantidad.cantidad == cantidadAVerificar)
                                            {
                                                #region agrega a la listaTallasCantidad las tallas segun maximo de cuerpos o cantidadDeVecesQueSeVaAPartir
                                                for (int l = 0; l < cantidadDeVecesQueSeVaAPartir; l++)
                                                {
                                                    TallaCantidad cantidaNuevaA = new TallaCantidad();
                                                    cantidaNuevaA.tallaCuello = tallaCantidad.tallaCuello;
                                                    cantidaNuevaA.tallaManga = tallaCantidad.tallaManga;
                                                    cantidaNuevaA.tallaLetra = tallaCantidad.tallaLetra;
                                                    cantidaNuevaA.cantidad = Convert.ToInt32(cantidadEnQueSePatio);

                                                    listaEnQueSeAgrupanYDividenLasCantidadesDeLaTalla.Add(cantidaNuevaA);

                                                }
                                                #endregion

                                            }
                                            if (tallaCantidad.cantidad == cantidadEnQueSePatio)
                                            {
                                                TallaCantidad cantidadNuevaB = new TallaCantidad();
                                                cantidadNuevaB.tallaCuello = tallaCantidad.tallaCuello;
                                                cantidadNuevaB.tallaManga = tallaCantidad.tallaManga;
                                                cantidadNuevaB.tallaLetra = tallaCantidad.tallaLetra;
                                                cantidadNuevaB.cantidad = Convert.ToInt32(cantidadEnQueSePatio);

                                                listaEnQueSeAgrupanYDividenLasCantidadesDeLaTalla.Add(cantidadNuevaB);
                                            }


                                        }

                                            #endregion

                                    }

                                }
                                #endregion
                            }
                            #endregion
                        }
                    }
                    #endregion

                }


                foreach (var tallaCantidad in lista)
                {
                    if (yaEsta((int)tallaCantidad.cantidad, listaYaEsta))
                    {

                    }
                    else
                    {
                        var nuevaCantidad = new TallaCantidad();
                        nuevaCantidad.tallaCuello = tallaCantidad.tallaCuello;
                        nuevaCantidad.tallaManga = tallaCantidad.tallaManga;
                        nuevaCantidad.tallaLetra = tallaCantidad.tallaLetra;
                        nuevaCantidad.cantidad = (int)tallaCantidad.cantidad;

                        listaEnQueSeAgrupanYDividenLasCantidadesDeLaTalla.Add(nuevaCantidad);

                    }

                }

                #endregion

                return agrupar(listaEnQueSeAgrupanYDividenLasCantidadesDeLaTalla);
            }
            else
            {
                return lista;
            }



        }
        


        private bool yaEsta(int cantidadAVerificar, List<int> listaEnQueSeVerifica)
        {
            bool vf = false;


            foreach (var i in listaEnQueSeVerifica)
            {
                if (i == cantidadAVerificar)
                {
                    vf = true;
                    break;

                }
            }
            return vf;
        }


        private bool contieneAlgunaCantidadDivisible(CantidaAgrupada[] arrayAgrupado)
        {
            bool vf = false;
            var array = arrayAgrupado;
            int contador = array.Count();
            bool bandera = false;

            for (int i = 0; i < contador; i++)
            {
                int? cantidadA = array[i].cantidad;

                for (int j = 0; j < contador; j++)
                {

                    if (i != j)
                    {
                        int? cantidadB = array[j].cantidad;

                        if ((cantidadA % cantidadB) == 0 && (cantidadA>=38) && (cantidadB>=38))
                        {
                            vf = true;
                            bandera = true;
                            break;
                        }
                    }

                }

                if (bandera)
                {
                    break;
                }

            }

            return vf;
        }

        private bool contieneAlgunaCantidadDivisible(CantidaAgrupada[] arrayAgrupado,int maximoSplitPorCapa)
        {
            bool vf = false;
            var array = arrayAgrupado;
            int contador = array.Count();
            bool bandera = false;

            for (int i = 0; i < contador; i++)
            {
                int? cantidadA = array[i].cantidad;

                for (int j = 0; j < contador; j++)
                {

                    if (i != j)
                    {
                        int? cantidadB = array[j].cantidad;

                        if ((cantidadA % cantidadB) == 0 && (cantidadA >= maximoSplitPorCapa) && (cantidadB >= maximoSplitPorCapa))
                        {
                            vf = true;
                            bandera = true;
                            break;
                        }
                    }

                }

                if (bandera)
                {
                    break;
                }

            }

            return vf;
        }




        private CantidaAgrupada[] getListaAgrupaPorCantidad(List<TallaCantidad> listaTallaCantida)
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

    }
}