using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models
{
    public class ModelPerfil
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public List<Diccionario> listaFuncionesPorPerfil { get; set; }
        public List<FuncionEntidad> listaTodasLasFunciones { get; set; }
    }
}