using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos.Entidades;

namespace MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos
{
    public class PasoCero
    {
        private const float Porcentaje = 30;


        public bool existeUnNumeroQueDivisbleSeaMenorQueElPorcentaje(List<TallaCantidad> tallaCantidad)
        {
            bool vf = false;


            var listaAgrupaPorCantidad = Util.getListaAgrupaPorCantidad(tallaCantidad).ToList();
            int contadoListaAgrupada = listaAgrupaPorCantidad.Count;

            var cantidadA = (float)listaAgrupaPorCantidad.FirstOrDefault().cantidad;
            var cantidadB = (float)listaAgrupaPorCantidad.LastOrDefault().cantidad;
            var listaYaEsta = new List<int>();
            //if((cantidadA % cantidadB) == 0)
            //{
            //    var porcentaje = ((cantidadB / cantidadA) * 100);



            //    if (porcentaje >= Porcentaje)
            //    {
            //        vf = true;
            //    }
            //}



            for (int i = 0; i < contadoListaAgrupada; i++)
            {
                var A = (float)listaAgrupaPorCantidad[i].cantidad;
                if (!yaEsta((int)A, listaYaEsta))
                {
                    for (int j = 0; j < contadoListaAgrupada; j++)
                    {
                        var B = (float)listaAgrupaPorCantidad[j].cantidad;

                        if (!yaEsta((int)A, listaYaEsta) && (!yaEsta((int)B, listaYaEsta)))
                        {

                            if (i != j)
                            {
                                if ((A % B) == 0)
                                {
                                    listaYaEsta.Add((int)A);
                                    listaYaEsta.Add((int)B);
                                    var porcentaje = 0.0;

                                    if (A > B)
                                    {
                                        porcentaje = ((B / A) * 100);

                                    }
                                    else
                                    {
                                        porcentaje = ((A / B) * 100);
                                    }





                                    if (porcentaje < Porcentaje)
                                    {
                                        vf = true;
                                        break;
                                    }


                                }


                            }
                        }
                    }
                    if (vf)
                    {
                        break;
                    }
                }

            }





            return vf;
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
    }
}