using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using MVC3AplicacionPrueba.Reportes;
using MVC3AplicacionPrueba.Reportes.Entidades;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class RepositorioCupon
    {
        private readonly DataContextoDataContext _contexto;

        public RepositorioCupon(DataContextoDataContext contexto)
        {
            _contexto = contexto;
        }

        public List<CuponModel> getCuponesCorte(string codigoCorte)
        {
            var queryCuponVw = from c in _contexto.Cupones_vws
                          where c.codigoCorte ==codigoCorte
                          select c;
            var listaCupones = queryCuponVw.Select(cuponVw => Mapper.DynamicMap<CuponModel>(cuponVw)).ToList();
            
            return listaCupones;
        }

        public bool confirmoOrden(string codigoCorte)
        {
            return _contexto.Cortes.Any(x => x.codigoCorte == codigoCorte && x.fechaCortado != null);
        } 
        public bool existCorte(string codigoCorte)
        {
            return _contexto.Cupons.Any(x => x.codigoCorte == codigoCorte);
        }

        private bool existeCorteCuponVw(string codigoCorte)
        {
        return _contexto.Cupones_vws.Any(x => x.codigoCorte == codigoCorte);
        }

        public string saveCuponesCorte(string codigoCorte)
        {
            if(!confirmoOrden(codigoCorte)) throw new Exception("Por favor confirmar unidades cortadas");
            if(!existeCorteCuponVw(codigoCorte)) throw new Exception("Por Favor Verifique La Orden");
            if (existCorte(codigoCorte)) return "Cupones Para Este Corte Ya Existen";
            
            var queryCuponVw = from c in _contexto.Cupones_vws
                               where c.codigoCorte == codigoCorte
                               select c;
            var listaCupones = queryCuponVw.Select(cuponVw => Mapper.DynamicMap<Cupon>(cuponVw)).ToList();

            _contexto.Cupons.InsertAllOnSubmit(listaCupones);
            _contexto.SubmitChanges();

            return "Cupones Generado Correctamente";
        }

        public string eliminarCupones(string codigoCorte)
        {
            if (!existCorte(codigoCorte)) throw new Exception("Orden no tiene cupones");
            
            var cuponesAEliminar = from x in _contexto.Cupons
                                   where x.codigoCorte == codigoCorte
                                   select x;

            _contexto.Cupons.DeleteAllOnSubmit(cuponesAEliminar);
            _contexto.SubmitChanges();

            return "Cupones Eliminados correctamente";
        }

        public List<CuponModel> getReportCupones(string codigo)
        {
            var codigoCorte = codigo;
        
            var queryable = _contexto.Cupons.Where(x => x.codigoCorte == codigoCorte);

            var cuponModels = queryable.Select(x => Mapper.DynamicMap<CuponModel>(x)).ToList();

            foreach (var cuponModel in cuponModels)
            {
                cuponModel.qrCode =
                    HelperQrCode.generar(string.Format("{0}|{1}|{2}|{3}|{4}", cuponModel.nroCupon, cuponModel.cantidadCortada, cuponModel.tiempoCupon, cuponModel.valorCupon,cuponModel.serie));
            }
            return cuponModels;
        }

    }
}