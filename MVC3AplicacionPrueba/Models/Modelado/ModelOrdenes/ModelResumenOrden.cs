namespace MVC3AplicacionPrueba.Models.Modelado.ModelOrdenes
{
    public class ModelResumenOrden
    {
        public string codigo { get; set; }
        public int codigoUsuario { get; set; }
        public int proyecto { get; set; }
        public string fecha { get; set; }
        public int codigoCliente { get; set; }
        public string custPoCorte { get; set; }
        public int cantidaTotal { get; set; }
        public int codigoEstilo { get; set; }
        public string tela { get; set; }
        public bool pinado { get; set; }
        public decimal? consumoTela { get; set; }
        public string fechaCortado { get; set; }
        public string comentario { get; set; }
    }

    public class ModelDetalleOrden
    {
        public string codigoOrden { get; set; }
        public int tallaCuello { get; set; }
        public int tallaManga { get; set; }
        public string tallaLetra { get; set; }
        public int cantidad { get; set; }
    }
}