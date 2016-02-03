using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelEstilo
{
    public class ModelEstilo
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string comentario { get; set; }

        public List<Diccionario> listaTodasLasOperaciones { get; set; }
        public List<Diccionario> listaOperacionesPorEstilo { get; set; }
    }
}