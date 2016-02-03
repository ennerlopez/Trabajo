using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques
{
    public class ModelPreguntaReporteHojaBloque
    {
        public ModelPreguntaReporteHojaBloque()
        {
            colores=new List<Diccionario>();
            lineas = new List<Diccionario>();
            colores.Add(new Diccionario(){value = 1,text = "Verde"});
            colores.Add(new Diccionario() { value = 2, text = "Rojo" });
            colores.Add(new Diccionario() { value = 3, text = "Azul" });

        }
        public int semana { get; set; }
        public int year { get; set; }
        public string color { get; set; }
        public int linea { get; set; }
        public List<Diccionario> colores { get; set; }
        public List<Diccionario> lineas { get; set; } 
        
    }
}