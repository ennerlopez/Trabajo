using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelDepartamentos
{
    public class ModelDepartamentos
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string comentario { get; set; }
        public int codigoLinea { get; set; }
        public string nombreLinea { get; set; }
        public int codigoGrupo { get; set; }
        public string nombreGrupo { get; set; }
        public List<Diccionario> listaLineasProduccion { get; set; }
        public List<Diccionario> listaGrupos { get; set; }
        public List<Diccionario> listaTodasOperaciones { get; set; }
        public List<Diccionario> listaOperacionesPorDepartamento { get; set; }
    }
}