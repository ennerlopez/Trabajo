using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC3AplicacionPrueba.Models
{
    public static class SimpleSessionPersister
    {
        private const string FuncionesUsuario = "funcionesUsuario";

        public static List<Login_vw> funciones
        {
            get
            {
                if (HttpContext.Current == null) return null;
                if (HttpContext.Current.Session[FuncionesUsuario] != null)
                    return HttpContext.Current.Session[FuncionesUsuario] as List<Login_vw>;
                return null;
            }
            set { HttpContext.Current.Session[FuncionesUsuario] = value; }
        }
    }
}