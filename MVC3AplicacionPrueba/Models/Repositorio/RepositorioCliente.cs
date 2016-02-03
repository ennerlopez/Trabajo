using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelCliente;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class RepositorioCliente
    {
        private DataContextoDataContext contexto; 
        
        
        public RepositorioCliente(DataContextoDataContext contexto)
        {
            this.contexto = contexto;
        }

        public Cliente getCliente(int id)
        {
            return contexto.Clientes.Single(x => x.codigoCliente == id);
        }

        public List<Cliente> getListaClientes()
        {
            return contexto.Clientes.ToList();
        }

        public void editarCliente(int id,int cuello,int manga,string letra)
        {
            var talla = (from t in contexto.TallasClientes
                         where t.codigoCliente == id && t.tallaCuello == cuello && t.tallaManga == manga
                         select t).FirstOrDefault();
          
           
             if(string.IsNullOrEmpty(letra))
             {
                 talla.tallaLetra = null;
                 int n = talla.tallaCompleta.Length;
                 talla.tallaCompleta = talla.tallaCompleta.Substring(0, (n - 2));
             }else
             {
                 talla.tallaLetra = letra;
                 talla.tallaCompleta = "(" + cuello + "-" + manga + ") " + letra;

             }


            contexto.SubmitChanges();

            
        }


        public List<Diccionario> getListaTallaCuello()
        {
            var listaTalla = contexto.TallaCuellos.ToList();

            List<Diccionario> listaDiccionario = new List<Diccionario>();
         
            foreach (var tallaCuello in listaTalla)
            {
                Diccionario diccionario = new Diccionario();
                diccionario.value = tallaCuello.codigoCuello;
                diccionario.text = tallaCuello.tallaCuello1.ToString();
              
                listaDiccionario.Add(diccionario);
            }
            

            return listaDiccionario;
        }



        public List<Diccionario> getListaTallaManga()
        {
            var listaTalla = contexto.tallaMangas.ToList();

            List<Diccionario> listaDiccionario = new List<Diccionario>();

            foreach (var tallaCuello in listaTalla)
            {
                Diccionario diccionario = new Diccionario();
                diccionario.value = tallaCuello.codigoManga;
                diccionario.text = tallaCuello.tallaManga1.ToString();

                listaDiccionario.Add(diccionario);
            }


            return listaDiccionario;
        }

        public List<Diccionario> getListaTallaLetra()
        {
            var listaTalla = contexto.tallaLetras.ToList();

            List<Diccionario> listaDiccionario = new List<Diccionario>();

            foreach (var tallaCuello in listaTalla)
            {
                Diccionario diccionario = new Diccionario();
                diccionario.value = tallaCuello.codigoLetra;
                diccionario.text = tallaCuello.tallaLetra1;

                listaDiccionario.Add(diccionario);
            }


            return listaDiccionario;
        }

        public List<ModelTallasCliente> getListaTallasCliente(int id)
        {
            var listaTallaCliente = contexto.TallasClientes.Where(x=>x.codigoCliente ==id).ToList();

            List<ModelTallasCliente> listaModelTallasCliente = new List< ModelTallasCliente>();

            foreach (var talla in listaTallaCliente)
            {
                ModelTallasCliente tallasCliente = new ModelTallasCliente();
                tallasCliente.codigo = talla.codigoCliente;
                tallasCliente.tallaCuello = talla.tallaCuello;
                tallasCliente.tallaManga = talla.tallaManga;
                tallasCliente.tallaLetra = talla.tallaLetra;
                tallasCliente.tallaCompleta = talla.tallaCompleta;
                
                listaModelTallasCliente.Add(tallasCliente);

                
            }


            return listaModelTallasCliente;
        }

        public void eliminarTallaDeCliente(int id, int cuello, int manga)
        {
            var talla = (from t in contexto.TallasClientes
                         where t.codigoCliente == id && t.tallaCuello == cuello && t.tallaManga == manga
                         select t).FirstOrDefault();
           contexto.TallasClientes.DeleteOnSubmit(talla);
           contexto.SubmitChanges();


        }

        public void guardarTalla(int idCliente,string cuello, string manga,string letra)
        {
            TallasCliente tallas = new TallasCliente();
            if(string.IsNullOrEmpty(letra))
            {
                tallas.codigoCliente = idCliente;
                tallas.tallaCuello = int.Parse(cuello);
                tallas.tallaManga = int.Parse(manga);
                tallas.tallaCompleta = "(" + cuello + "-" + manga + ")";
               
            }else
            {
                tallas.codigoCliente = idCliente;
                tallas.tallaCuello = int.Parse(cuello);
                tallas.tallaManga = int.Parse(manga);
                tallas.tallaLetra = letra;
                tallas.tallaCompleta = "(" + cuello + "-" + manga + ") " + letra;

            }

            contexto.TallasClientes.InsertOnSubmit(tallas);
            contexto.SubmitChanges();
        }
    }
}