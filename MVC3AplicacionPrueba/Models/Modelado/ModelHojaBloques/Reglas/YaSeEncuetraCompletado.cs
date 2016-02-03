using System;
using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Reglas
{
    public class YaSeEncuetraCompletado
    {

        public void execute(Planificacion planificacion)
        {
            var sum = planificacion.bloques.Where(x => x.bloque == 44).Sum(x => x.cantidad);

           if (sum == planificacion.datos.capacidadXHora)
           {
               throw new Exception("La planificación esta completa.");
           }
        }

      
    }
}