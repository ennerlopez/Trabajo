namespace MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades
{
    public class MyHojaBloques
    {
        public int bloque { get; set; }
        public int seccion { get; set; }
        public string corte { get; set; }
        public string capaBulto { get; set; }
        public int cantidad { get; set; }
        public string tallaCompleta { get; set; }
        public int serie { get; set; }
        public string color { get; set; }
        public int semana { get; set; }
        public int year { get; set; }
        public int codigoLinea { get; set; }
        public string nombreLinea { get; set; }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (MyHojaBloques)) return false;
            return Equals((MyHojaBloques) obj);
        }

        protected bool Equals(MyHojaBloques other)
        {
            return bloque == other.bloque && seccion == other.seccion && string.Equals(capaBulto, other.capaBulto) &&
                   string.Equals(tallaCompleta, other.tallaCompleta) && string.Equals(color, other.color) &&
                   semana == other.semana && year == other.year;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = bloque;
                hashCode = (hashCode*397) ^ seccion;
                hashCode = (hashCode*397) ^ (capaBulto != null ? capaBulto.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (tallaCompleta != null ? tallaCompleta.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (color != null ? color.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ semana;
                hashCode = (hashCode*397) ^ year;
                return hashCode;
            }
        }
    }
}