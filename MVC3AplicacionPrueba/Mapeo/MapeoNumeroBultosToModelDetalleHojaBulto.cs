using AutoMapper;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos.Entidades;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBultos;

namespace MVC3AplicacionPrueba.Mapeo
{
    public class MapeoNumeroBultosToModelDetalleHojaBulto : Profile
    {

        protected override void Configure()
        {
            CreateMap<NumeroBultos, ModelDetalleHojaBulto>()
                .ForMember(bulto => bulto.numeroBulto, expression => expression.MapFrom(nBultos => nBultos.numeroBulto))
                .ForMember(bulto => bulto.nroSeccion, expression => expression.MapFrom(nBultos => nBultos.numeroSeccion))
                .ForMember(bulto=>bulto.capaCorte,expresion=>expresion.MapFrom(nBultos=>nBultos.capa))
                .ForMember(bulto => bulto.tallaCompleta, expression => expression.MapFrom(nBultos => nBultos.tallaCompleta))
                .ForMember(bulto => bulto.cantidad, expression => expression.MapFrom(nBultos => nBultos.cantidadPorCapa))
                .ForMember(bulto => bulto.cantidaHistorico, expression => expression.MapFrom(nBultos => nBultos.cantidadPorCapa)); 
        }
    }
}