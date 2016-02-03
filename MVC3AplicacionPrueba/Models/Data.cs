using System.Collections.Generic;
using System.Linq;

namespace MVC3AplicacionPrueba.Models
{
    public class Data
    {
        
        public static List<VistaMenus> obtenerLista (int codigoModulo,int codigoUsuario)
        {
            var d = new DataContextoDataContext();
             
            var lista = (from e in d.VistaMenus
                        where e.codigoModulo == codigoModulo && e.codigoUsuario == codigoUsuario
                        select e).ToList();
            

            return lista;;
        }

        public static List<VistaFuncione> obtenerListaFunciones(int codigoMenu,int codigoUsuario)
        {
            var d = new DataContextoDataContext();

            var lista = (from e in d.VistaFunciones
                         where e.codigoMenu == codigoMenu && e.codigoUsuario == codigoUsuario
                         select e).ToList();


            return lista; ;
        }


        public static List<VistaModulo> obtenerListaModulos(int codidoUsuario)
        {
            var d = new DataContextoDataContext();

            var query = from a in d.VistaModulos
                        where a.codigoUsuario == codidoUsuario
                        select a;


            return query.ToList(); ;
        }

        public static  List<ModuloEntidad> listaDeModuloEntidad()
        {
            var context = new DataContextoDataContext();

            return context.ModuloEntidads.ToList();
        }

        public static List<MenuEntidad> listaDeMenuEntidad()
        {
            var context = new DataContextoDataContext();

            return context.MenuEntidads.ToList();
        }
        public static List<PerfilEntidad> listaDePerfilEntidad()
        {
            var context = new DataContextoDataContext();

            return context.PerfilEntidads.ToList();
        }

        public static List<siNo> listaDeSiNo()
        {
            var context = new DataContextoDataContext();

            return context.siNos.ToList();
        }
    }
}