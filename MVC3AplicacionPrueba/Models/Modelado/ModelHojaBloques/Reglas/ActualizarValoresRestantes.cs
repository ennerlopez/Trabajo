using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Reglas
{
    public class ActualizarValoresRestantes
    {
        public void execute(Planificacion planificacion)
        {
            foreach (var bloque in planificacion.bloques)
            {
                foreach (var bulto in planificacion.bultos)
                {
                    if (bloque.capaBulto == bulto.capaCorte && bloque.seccion == bulto.numeroSeccion)
                    {
                        bulto.cantidadRestante = bulto.cantidadRestante - bloque.cantidad;
                    }
                }
            }
        }
    }
}