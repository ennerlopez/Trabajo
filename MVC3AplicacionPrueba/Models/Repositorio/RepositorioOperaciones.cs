using System;
using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelOperaciones;
using MVC3AplicacionPrueba.Models.Util;

namespace MVC3AplicacionPrueba.Models.Repositorio
{
    public class RepositorioOperaciones
    {
        private DataContextoDataContext contexto; 
        
        
        public RepositorioOperaciones(DataContextoDataContext contexto)
        {
            this.contexto = contexto;
        }

        public List<Operacione> getListaOperaciones()
        {
            return contexto.Operaciones.ToList();
        }


        public Operacione getOperacion(int id)
        {
            return contexto.Operaciones.Single(x => x.codigoOperacion == id);

        }

        public ModelOperaciones getModelOperacion(int id)
        {
           var operacion = contexto.Operaciones.Single(x => x.codigoOperacion == id);
            
            var modelOperacion = new ModelOperaciones();
            modelOperacion.nombre = operacion.nombreOperacion;
        
            modelOperacion.listaGrado = getListaGrado();
            modelOperacion.codigoGrado = operacion.codigoGrado;
            modelOperacion.nombreGrado = operacion.Grado.descripcionGrado;
            modelOperacion.duracionSegundos = operacion.duracionEnSegundosOperacion;
            modelOperacion.tiempoManejoBultos = operacion.tiempoManejoBultoOperacion;
            modelOperacion.valorPieza = operacion.valorPiezaOperacion;
            
            return modelOperacion;

        }

        public List<Diccionario> getListaGrado()
        {
            var lista = contexto.Grados.ToList();

            var listaDiccionario  = new List<Diccionario>();
            foreach (var grado in lista)
            {
                Diccionario diccionario = new Diccionario();
                diccionario.value = grado.codigoGrado;
                diccionario.text = grado.descripcionGrado;
                listaDiccionario.Add(diccionario);
            }

            return listaDiccionario;
        }

        public void guardarOperacion(string nombre, int codigoGrado,string duracion ,string manejoBultos,decimal valorPieza)
        {

          

            Operacione operaciones = new Operacione();
            operaciones.nombreOperacion = nombre;
            operaciones.codigoGrado = codigoGrado;
            operaciones.duracionEnSegundosOperacion = getTime(duracion);    
            operaciones.tiempoManejoBultoOperacion = getTime(manejoBultos);
            operaciones.valorPiezaOperacion = valorPieza;
            contexto.Operaciones.InsertOnSubmit(operaciones);
            contexto.SubmitChanges(); 

        }

        private TimeSpan getTime(string time)
        {
            var dur = time.Split(':');
            var hora = Convert.ToInt32(dur[0]);
            var minuto = Convert.ToInt32(dur[1]);
            var segundo = Convert.ToInt32(dur[2]);

            if (hora > 23 || minuto > 59 || segundo > 59)
            {
                throw new Exception("Tiempo Incorrecto");
            }

            return new TimeSpan(hora, minuto, segundo);
        }

        public void UpdateOperacion(int id, string nombre, int codigoGrado, string duracion, string manejoBultos, decimal valorPieza)
        {
           
            Operacione operaciones = getOperacion(id);
            operaciones.nombreOperacion = nombre;
            operaciones.codigoGrado = codigoGrado;
            operaciones.duracionEnSegundosOperacion = getTime(duracion);
            operaciones.tiempoManejoBultoOperacion = getTime(manejoBultos);
            operaciones.valorPiezaOperacion = valorPieza;
            
            contexto.SubmitChanges();

        }

        public void eliminarOperacion(int id)
        {
            Operacione operaciones =getOperacion(id);

            contexto.Operaciones.DeleteOnSubmit(operaciones);
            contexto.SubmitChanges();
        }


    }
}