using System;
using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Reglas
{
    public class VerificarSerieConsecutiva
    {

        public void execute(Planificacion planificacion)
        {
            var bulto =planificacion.bultos.Where(x => x.cantidadRestante != 0).ToList()[0];
            var ultimo = planificacion.bloques.LastOrDefault(x => x.serie != 0);
            
            if (ultimo != null && ultimo.serie + 1 != bulto.serie)
            {
                throw new Exception(bulto.codigoCorte + " con serie incorrecta");
            }
        }
    }
}