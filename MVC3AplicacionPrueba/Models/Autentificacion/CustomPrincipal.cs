using System.Security.Principal;

namespace MVC3AplicacionPrueba.Models.Autentificacion
{
    public class CustomPrincipal : IPrincipal
    {
        public CustomPrincipal(CustomIdentity identity)
        {
            this.Identity = identity;
        }

        #region IPrincipal Members

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return true; // everyone's a winner
        }

        #endregion
    }
}