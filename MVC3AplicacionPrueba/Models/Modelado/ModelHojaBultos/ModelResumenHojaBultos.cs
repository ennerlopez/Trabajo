using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBultos
{
    public class ModelResumenHojaBultos
    {
        public string codigoOrden { get; set; }
        public string cliente { get; set;}
        public string fechaCortado { get; set; }

        public List<ModelDetalleHojaBulto> listaDetalle { get; set; }
        public List<Diccionario> listaTallas { get; set; }
    }
}