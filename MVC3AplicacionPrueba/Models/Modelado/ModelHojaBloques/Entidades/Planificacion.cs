using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Dto;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades
{
    public class Planificacion
    {
        public List<MyHojaBloques> bloques { get; set; }
        public List<HojaBultos> bultos { get; set; }
        public string mensaje { get; set; }


        public Planificacion()
        {
            bloques = new List<MyHojaBloques>();
            bultos = new List<HojaBultos>();
            mensaje = "Transacción realizada correctamente";
        }

        public void add(MyHojaBloques myHojaBloques)
        {
            bloques.Add(myHojaBloques);
        }

        public void addRange(IEnumerable<MyHojaBloques> lista)
        {
            bloques.AddRange(lista);
        }

        public Dto.ModelHojaBloques datos { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == typeof (Planificacion) && Equals((Planificacion) obj);
        }

        protected bool Equals(Planificacion other)
        {
            return bloques.Equals(other.bloques) && datos.Equals(other.datos);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (bloques.GetHashCode()*397) ^ datos.GetHashCode();
            }
        }
    }
}