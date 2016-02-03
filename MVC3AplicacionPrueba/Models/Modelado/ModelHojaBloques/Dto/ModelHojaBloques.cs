using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Dto
{
    public class ModelHojaBloques
    { 
        public ModelHojaBloques()
        {
           cortes= new List<Diccionario>();
           colores = new List<Diccionario>();
            bloques = new List<MyHojaBloques>();
            lineas = new List<Diccionario>();
            corte = string.Empty;
            capacidadXHora = 0;
            color = string.Empty;
            semana = 0;
            year = 0;

        }

        public List<MyHojaBloques> bloques { get; set; } 
        public List<Diccionario> cortes { get; set; }
        public List<Diccionario> colores { get; set; }
        public List<Diccionario> lineas { get; set; } 
        public string corte { get; set; }
        public int capacidadXHora { get; set; }
        public string color { get; set; }
        public int semana { get; set; }
        public int year { get; set; }
        public int linea { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (ModelHojaBloques) && Equals((ModelHojaBloques) obj);
        }

        protected bool Equals(ModelHojaBloques other)
        {
            return string.Equals(corte, other.corte) && capacidadXHora == other.capacidadXHora && semana == other.semana && string.Equals(color, other.color) && year == other.year;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = corte.GetHashCode();
                hashCode = (hashCode*397) ^ capacidadXHora;
                hashCode = (hashCode*397) ^ semana;
                hashCode = (hashCode*397) ^ color.GetHashCode();
                hashCode = (hashCode*397) ^ year;
                return hashCode;
            }
        }
    }
}