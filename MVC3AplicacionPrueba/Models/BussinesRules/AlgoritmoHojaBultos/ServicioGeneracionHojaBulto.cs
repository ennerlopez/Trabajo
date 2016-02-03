using System.Collections.Generic;
using MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos.Entidades;

namespace MVC3AplicacionPrueba.Models.BussinesRules.AlgoritmoHojaBultos
{
    public class ServicioGeneracionHojaBulto
    {

        private readonly Recursivo _recursivo;// = new Recursivo();
        private readonly VerificacionMaximoCapas _verificacionMaximoCapas;// = new VerificacionMaximoCapas();
        private readonly DefinirSecciones _definirSecciones; //= new DefinirSecciones();
        private readonly DefinirHojaBultos _definirHojaBultos;// = new DefinirHojaBultos();

        public ServicioGeneracionHojaBulto()
        {
            _recursivo = new Recursivo();
            _verificacionMaximoCapas = new VerificacionMaximoCapas();
            _definirSecciones = new DefinirSecciones();
            _definirHojaBultos = new DefinirHojaBultos();
        }


        public IList<NumeroBultos> getHojaBulto(string codigoCorte, List<TallaCantidad> lista, int maximoCapasPorTalla, int maximoRepeticionesDeCuerpoPermitido)
        {
            var primeraListaAgrupada = _recursivo.agrupar(lista);
            var listaRedefinida = _verificacionMaximoCapas.redefinir(primeraListaAgrupada, maximoCapasPorTalla);
            var listaRecursiva = _recursivo.agrupar(listaRedefinida);
            var listaConSeccionesDefinidas = _definirSecciones.definir(listaRecursiva, maximoRepeticionesDeCuerpoPermitido);
            var listaConHojaDeBultosDefinida = _definirHojaBultos.getHojaBultos(listaConSeccionesDefinidas, codigoCorte);

            return listaConHojaDeBultosDefinida;
        }

        public IList<NumeroBultos> getHojaBulto(string codigoCorte, List<TallaCantidad> lista, int maximoCapasPorTalla, int maximoRepeticionesDeCuerpoPermitido,int maximoSplitPorCapa)
        {
            var primeraListaAgrupada = _recursivo.agrupar(lista,maximoSplitPorCapa);
            var listaRedefinida = _verificacionMaximoCapas.redefinir(primeraListaAgrupada, maximoCapasPorTalla);
            var listaRecursiva = _recursivo.agrupar(listaRedefinida, maximoSplitPorCapa);
            var listaConSeccionesDefinidas = _definirSecciones.definir(listaRecursiva, maximoRepeticionesDeCuerpoPermitido);
            var listaConHojaDeBultosDefinida = _definirHojaBultos.getHojaBultos(listaConSeccionesDefinidas, codigoCorte,maximoSplitPorCapa);

            return listaConHojaDeBultosDefinida;
        }
    }
}