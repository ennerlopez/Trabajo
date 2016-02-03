using System;
using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelOperaciones
{
    public class ModelOperaciones
    {
        public string nombre { get; set; }
      //  public int codigoDepartamento { get; set; }
        public int codigoGrado { get; set; }
        public string nombreGrado { get; set; }
        // public List<Diccionario> listaDepartamentos { get; set; }
        public List<Diccionario> listaGrado { get; set; }
        /*public int minutos { get; set; }
        public int segundos { get; set; }
       */ 
        //public decimal? duracionSegundos { get; set; }
        //public decimal? tiempoManejoBultos { get; set; }
        public TimeSpan? duracionSegundos { get; set; }
        public TimeSpan? tiempoManejoBultos { get; set; }
        public decimal? valorPieza { get; set; }
      


    }
}