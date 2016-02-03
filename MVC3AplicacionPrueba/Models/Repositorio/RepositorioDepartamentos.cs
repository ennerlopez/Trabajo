using System;
using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class RepositorioDepartamentos
    {
        private DataContextoDataContext contexto; 
        
        
        public RepositorioDepartamentos(DataContextoDataContext contexto)
        {
            this.contexto = contexto;
        }

        public List<Departamento> getListaDepartamentos()
        {
            return contexto.Departamentos.ToList();
        }



        public Departamento getDepartamento(int id)
        {
            return contexto.Departamentos.Single(x => x.codigoDepartamento == id);

        }

        public List<Diccionario> getListaLineasProduccion()
        {
            var lista = contexto.LineasProduccions.ToList();

            List<Diccionario> listaDiccionario  = new List<Diccionario>();
            foreach (var lineas in lista)
            {
                Diccionario diccionario = new Diccionario();
                diccionario.value = lineas.codigoLinea;
                diccionario.text = lineas.nombreLinea;
                listaDiccionario.Add(diccionario);
            }

            return listaDiccionario;
        }

        public List<Diccionario> getListaGrupos()
        {
            var lista = contexto.Grupos.ToList();

            List<Diccionario> listaDiccionario = new List<Diccionario>();
            foreach (var lineas in lista)
            {
                Diccionario diccionario = new Diccionario();
                diccionario.value = lineas.codigoGrupo;
                diccionario.text = lineas.nombreGrupo;
                listaDiccionario.Add(diccionario);
            }

            return listaDiccionario;
        }

        public List<Diccionario> getListaTodasOperaciones()
        {
            var lista = contexto.Operaciones.ToList();

            List<Diccionario> listaDiccionario = new List<Diccionario>();
            foreach (var operacion in lista)
            {
                Diccionario diccionario = new Diccionario();
                diccionario.value = operacion.codigoOperacion;
                diccionario.text = operacion.nombreOperacion;
                listaDiccionario.Add(diccionario);
            }

            return listaDiccionario;
        }

        public List<Diccionario> getListaOperacionesPorDepartamento(int id)
        {
            var lista = contexto.vistaRelacionDepartamentoOperaciones.ToList().Where(x => x.codigoDepartamento == id);

            List<Diccionario> listaDiccionario = new List<Diccionario>();
            foreach (var operacion in lista)
            {
                Diccionario diccionario = new Diccionario();
                diccionario.value = operacion.codigoOperacion;
                diccionario.text = operacion.nombreOperacion;
                listaDiccionario.Add(diccionario);
            }

            return listaDiccionario;
        }

        public void borrarOperacionesActualesDepartamento(int id)
        {
            var operacionesActuales = contexto.DepartamentoOperaciones.Where(x => x.codigoDepartamento == id).ToList();;
            contexto.DepartamentoOperaciones.DeleteAllOnSubmit(operacionesActuales);
            contexto.SubmitChanges();
        }

        public void agregarNuevasOperacionesADepartamento(int[] op, int id)
        {
            var lista = new List<DepartamentoOperacione>();
            foreach (var codigoOperacion in op)
            {
                var departamentoOperacione = new DepartamentoOperacione();
                departamentoOperacione.codigoDepartamento = id;
                departamentoOperacione.codigoOperacion = codigoOperacion;
                lista.Add(departamentoOperacione);
            }

            contexto.DepartamentoOperaciones.InsertAllOnSubmit(lista);
            contexto.SubmitChanges();

        }

        

        public void guardarOperacionesADepartamento(int[] op, int id)
        {

            var operacionesActuales = contexto.DepartamentoOperaciones.Where(x => x.codigoDepartamento == id).ToList();

            var lista = new List<DepartamentoOperacione>();
            foreach (var codigoOperacion in op)
            {
                var departamentoOperacione = new DepartamentoOperacione();
                departamentoOperacione.codigoDepartamento = id;
                departamentoOperacione.codigoOperacion = codigoOperacion;
                lista.Add(departamentoOperacione);
            }


            contexto.DepartamentoOperaciones.DeleteAllOnSubmit(operacionesActuales);
            contexto.DepartamentoOperaciones.InsertAllOnSubmit(lista);
           
            contexto.SubmitChanges();
           

        

            
        }

        
    }
}