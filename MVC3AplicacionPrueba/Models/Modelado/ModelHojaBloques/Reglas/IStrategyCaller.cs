using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Reglas
{
    public interface IStrategyCaller
    {
        void runRules(Planificacion planificacion);
    }
}