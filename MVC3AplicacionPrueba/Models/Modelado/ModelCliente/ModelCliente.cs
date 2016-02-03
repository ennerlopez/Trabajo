using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelCliente
{
    public class ModelCliente
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public List<Diccionario> tallasCuello { get; set; }
        public List<Diccionario> tallasManga { get; set; }
        public List<Diccionario> tallasLetra { get; set; }
        public List<ModelTallasCliente> listaTallas { get; set; }
    }

    
}