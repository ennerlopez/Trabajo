using AutoMapper;
using MVC3AplicacionPrueba.Models;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Mapeo
{
    public class MapeoCortesAPlanificarToDiccionario:Profile
    {
        protected override void Configure()
        {
            CreateMap<CortesAPlanificar_vw, Diccionario>()
                .ForMember(diccionario => diccionario.text, expresion => expresion.MapFrom(vw => vw.codigo));

        }
    }
}