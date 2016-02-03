using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Dto;

namespace MVC3AplicacionPrueba.Mapeo
{
    public class MapeoCortesDisponiblesAPlanificarToHojaBultos:Profile
    {
        protected override void Configure()
        {
            CreateMap<Models.CortesDisponiblesParaPlanificar_vw, HojaBultos>().ForMember(

                x => x.codigoCorte, expression => expression.MapFrom(bultos => bultos.codigoCorte))
                                                                              .ForMember(x => x.cantidadRestante,
                                                                                         expression =>
                                                                                         expression.MapFrom(
                                                                                             bultos =>
                                                                                             bultos.cantidadRestante))
                                                                              .ForMember(x => x.capaCorte,
                                                                                         expression =>
                                                                                         expression.MapFrom(
                                                                                             bultos => bultos.capaCorte))
                                                                              .ForMember(x => x.numeroSeccion,
                                                                                         expression =>
                                                                                         expression.MapFrom(
                                                                                             bultos =>
                                                                                             bultos.nroSeccion))
                                                                              .ForMember(x => x.numeroBultos,
                                                                                         expression =>
                                                                                         expression.MapFrom(
                                                                                             bultos =>
                                                                                             bultos.numeroBulto))
                                                                              .ForMember(x => x.serie,
                                                                                         expression =>
                                                                                         expression.MapFrom(
                                                                                             bultos => bultos.serie))
                                                                              .ForMember(x => x.tallaCompleta,
                                                                                         expression =>
                                                                                         expression.MapFrom(
                                                                                             bultos =>
                                                                                             bultos.tallaCompleta));

        }
    }
}