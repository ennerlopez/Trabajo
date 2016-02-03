using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Modelado.ModelOrdenes
{
    public class ModelOrden
    {
        public string codigo { get; set; }
        public string proyecto { get; set; }
        public string fechaCorte { get; set; }
        public int codigoCliente { get; set; }
        public string nombreCliente { get; set; }
        public string custPoCorte { get; set; }
        public int cantida { get; set; }
        public int codigoEstilo { get; set; }
        public string tela { get; set; }
        public bool pinado { get; set; }
        public decimal? consumoTela { get; set; }
        public string fechaCortado { get; set; }
        public bool tieneHojaBulto { get; set; }
        public string descripcionTieneHojaBulto { get; set; }
        public string comentario { get; set; }
        public List<Diccionario> listaCliente { get; set; }
        public List<Diccionario> listaEstilos { get; set; }
        public List<ModelDetalleOrden> listaDetalleOrden { get; set; } 
    }
}