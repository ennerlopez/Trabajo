using System;
using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelFunciones;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class RepositorioFunciones
    {
          private readonly DataContextoDataContext _contexto; 
        
        
        public RepositorioFunciones(DataContextoDataContext contexto)
        {
            this._contexto = contexto;
        }

        public ModelFunciones getFuncion(int codigoFuncion)
        {
            var modelFuncion = new ModelFunciones();

            var funcionEntidad = _contexto.FuncionEntidads.Single(x => x.codigoFuncion == codigoFuncion);
            modelFuncion.descripcion = funcionEntidad.descripcionFuncion;
            modelFuncion.controlador = funcionEntidad.accionContralador;
            modelFuncion.accion = funcionEntidad.accionFuncion;
            modelFuncion.nombreMenu = funcionEntidad.MenuEntidad.descripcionMenu;
            modelFuncion.codigoMenu = Convert.ToInt32(funcionEntidad.codigoMenu);
            modelFuncion.listaMenu = listaMenus();
            
            return modelFuncion;
        }

        public List<Diccionario>  listaMenus()
        {
            var lista = new List<Diccionario>();

            var menus = _contexto.MenuEntidads;
           
            foreach (var menuEntidad in menus)
            {
                var diccionario = new Diccionario();
                diccionario.value = menuEntidad.codigoMenu;
                diccionario.text = menuEntidad.descripcionMenu;
                lista.Add(diccionario);
            }
            return lista;
        }

        public void insertar(ModelFunciones modelFunciones)
        {
            FuncionEntidad funcionEntidad = new FuncionEntidad();
            funcionEntidad.descripcionFuncion = modelFunciones.descripcion;
            funcionEntidad.accionContralador = modelFunciones.controlador;
            funcionEntidad.accionFuncion = modelFunciones.accion;
            funcionEntidad.codigoMenu = modelFunciones.codigoMenu;
        
            _contexto.FuncionEntidads.InsertOnSubmit(funcionEntidad);
            _contexto.SubmitChanges();
        }

        public void actualizar(int id,ModelFunciones modelFunciones)
        {
            FuncionEntidad funcionEntidad = _contexto.FuncionEntidads.Single(x => x.codigoFuncion == id);
            funcionEntidad.descripcionFuncion = modelFunciones.descripcion;
            funcionEntidad.accionContralador = modelFunciones.controlador;
            funcionEntidad.accionFuncion = modelFunciones.accion;
            funcionEntidad.codigoMenu = modelFunciones.codigoMenu;
            _contexto.SubmitChanges();

        }
    }
}