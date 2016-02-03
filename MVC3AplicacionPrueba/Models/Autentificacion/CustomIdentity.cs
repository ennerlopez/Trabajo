using System;
using System.Linq;
using System.Security.Principal;

namespace MVC3AplicacionPrueba.Models.Autentificacion
{
    public class CustomIdentity : IIdentity
    {
        private readonly string _controlador;
        private readonly string _accion;

        public CustomIdentity(string name,string controlador,string accion)
        {
            if (controlador == null) throw new ArgumentNullException("controlador");
            if (accion == null) throw new ArgumentNullException("accion");
            _controlador = controlador;
            _accion = accion;
            this.Name = name;
        }

        #region IIdentity Members

        public string Name { get; private set; }

        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get
            {
                var funcione = SimpleSessionPersister.funciones;
                return funcione.Any(vw => vw.accionContralador.ToUpper() == _controlador.ToUpper()); //&& vw.accionFuncion.ToUpper() == _accion.ToUpper()
            }
        }

        #endregion
    }
}