namespace MVC3AplicacionPrueba.Reportes.Entidades
{
    public class CuponModel
    {
        public int serie { get; set; }
        public long nroCupon { get; set; }
        public string codigoCorte { get; set; }
        public string tallaCompleta { get; set; }
        public int cantidadCortada { get; set; }
        public string nombreEstilo { get; set; }
        public string nombreOperacion { get; set; }
        public string tiempoCupon { get; set; }
        public decimal valorCupon { get; set; }
        public string nombreDepartamento { get; set; }
        public string nombreGrupo { get; set; }
        public string bulto { get; set; }
        public byte[] qrCode { get; set; }
       
    }
}