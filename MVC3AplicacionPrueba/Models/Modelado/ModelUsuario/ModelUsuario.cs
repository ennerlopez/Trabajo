using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC3AplicacionPrueba.Models
{
    public class ModelUsuario
    {
        public int codigoUsuario { get; set; }
        public string nickUsuario { get; set; }
        public string password { get; set; }
        public int codigoCambioPassword { get; set; }
        public int codigoPerfil { get; set; }
        public List<siNo> listaCambioPassword { get; set; }
        public List<PerfilEntidad> listaPerfiles { get; set; }
    }
}