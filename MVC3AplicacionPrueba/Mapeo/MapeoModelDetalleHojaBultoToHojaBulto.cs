using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBultos;

namespace MVC3AplicacionPrueba.Mapeo
{
    public class MapeoModelDetalleHojaBultoToHojaBulto : Profile
    {

        protected override void Configure()
        {
            CreateMap<ModelDetalleHojaBulto, HojaBulto>()
                .ForMember(bulto => bulto.serie, expression => expression.MapFrom(dto => dto.serie))
                .ForMember(bulto => bulto.numeroBulto, expression => expression.MapFrom(dto => dto.numeroBulto))
                .ForMember(bulto => bulto.nroSeccion, expression => expression.MapFrom(dto => dto.nroSeccion))
                .ForMember(bulto => bulto.tallaCompleta, expression => expression.MapFrom(dto => dto.tallaCompleta))
                .ForMember(bulto => bulto.cantidad, expression => expression.MapFrom(dto => dto.cantidaHistorico))
                .ForMember(bulto => bulto.cantidadCortada, expression => expression.MapFrom(dto => dto.cantidad));


            CreateMap<HojaBulto, ModelDetalleHojaBulto>()
                .ForMember(bulto => bulto.serie, expression => expression.MapFrom(dto => dto.serie))
                .ForMember(bulto => bulto.numeroBulto, expression => expression.MapFrom(dto => dto.numeroBulto))
                .ForMember(bulto => bulto.nroSeccion, expression => expression.MapFrom(dto => dto.nroSeccion))
                .ForMember(bulto => bulto.tallaCompleta, expression => expression.MapFrom(dto => dto.tallaCompleta))
                .ForMember(bulto => bulto.cantidaHistorico, expression => expression.MapFrom(dto => dto.cantidad))
                .ForMember(bulto => bulto.cantidad, expression => expression.MapFrom(dto => dto.cantidadCortada));

        }
    }
}