using System;
using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Reglas;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class StrategyCaller : IStrategyCaller
    {
        private readonly IEnumerable<IStrategy> _strategies;
        private readonly ActualizarValoresRestantes _actualizarValoresRestantes;
        private readonly VerificarSerieConsecutiva _verificarSerieConsecutiva;
        private readonly YaSeEncuetraCompletado _yaSeEncuetraCompletado;

        public StrategyCaller(IEnumerable<IStrategy> strategies)
        {
            if (strategies == null) throw new ArgumentNullException("strategies");
            _strategies = strategies;
            _actualizarValoresRestantes = new ActualizarValoresRestantes();
            _verificarSerieConsecutiva = new VerificarSerieConsecutiva();
            _yaSeEncuetraCompletado = new YaSeEncuetraCompletado();
        }

       
        public void runRules(Planificacion planificacion)
        {
            _actualizarValoresRestantes.execute(planificacion);
            _verificarSerieConsecutiva.execute(planificacion);
            _yaSeEncuetraCompletado.execute(planificacion);

            planificar(planificacion);
        }

        
        private int getBloque(Planificacion planificacion)
        {
            if (!planificacion.bloques.Any()) return 1;
            var miBloque = planificacion.bloques.LastOrDefault().bloque;

            var sum = planificacion.bloques.Where(x => x.bloque == miBloque).Sum(x => x.cantidad);
            if (sum == planificacion.datos.capacidadXHora)
            {
                miBloque = miBloque + 1;
            }
            return miBloque;
        }


        private void planificar(Planificacion planificacion, bool continuar = true)
        {
            var vaAContinuar = true;
            if (planificacion.bultos.Count(x => x.cantidadRestante > 0) == 0 || continuar == false)
            {

            }
            else
            {   var bloqueAUtilizar = getBloque(planificacion);
                
                if (bloqueAUtilizar >44)
                {
                    vaAContinuar = false;
                }
                else
                {
                    var bulto = planificacion.bultos.Where(x => x.cantidadRestante > 0).ToList()[0];
                 
                    var sum = planificacion.bloques.Where(x => x.bloque == bloqueAUtilizar).Sum(x => x.cantidad);

                    if (sum < planificacion.datos.capacidadXHora)
                    {
                       // var cantidadParaCompletarElBloque = planificacion.datos.capacidadXHora - sum;

                        foreach (var strategy in _strategies)
                        {
                            strategy.execute(planificacion,sum,bloqueAUtilizar,bulto);
                            if (strategy.isExecute) break;

                        }

                        foreach (var strategy in _strategies)
                        {
                            strategy.isExecute = false;
                        }
                    }

                }
                planificar(planificacion, vaAContinuar);
            }
        }
    }
}