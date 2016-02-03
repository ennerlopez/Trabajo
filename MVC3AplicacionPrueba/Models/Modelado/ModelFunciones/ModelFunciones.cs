using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelFunciones
{
    public class ModelFunciones
    {
        public string descripcion { get; set; }
        public string accion { get; set; }
        public string controlador { get; set; }
        public int codigoMenu { get; set; }
        public string nombreMenu { get; set; }
        public List<Diccionario> listaMenu { get; set; }
    }
}