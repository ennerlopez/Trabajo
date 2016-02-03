using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Dto;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Reglas
{
    public interface IStrategy
    {
        bool isExecute { get; set; }
        void execute(Planificacion planificacion, int sumaBloqueActual, int bloqueActual, HojaBultos bulto);
        //void execute(Planificacion planificacion);
    }
}