using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Dto;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;

namespace MVC3AplicacionPrueba.Mapeo
{
    public class MapeoBultosToHojaBloques:Profile
    {

        protected override void Configure()
        {
            CreateMap<HojaBultos, MyHojaBloques>()
                .ForMember(x => x.corte, expression => expression.MapFrom(bulto => bulto.codigoCorte))
                .ForMember(x => x.capaBulto, expression => expression.MapFrom(bulto => bulto.capaCorte))
                .ForMember(x => x.cantidad, expression => expression.MapFrom(bulto => bulto.cantidadRestante))
                .ForMember(x => x.tallaCompleta, expression => expression.MapFrom(bulto => bulto.tallaCompleta))
                .ForMember(x => x.serie, expression => expression.MapFrom(bulto => bulto.serie))
                .ForMember(x => x.seccion, expression => expression.MapFrom(bulto => bulto.numeroSeccion));
        }
    }
}