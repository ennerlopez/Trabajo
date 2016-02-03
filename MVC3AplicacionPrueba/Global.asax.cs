using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using MVC3AplicacionPrueba.Mapeo;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBultos;

namespace MVC3AplicacionPrueba
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

          //  routes.MapRoute(
          //    "Enner", // Route name
          //    "Herramientas/prueba/{controller}/{action}/{id}", // URL with parameters
          //    new { controller = "Prueba", action = "Index", id = UrlParameter.Optional } // Parameter defaults
          //);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            Mapper.AddProfile(new MapeoModelDetalleHojaBultoToHojaBulto());
            Mapper.AddProfile(new MapeoNumeroBultosToModelDetalleHojaBulto());
            Mapper.AddProfile(new MapeoCortesAPlanificarToDiccionario());
            Mapper.AddProfile(new MapeoCortesDisponiblesAPlanificarToHojaBultos());
            Mapper.AddProfile(new MapeoLineasProduccionToDiccionario());

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}