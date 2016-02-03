using System;
using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelEstilo;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class RepositorioEstilo
    {
        private DataContextoDataContext contexto; 
        
        
        public RepositorioEstilo(DataContextoDataContext contexto)
        {
            this.contexto = contexto;
        }

        public ModelPreguntaCrearEstilo getModelPreguntaCrearEstilo()
        {
             var pregunta = new ModelPreguntaCrearEstilo();
            var estilos = contexto.Estilos;
          pregunta.estilos = estilos.Select(estilo => new Diccionario() {value = estilo.codigoEstilo,text = estilo.nombreEstilo}).ToList();
            return pregunta;
        }

       public Estilo obtenerEstilo(int id)
       {
           return contexto.Estilos.Single(x => x.codigoEstilo == id);
       }

        public List<Estilo> listaEstilos()
        {
            return contexto.Estilos.ToList();
        }


        public List<Diccionario> listarTodasLasOperaciones()
        {
            var listaOperaciones = contexto.Operaciones.ToList();
           var listaDiccionario = new List<Diccionario>();


            foreach(var operacion in listaOperaciones)
            {
                var diccionario = new Diccionario();
                diccionario.value = operacion.codigoOperacion;
                diccionario.text = operacion.nombreOperacion;

                listaDiccionario.Add(diccionario);

            }
            return listaDiccionario;
        }


        public List<Diccionario> listarOperacionesDeEstilo(int id)
        {
            var listaOperaciones = contexto.VistaRelacionEstiloOperaciones.Where(x=>x.codigoEstilo ==id).ToList();
            var listaDiccionario = new List<Diccionario>();


            foreach (var operacion in listaOperaciones)
            {
                var diccionario = new Diccionario();
                diccionario.value = operacion.codigoOperacion;
                diccionario.text = operacion.nombreOperacion;

                listaDiccionario.Add(diccionario);

            }
            return listaDiccionario;
        }

        public void eliminarEstilo (int id)
       {
           Estilo estilo = obtenerEstilo(id);
           contexto.Estilos.DeleteOnSubmit(estilo);
           contexto.SubmitChanges();
           
       }

        public void actualizarEstilo(int id, Estilo estilo)
        {
            var estiloAActualizar = obtenerEstilo(id);
            estiloAActualizar.nombreEstilo = estilo.nombreEstilo;
            estiloAActualizar.comentario = estilo.comentario;
            contexto.SubmitChanges();
        }

        public  void agregarEstilo(ModelPreguntaCrearEstilo pregunta)
        {
            var vf = true;
            var estilo = new Estilo {nombreEstilo = pregunta.estilo, comentario = pregunta.comentario};

            try
            {
                contexto.Estilos.InsertOnSubmit(estilo);
                contexto.SubmitChanges();

             
            }
            catch (Exception)
            {
                vf = false;

            }
            if (vf == false) throw new Exception("Ocurrio un error al guardar estilo");

            try
            {
                if (pregunta.estiloSeleccionado > 0)
                {
                    var estiloOperaciones = contexto.EstiloOperaciones.Where(x => x.codigoEstilo == pregunta.estiloSeleccionado);
                    var lista = new List<EstiloOperacione>();
                    foreach (var estiloOperacione in estiloOperaciones)
                    {

                        var nuevo = new EstiloOperacione();
                        nuevo.codigoEstilo = estilo.codigoEstilo;
                        nuevo.codigoOperacion = estiloOperacione.codigoOperacion;
                        lista.Add(nuevo);
                    }

                    contexto.EstiloOperaciones.InsertAllOnSubmit(lista);
                    contexto.SubmitChanges();
                }


            }
            catch (Exception)
            {
                
                throw  new Exception("Ocurrio un problema al copiar estilo");
            }
            
        }


        public void borrarOperacionesActualesDeEstilo(int id)
        {
            var operacionesActuales = contexto.EstiloOperaciones.Where(x => x.codigoEstilo == id).ToList(); ;
            contexto.EstiloOperaciones.DeleteAllOnSubmit(operacionesActuales);
            contexto.SubmitChanges();
        }

        public void agregarNuevasOperacionesAEstilo(int[] op, int id)
        {
            var lista = new List<EstiloOperacione>();
            foreach (var codigoOperacion in op)
            {
                var estiloOperacione = new EstiloOperacione();
                estiloOperacione.codigoEstilo = id;
                estiloOperacione.codigoOperacion = codigoOperacion;
                lista.Add(estiloOperacione);
            }

            contexto.EstiloOperaciones.InsertAllOnSubmit(lista);
            contexto.SubmitChanges();

        }

        public void guardarOperacionesAEstilo(int[] op, int id)
        {
            var operacionesActuales = contexto.EstiloOperaciones.Where(x => x.codigoEstilo == id).ToList(); ;
            
            var lista = new List<EstiloOperacione>();
            foreach (var codigoOperacion in op)
            {
                var estiloOperacione = new EstiloOperacione();
                estiloOperacione.codigoEstilo = id;
                estiloOperacione.codigoOperacion = codigoOperacion;
                
                lista.Add(estiloOperacione);
                
            }

            contexto.EstiloOperaciones.DeleteAllOnSubmit(operacionesActuales);
            contexto.EstiloOperaciones.InsertAllOnSubmit(lista); 

            contexto.SubmitChanges();
            
        }
    }
}