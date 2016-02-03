using AutoMapper;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Mapeo
{
    public class MapeoLineasProduccionToDiccionario:Profile
    {
        protected override void Configure()
        {
            CreateMap<LineasProduccion, Diccionario>()
                .ForMember(diccionario => diccionario.value,
                           expression => expression.MapFrom(produccion => produccion.codigoLinea))
                .ForMember(diccionario => diccionario.text,
                           expression => expression.MapFrom(produccion => produccion.nombreLinea));
        }
    }
}