using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Web;
using AutoMapper;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Dto;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Reglas;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class HojaBloquesRepositorio
    {
        private readonly DataContextoDataContext _contexto;
        private readonly IStrategyCaller _strategyCaller;

        public HojaBloquesRepositorio(DataContextoDataContext contexto, IEnumerable<IStrategy> reglas)
        {
            if (contexto == null) throw new ArgumentNullException("contexto");
            if (reglas == null) throw new ArgumentNullException("reglas");
            _contexto = contexto;
            _strategyCaller = new StrategyCaller(reglas);
        }

        public ModelHojaBloques getModelHojaBloques()
        {

            var lista = _contexto.CortesAPlanificar_vws;
            var model = new ModelHojaBloques();
            model.cortes.AddRange(
                lista.Select(cortesAPlanificarVw => Mapper.DynamicMap<Diccionario>(cortesAPlanificarVw)));
            model.colores.AddRange(new List<Diccionario>()
                {
                    new Diccionario() {text = "Verde", value = 1},
                    new Diccionario() {text = "Rojo", value = 2},
                    new Diccionario() {text = "Azul", value = 3}
                });
            model.lineas.AddRange(_contexto.LineasProduccions.Select(x => Mapper.DynamicMap<Diccionario>(x)));

            return model;
        }

        public List<Diccionario> getLineas()
        {
            return _contexto.LineasProduccions.Select(x => Mapper.DynamicMap<Diccionario>(x)).ToList();
        }


        public ModelHojaBloques getModelHojaBloques(ModelPreguntaReporteHojaBloque pregunta)
        {
            var hojaBloques =
                _contexto.HojaBloques.Where(
                    x =>
                    x.semana == pregunta.semana && x.year == pregunta.year && x.color == pregunta.color &&
                    x.codigoLinea == pregunta.linea);

            var lista = _contexto.CortesAPlanificar_vws;
            var model = new ModelHojaBloques();
            model.semana = pregunta.semana;
            model.color = pregunta.color;
            model.linea = pregunta.linea;
            model.bloques.AddRange(hojaBloques.Select(x => Mapper.DynamicMap<MyHojaBloques>(x)));
            model.cortes.AddRange(
                lista.Select(cortesAPlanificarVw => Mapper.DynamicMap<Diccionario>(cortesAPlanificarVw)));
            model.colores.AddRange(new List<Diccionario>
                {
                    new Diccionario() {text = "Verde", value = 1},
                    new Diccionario() {text = "Rojo", value = 2},
                    new Diccionario() {text = "Azul", value = 3}
                });
            model.lineas.AddRange(_contexto.LineasProduccions.Select(x => Mapper.DynamicMap<Diccionario>(x)));


            return model;
        }

        public IEnumerable<MyHojaBloques> getPlanificacion(Planificacion planificacion)
        {

            planificacion.bultos =
                _contexto.CortesDisponiblesParaPlanificar_vws.Where(x => x.codigoCorte == planificacion.datos.corte)
                         .Select(x => Mapper.DynamicMap<HojaBultos>(x))
                         .ToList()
                         .OrderBy(x => x.serie)
                         .ToList();

            _strategyCaller.runRules(planificacion);



            return planificacion.bloques;
        }

        public void guardarPlanificacion(IEnumerable<MyHojaBloques> bloques)
        {
            var hojaBloques = bloques.Select(Mapper.DynamicMap<HojaBloque>);


            var hojaBultos = ModificarCantidadRestanteDe(bloques); //editar

            _contexto.HojaBloques.InsertAllOnSubmit(hojaBloques);
            _contexto.SubmitChanges();
        }



        public bool existePlanificacion(ModelPreguntaReporteHojaBloque model)
        {
            return
                _contexto.HojaBloques.Any(
                    x =>
                    x.semana == model.semana && x.year == model.year && x.color == model.color &&
                    x.codigoLinea == model.linea);
        }

        public void editarPlanificacion(IEnumerable<MyHojaBloques> bloques, ModelHojaBloques model)
        {
            var eliminar =
                _contexto.HojaBloques.Where(
                    x =>
                    x.semana == model.semana && x.year == model.year && x.color == model.color &&
                    x.codigoLinea == model.linea);

            var hojaBloques = bloques.Select(Mapper.DynamicMap<HojaBloque>);

            var hojaBultos = ModificarCantidadRestanteDe(bloques);

            _contexto.HojaBloques.DeleteAllOnSubmit(eliminar);
            _contexto.HojaBloques.InsertAllOnSubmit(hojaBloques);
            _contexto.SubmitChanges();
        }

        private List<HojaBulto> ModificarCantidadRestanteDe(IEnumerable<MyHojaBloques> bloques)
        {
            var lista = new List<HojaBulto>();
            foreach (var hojaBloque in bloques)
            {
                if (hojaBloque.serie == 0) continue;
                var strings = hojaBloque.capaBulto.Split('-');
                var seccion = Convert.ToInt32(strings[0]);
                var numeroBultoCapa = strings[1];

                var numeroBulto = Convert.ToInt32(numeroBultoCapa.Substring(0, numeroBultoCapa.Length - 1));
                var capa = numeroBultoCapa.Last();

                //var hojaBultos = _contexto.HojaBultos.FirstOrDefault(x => x.numeroBulto == numeroBulto && x.codigoCorte == hojaBloque.corte && x.nroSeccion == seccion && x.capaCorte == capa.ToCharArray()[0]);

                var query = from a in _contexto.HojaBultos
                            where
                                a.numeroBulto == numeroBulto &&
                                a.codigoCorte == hojaBloque.corte &&
                                a.nroSeccion == seccion &&
                                a.capaCorte == capa &&
                                a.cantidadRestante != 0
                            select a;
                var hojaBultos = query.FirstOrDefault();
                if (hojaBultos == null) continue;
                hojaBultos.cantidadRestante = hojaBultos.cantidadRestante - hojaBloque.cantidad;
                lista.Add(hojaBultos);
            }
            return lista;
        }

        public bool existHojaBloque(int semana, int year, string color, int linea)
        {
            return
                _contexto.HojaBloques.Any(
                    x => x.semana == semana && x.year == year && x.color == color && x.codigoLinea == linea);
        }

        public IEnumerable<MyHojaBloques> getBloques(int semana, int year, string color, int linea)
        {
            var lineaProduccion = _contexto.LineasProduccions.FirstOrDefault(x => x.codigoLinea == linea);
            var nombreLinea = "";
            if (lineaProduccion != null)
            {
                nombreLinea = lineaProduccion.nombreLinea;
            }
            var bloques =
                _contexto.HojaBloques.Where(
                    x => x.semana == semana && x.year == year && x.color == color && x.codigoLinea == linea)
                         .Select(Mapper.DynamicMap<MyHojaBloques>).ToList();
            
            foreach (var myHojaBloquese in bloques)
            {
                myHojaBloquese.nombreLinea = nombreLinea;
            }
            return bloques;

        }
    }


}