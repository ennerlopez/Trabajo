using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelEstilo
{
    public class ModelPreguntaCrearEstilo
    {
        public string estilo { get; set; }
        public string comentario { get; set; }
        public List<Diccionario> estilos { get; set; }
        public int estiloSeleccionado { get; set; } 
    }
}