using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Dto;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Reglas
{
    public class CantidadParaCompletarBloqueMenorACantidadDeBulto:IStrategy
    {
        public bool isExecute { get; set; }
     
        public CantidadParaCompletarBloqueMenorACantidadDeBulto()
        {
            isExecute = false;
        }
        
        public void execute(Planificacion planificacion, int sumaBloqueActual, int bloqueActual, HojaBultos bulto)
        {
            var cantidadParaCompletarElBloque = planificacion.datos.capacidadXHora - sumaBloqueActual;
            
            if (cantidadParaCompletarElBloque < bulto.cantidadRestante)
            {
                var hojaBloques = new MyHojaBloques()
                {
                    bloque = bloqueActual,
                    capaBulto = bulto.numeroSeccion + "-" + bulto.numeroBultos + bulto.capaCorte,
                    cantidad = cantidadParaCompletarElBloque,
                    corte = bulto.codigoCorte,
                    seccion = bulto.numeroSeccion,
                    serie = bulto.serie,
                    color = planificacion.datos.color,
                    semana = planificacion.datos.semana,
                    year = planificacion.datos.year,
                    tallaCompleta = bulto.tallaCompleta

                    

                };
                bulto.cantidadRestante = bulto.cantidadRestante - cantidadParaCompletarElBloque;
                planificacion.add(hojaBloques);
                isExecute = true;
            }

        }
    }
}