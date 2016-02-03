using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelOrdenes;
using MVC3AplicacionPrueba.Models.Util;
using MVC3AplicacionPrueba.Reportes.Entidades;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class RepositorioOrden
    {
        private readonly DataContextoDataContext _contexto; 
        
        
        public RepositorioOrden(DataContextoDataContext contexto)
        {
            _contexto = contexto;
        }


        public List<ModelOrden> listaDeOrdenesNoCortadas()
        {
            var lista = new List<ModelOrden>();

            var query = from o in _contexto.Cortes
                        where o.fechaCortado == null
                        select o;
            foreach (var corte in query)
            {
                var orden = new ModelOrden();
                orden.codigo = corte.codigoCorte;
                orden.nombreCliente = getNombreCliente(corte.codigoCliente);
                orden.fechaCorte = corte.fechaCorte.ToShortDateString();
                orden.descripcionTieneHojaBulto = corte.tieneHojaBulto==null||corte.tieneHojaBulto ==0? "NO":"SI";
                orden.cantida = corte.totalCamisasCorte;
                lista.Add(orden);
            }
            return lista;
        }

        private string getNombreCliente (int codigo)
        {
            return _contexto.Clientes.Single(x => x.codigoCliente == codigo).nombreCliente;
        }

        public List<Diccionario> listaEstilos()
        {
            var contextoEstilosToList = _contexto.Estilos.ToList();
            var listaDiccionario = new List<Diccionario>();
            
            foreach (var estilo in contextoEstilosToList)
            {
                var diccionario = new Diccionario();
                diccionario.value = estilo.codigoEstilo;
                diccionario.text = estilo.nombreEstilo+" - "+ estilo.comentario;
                listaDiccionario.Add(diccionario);
            }
            return listaDiccionario;
        }


        public List<Diccionario> listaClientes()
        {
            var listaCliente = _contexto.Clientes.ToList();
           var listaDiccionario = new List<Diccionario>();


            foreach(var cliente in listaCliente)
            {
                var diccionario = new Diccionario();
                diccionario.value = cliente.codigoCliente;
                diccionario.text = cliente.nombreCliente;

                listaDiccionario.Add(diccionario);

            }
            return listaDiccionario;
        }

        public List<Diccionario> listaTallasCliente(int id)
        {
            var listaCliente = _contexto.TallasClientes.Where(x=>x.codigoCliente==id).ToList();
           var listaDiccionario = new List<Diccionario>();


            foreach(var cliente in listaCliente)
            {
                var diccionario = new Diccionario();
                diccionario.value = cliente.codigoCliente;
                diccionario.text = cliente.tallaCompleta;

                listaDiccionario.Add(diccionario);

            }
            return listaDiccionario;
        }
        
        public bool OrdenYaFueProcesada(string codigoOrden)
        {
            bool vf = false;
            
            var orden = _contexto.Cortes.Single(x => x.codigoCorte == codigoOrden);
            if (orden.fechaCortado != null)
            {
                vf = true;
            }

            return vf;
        }


        public ModelOrden ObtenerOrden(string codigoOrden)
        {
           var orden =  _contexto.Cortes.Single(x => x.codigoCorte == codigoOrden);
            
            
            var modelOrden = new ModelOrden();
            modelOrden.codigo = orden.codigoCorte;
            modelOrden.proyecto =orden.proyectoCorte.ToString();
            modelOrden.fechaCorte = orden.fechaCorte.ToShortDateString();
            modelOrden.codigoCliente = orden.codigoCliente;
            modelOrden.custPoCorte = orden.custPoCorte;
            modelOrden.cantida = orden.totalCamisasCorte;
            modelOrden.codigoEstilo = orden.codigoEstilo;
            modelOrden.tela = orden.telaCorte;
            modelOrden.pinado = (orden.pinadoCorte==1);
            modelOrden.consumoTela = orden.consumoTela;
            modelOrden.comentario = orden.comentarioCorte;
            modelOrden.listaCliente = listaClientes();
            modelOrden.listaEstilos = listaEstilos();
            modelOrden.listaDetalleOrden = ObtenerDetalleOrden(codigoOrden);
            return modelOrden;
        }


        private List<ModelDetalleOrden> ObtenerDetalleOrden(string codigo)
        {
           var lista= new List<ModelDetalleOrden>();
            var detalleOrden = _contexto.CorteDetalles.Where(x => x.codigoCorte == codigo);
            foreach (var corteDetalle in detalleOrden)
            {
                var modelDetalleOrden = new ModelDetalleOrden();
                modelDetalleOrden.codigoOrden = corteDetalle.codigoCorte;
                modelDetalleOrden.tallaCuello = corteDetalle.tallaCuello;
                modelDetalleOrden.tallaManga = corteDetalle.tallaManga;
                modelDetalleOrden.tallaLetra = corteDetalle.tallaLetra;
                modelDetalleOrden.cantidad = Convert.ToInt32(corteDetalle.cantidad);
                lista.Add(modelDetalleOrden);
            }
            return lista;
        }

        public void guardarOrden(ModelResumenOrden resumenOrden, ModelDetalleOrden[] listaModelDetalleOrdenes)
        {
            if (listaModelDetalleOrdenes == null)
            {
                throw new Exception("No Se Ingreso ninguna talla");
            }

            //RESUMEN
            var corte= new Corte();
            corte.codigoCorte = resumenOrden.codigo.ToUpper();
            corte.codigoUsuario = resumenOrden.codigoUsuario;
            corte.proyectoCorte = resumenOrden.proyecto;
            
            IFormatProvider format  = new CultureInfo("es-ES", true);
            corte.fechaCorte = DateTime.Parse(resumenOrden.fecha, format);
            corte.codigoCliente = resumenOrden.codigoCliente;
            corte.custPoCorte = resumenOrden.custPoCorte;
            corte.totalCamisasCorte = resumenOrden.cantidaTotal;
            corte.codigoEstilo = resumenOrden.codigoEstilo;
            corte.telaCorte = resumenOrden.tela;
            corte.pinadoCorte = (resumenOrden.pinado)?1:0;
            corte.consumoTela = Convert.ToDecimal(resumenOrden.consumoTela);
            corte.comentarioCorte = resumenOrden.comentario;
            
            //DETALLE
            var listaCorte = new List<CorteDetalle>();
            foreach (var detalle in listaModelDetalleOrdenes)
            {
                var corteDetalle = new CorteDetalle();
                corteDetalle.codigoCorte = detalle.codigoOrden;
                corteDetalle.tallaCuello = detalle.tallaCuello;
                corteDetalle.tallaManga = detalle.tallaManga;
                corteDetalle.tallaLetra = detalle.tallaLetra;
                corteDetalle.cantidad = detalle.cantidad;
                listaCorte.Add(corteDetalle);
            }
            try
            {
                
                _contexto.Cortes.InsertOnSubmit(corte);
                _contexto.CorteDetalles.InsertAllOnSubmit(listaCorte);
            }
            catch (Exception)
            {
                
                throw;
            }
            finally
            {
                _contexto.SubmitChanges(); 
            }
        
           

        }

        public void EditarOrden(ModelResumenOrden resumenOrden, ModelDetalleOrden[] listaModelDetalleOrdenes)
        {
            if (listaModelDetalleOrdenes == null)
            {
                throw new Exception("No Se Ingreso ninguna talla");
            }

            //RESUMEN
            Corte corte = _contexto.Cortes.Single(x => x.codigoCorte == resumenOrden.codigo);
            corte.codigoCorte = resumenOrden.codigo;
            corte.codigoUsuario = resumenOrden.codigoUsuario;
            corte.proyectoCorte = resumenOrden.proyecto;

            IFormatProvider format = new CultureInfo("es-ES", true);
            corte.fechaCorte = DateTime.Parse(resumenOrden.fecha, format);
            corte.codigoCliente = resumenOrden.codigoCliente;
            corte.custPoCorte = resumenOrden.custPoCorte;
            corte.totalCamisasCorte = resumenOrden.cantidaTotal;
            corte.codigoEstilo = resumenOrden.codigoEstilo;
            corte.telaCorte = resumenOrden.tela;
            corte.pinadoCorte = (resumenOrden.pinado) ? 1 : 0;
            corte.consumoTela = Convert.ToDecimal(resumenOrden.consumoTela);
            corte.comentarioCorte = resumenOrden.comentario;

            //DETALLE
        
            List<CorteDetalle> listaCorte = new List<CorteDetalle>();
            foreach (var detalle in listaModelDetalleOrdenes)
            {
                CorteDetalle corteDetalle = new CorteDetalle();
                corteDetalle.codigoCorte = detalle.codigoOrden;
                corteDetalle.tallaCuello = detalle.tallaCuello;
                corteDetalle.tallaManga = detalle.tallaManga;
                corteDetalle.tallaLetra = detalle.tallaLetra;
                corteDetalle.cantidad = detalle.cantidad;
                listaCorte.Add(corteDetalle);
            }
            try
            {
                var detalleAntiguo = _contexto.CorteDetalles.Where(x => x.codigoCorte == resumenOrden.codigo);
                _contexto.CorteDetalles.DeleteAllOnSubmit(detalleAntiguo);
                _contexto.CorteDetalles.InsertAllOnSubmit(listaCorte);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _contexto.SubmitChanges();
            }



        }

        public List<ModelCapas> getReporteImpresionHojaBultoTallas(string codigo)
        {
            var capasVws = _contexto.Capas_vws.Where(vw => vw.codigoCorte == codigo).ToList();

            var lista = new List<ModelCapas>();
            foreach (var capasVw in capasVws)
            {
                ModelCapas dynamicMap = AutoMapper.Mapper.DynamicMap<ModelCapas>(capasVw);  
                lista.Add(dynamicMap);
            }

      
            return lista;
        }


        public void EliminarOrder(string codigo)
        {
            try
            {
                var corte = _contexto.Cortes.Single(x => x.codigoCorte == codigo);
                var detalle =_contexto.CorteDetalles.Where(x=>x.codigoCorte==codigo);
                _contexto.Cortes.DeleteOnSubmit(corte);
                _contexto.CorteDetalles.DeleteAllOnSubmit(detalle);
             
            }
            catch (Exception)
            {
                
                throw;
            }finally
            {
                _contexto.SubmitChanges();
            } 
        }

        public bool existCorte(string codigo)
        {
            return _contexto.Cortes.Any(corte => corte.codigoCorte == codigo);
        }
    }
}