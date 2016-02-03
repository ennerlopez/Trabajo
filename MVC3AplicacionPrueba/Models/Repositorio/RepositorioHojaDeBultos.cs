using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos.Entidades;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBultos;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class RepositorioHojaDeBultos
    {
        private readonly DataContextoDataContext _contexto;
        private readonly ServicioGeneracionHojaBulto _servicioGeneracion;
        public RepositorioHojaDeBultos(DataContextoDataContext contexto)
        {
            _contexto = contexto;
            _servicioGeneracion = new ServicioGeneracionHojaBulto(); ;
        }

        public bool existeCorte(string codigoOrden)
        {
            var codigo = codigoOrden.ToUpper();
            return _contexto.Cortes.Count(x => x.codigoCorte == codigo) != 0;
        }

        public bool tieneHojaBulto(string codigoOrden)
        {
            var codigo = codigoOrden.ToUpper();
            return _contexto.Cortes.Count(x => x.codigoCorte == codigo && x.tieneHojaBulto==1) != 0;
        }


        public ModelResumenHojaBultos getHojaDeBultosSegunOrden(string codigoOrden)
        {
            var codigoCorte = codigoOrden.ToUpper();
            var orden = _contexto.Cortes.SingleOrDefault(x => x.codigoCorte == codigoCorte);
            
            if (orden == null) return default(ModelResumenHojaBultos);

            var hojaBulto = _contexto.HojaBultos.Where(x => x.codigoCorte == codigoCorte);

            var fecha = _contexto.Cortes.Single(x => x.codigoCorte == codigoCorte).fechaCortado;
         
    

          
            var fechaCortado = !(fecha == null)
                                   ? fecha.Value.ToString("dd/MM/yyyy")
                                   : "";     
            
            var resumen = new ModelResumenHojaBultos
                              {
                                  codigoOrden = orden.codigoCorte,
                                  cliente = orden.Cliente.nombreCliente,
                                  fechaCortado = fechaCortado
                              };

            var detalle = hojaBulto.Select(bulto => Mapper.DynamicMap<ModelDetalleHojaBulto>(bulto)).ToList();
            
            resumen.listaDetalle = detalle;

            return resumen;

        }


        public ModelResumenHojaBultos getHojaDeBultosParaCreacion(string codigoOrden)
        {
            var codigoCorte = codigoOrden.ToUpper();
            var orden = _contexto.Cortes.SingleOrDefault(x => x.codigoCorte == codigoCorte);

            if (orden == null) return default(ModelResumenHojaBultos);

            //var hojaBulto = _contexto.HojaBultos.Where(x => x.codigoCorte == codigoCorte);
            
            var resumen = new ModelResumenHojaBultos
                              {
                                  codigoOrden = orden.codigoCorte,
                                  cliente = orden.Cliente.nombreCliente,
                                  fechaCortado = ""
                              };

            var tallasClientes = _contexto.TallasClientes.Where(x => x.codigoCliente == orden.codigoCliente);

            var diccionarios = tallasClientes.Select(tallasCliente => new Diccionario {text = tallasCliente.tallaCompleta}).ToList();

            resumen.listaTallas = diccionarios;
            return resumen;

        }

        public ModelResumenHojaBultos getHojaDeBultosParaEditar(string codigoOrden)
        {
            var codigoCorte = codigoOrden.ToUpper();
            var orden = _contexto.Cortes.SingleOrDefault(x => x.codigoCorte == codigoCorte);

            if (orden == null) return default(ModelResumenHojaBultos);

            var hojaBulto = _contexto.HojaBultos.Where(x => x.codigoCorte == codigoCorte);

            var resumen = new ModelResumenHojaBultos
            {
                codigoOrden = orden.codigoCorte,
                cliente = orden.Cliente.nombreCliente,
                fechaCortado = ""
            };

            var tallasClientes = _contexto.TallasClientes.Where(x => x.codigoCliente == orden.codigoCliente);

            var diccionarios = tallasClientes.Select(tallasCliente => new Diccionario { text = tallasCliente.tallaCompleta }).ToList();

            resumen.listaTallas = diccionarios;
            resumen.listaDetalle = hojaBulto.Select(x => Mapper.DynamicMap<ModelDetalleHojaBulto>(x)).ToList();
            return resumen;

        }

        public void confirmarOrden(ModelResumenHojaBultos resumenOrden, IEnumerable<ModelDetalleHojaBulto> detalleOrden)
        {
            var listaOriginalBd = _contexto.HojaBultos.Where(x => x.codigoCorte == resumenOrden.codigoOrden);


            var nuevaListaHojaBulto = new List<HojaBulto>();
           
            foreach (var detalle in detalleOrden)
            {
                var hoja = Mapper.DynamicMap<HojaBulto>(detalle);
                hoja.codigoCorte = resumenOrden.codigoOrden;
                hoja.capaCorte = detalle.capaCorte.ToCharArray()[0];
                hoja.cantidadCortada = detalle.cantidad;
                hoja.cantidadRestante = detalle.cantidad;
                nuevaListaHojaBulto.Add(hoja);
            }


            try
            {
                var singleOrDefault = _contexto.Cortes.SingleOrDefault(x => x.codigoCorte == resumenOrden.codigoOrden);
                IFormatProvider format = new CultureInfo("es-ES", true);
                if (singleOrDefault != null) singleOrDefault.fechaCortado =DateTime.Parse(resumenOrden.fechaCortado,format);

                _contexto.HojaBultos.DeleteAllOnSubmit(listaOriginalBd);
                _contexto.HojaBultos.InsertAllOnSubmit(nuevaListaHojaBulto);
              
            }
            catch (Exception)
            {
                
                throw;
            }finally
            {
                _contexto.SubmitChanges();
            }
           
        }


        public void guardarHojaBultosQueSeCreo(ModelResumenHojaBultos resumenOrden,
                                               IEnumerable<ModelDetalleHojaBulto> detalleOrden)
        {
            var nuevaListaHojaBulto = detalleOrden.Select(detalle=>getHojaBulto(detalle,resumenOrden.codigoOrden)).ToList();


            try
            {
               _contexto.HojaBultos.InsertAllOnSubmit(nuevaListaHojaBulto);
                var updateCorte = _contexto.Cortes.SingleOrDefault(x => x.codigoCorte == resumenOrden.codigoOrden);
                updateCorte.tieneHojaBulto = 1;


                var hojaBultos = _contexto.HojaBultos.Where(x => x.codigoCorte == resumenOrden.codigoOrden);
                if (hojaBultos.Any())
                {
                    _contexto.HojaBultos.DeleteAllOnSubmit(hojaBultos);
                    _contexto.SubmitChanges();
                }
                
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

   

        private HojaBulto getHojaBulto(ModelDetalleHojaBulto model, string codigoCorte)
        {
            var hojabulto = Mapper.DynamicMap<HojaBulto>(model);
            hojabulto.codigoCorte = codigoCorte;
            hojabulto.cantidadRestante = hojabulto.cantidadCortada;
            return hojabulto;
        }

        public void guardarHojaBultosQueSeGenero(ModelResumenHojaBultos resumenOrden, IEnumerable<ModelDetalleHojaBulto> detalleOrden)
        {

            var nuevaListaHojaBulto = new List<HojaBulto>();

            foreach (var detalle in detalleOrden)
            {   
                var hoja = Mapper.DynamicMap<HojaBulto>(detalle);
                hoja.codigoCorte = resumenOrden.codigoOrden;
                hoja.capaCorte = detalle.capaCorte.ToCharArray()[0];
                hoja.cantidadRestante = hoja.cantidadCortada;
                
                nuevaListaHojaBulto.Add(hoja);
                
            }


            try
            {
                var hojaBultos = _contexto.HojaBultos.Where(x => x.codigoCorte == resumenOrden.codigoOrden);
              if(hojaBultos.Any()) _contexto.HojaBultos.DeleteAllOnSubmit(hojaBultos);

                _contexto.HojaBultos.InsertAllOnSubmit(nuevaListaHojaBulto);
                var updateCorte = _contexto.Cortes.SingleOrDefault(x => x.codigoCorte == resumenOrden.codigoOrden);
                updateCorte.tieneHojaBulto = 1;

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


        public IList<NumeroBultos> getListaGenerada(string codigoOrden,int capasPorTalla,int repeticionesDeCuerpo)
        {
            var listaDesdeOrden = getListaDesdeOrden(codigoOrden);

            return _servicioGeneracion.getHojaBulto(codigoOrden,listaDesdeOrden,capasPorTalla,repeticionesDeCuerpo);
        }

        public IList<ModelDetalleHojaBulto> getListaGenerada(int serieComienza,string codigoOrden, int capasPorTalla, int repeticionesDeCuerpo, int maximoSplitPorCapa)
        {
            var listaDesdeOrden = getListaDesdeOrden(codigoOrden);
            var servicioGeneraciongetHojaBulto = _servicioGeneracion.getHojaBulto(codigoOrden, listaDesdeOrden, capasPorTalla, repeticionesDeCuerpo, maximoSplitPorCapa); ;
            var lista = new List<ModelDetalleHojaBulto>();
            var contador = 0;
            foreach (var numeroBultos in servicioGeneraciongetHojaBulto)
            {
               
                var modelDetalleHojaBulto = Mapper.DynamicMap<ModelDetalleHojaBulto>(numeroBultos);
                if (contador == 0)
                {
                    modelDetalleHojaBulto.serie = serieComienza;
                    contador++;
                }
                else
                {
                    serieComienza++;
                    modelDetalleHojaBulto.serie = serieComienza;
                    
                }

                lista.Add(modelDetalleHojaBulto);
            }

            return lista;
        }



        private List<TallaCantidad> getListaDesdeOrden(string codigoOrden)
        {
                  var listaCorteDetalleOrdenadoPorCantidad = from q in _contexto.CorteDetalles
                        where q.codigoCorte == codigoOrden
                        orderby q.cantidad descending
                        select q;
            
            var primeraListaTallaCantidad = new List<TallaCantidad>();
            
            foreach (var corteDetalle in listaCorteDetalleOrdenadoPorCantidad)
            {
                var talla = new TallaCantidad();
                talla.tallaCuello = corteDetalle.tallaCuello;
                talla.tallaManga = corteDetalle.tallaManga;
                talla.tallaLetra = corteDetalle.tallaLetra;
                talla.cantidad = (int)corteDetalle.cantidad;
                primeraListaTallaCantidad.Add(talla);
            }
            return primeraListaTallaCantidad;
        }
    }
}