using System.Collections.Generic;
using System.Linq;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Dto;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Entidades;
using MVC3AplicacionPrueba.Models.Modelado.ModelHojaBloques.Reglas;
using MVC3AplicacionPrueba.Models.Repositorio;
using NUnit.Framework;

namespace TestProyecto
{
    [TestFixture]
    public class ActualizarValoresTest
    {
        [Test]
        public void verificarValoresRestantes()
        {
            var planificacion = getPlanificacion();

            var actualizarValoresRestantes = new ActualizarValoresRestantes();
            actualizarValoresRestantes.execute(planificacion);

            var bultos = planificacion.bultos;

            var primerElemento = bultos.FirstOrDefault();
            Assert.AreEqual(0,primerElemento.cantidadRestante);
        }

        [Test]
        public void sss()
        {
            var planificacion = getPlanificacion();
            new ActualizarValoresRestantes().execute(planificacion);
            planificar(planificacion);
            var hojaBloqueses = planificacion.bloques;
        }

        [Test]
        public void nuevoTest()
        {

            var listaReglas = new List<IStrategy>()
                {
                 
                    new CantidadParaCompletarBloqueIgualACantidadBulto(),
                    new CantidadACompletarMayorQueCantidadDeBulto(),
                    new CantidadParaCompletarBloqueMenorACantidadDeBulto()
                };

            var planificacion = getPlanificacion();
            var strategyCaller = new StrategyCaller(listaReglas);
            strategyCaller.runRules(planificacion);

            var hojaBloqueses = planificacion.bloques;
        }

        private void planificar(Planificacion planificacion, bool continuar = true)
        {
            var vf = false;
            var vaAContinuar = true;
            if (planificacion.bultos.Count(x => x.cantidadRestante > 0) == 0 || continuar == false)
            {

            }
            else
            {
                var bulto = planificacion.bultos.Where(x => x.cantidadRestante > 0).ToList()[0];

                int bloqueAUtilizar = getBloque(planificacion);
                if (bloqueAUtilizar > 44)
                {
                    vaAContinuar = false;
                }
                else
                {

                    var sum = planificacion.bloques.Where(x => x.bloque == bloqueAUtilizar).Sum(x => x.cantidad);

                    if (sum < planificacion.datos.capacidadXHora)
                    {
                        var cantidadParaCompletarElBloque = planificacion.datos.capacidadXHora - sum;

                        if (cantidadParaCompletarElBloque == bulto.cantidadRestante)
                        {

                            var hojaBloques = new MyHojaBloques()
                            {
                                bloque = bloqueAUtilizar,
                                capaBulto = bulto.capaCorte,
                                cantidad = cantidadParaCompletarElBloque,
                                corte = bulto.codigoCorte,
                                seccion = bulto.numeroSeccion,
                                serie = bulto.serie

                            };
                            bulto.cantidadRestante = 0;
                            planificacion.add(hojaBloques);
                            vf = true;
                        }


                        if (cantidadParaCompletarElBloque > bulto.cantidadRestante && vf == false)
                        {

                            var hojaBloques = new MyHojaBloques()
                                {
                                    bloque = bloqueAUtilizar,
                                    capaBulto = bulto.capaCorte,
                                    cantidad = bulto.cantidadRestante,
                                    corte = bulto.codigoCorte,
                                    seccion = bulto.numeroSeccion,
                                    serie = bulto.serie

                                };
                            bulto.cantidadRestante = 0;
                            planificacion.add(hojaBloques);
                            vf = true;
                        }

                        if (cantidadParaCompletarElBloque < bulto.cantidadRestante && vf == false)
                        {
                            var hojaBloques = new MyHojaBloques()
                                {
                                    bloque = bloqueAUtilizar,
                                    capaBulto = bulto.capaCorte,
                                    cantidad = cantidadParaCompletarElBloque,
                                    corte = bulto.codigoCorte,
                                    seccion = bulto.numeroSeccion,
                                    serie = bulto.serie

                                };
                            bulto.cantidadRestante = bulto.cantidadRestante - cantidadParaCompletarElBloque;
                            planificacion.add(hojaBloques);
                        }

                    }

                }
                planificar(planificacion,vaAContinuar);
            }
        }

        private int getBloque(Planificacion planificacion)
        {
            if (planificacion.bloques == null) return 1;
             var miBloque = planificacion.bloques.LastOrDefault().bloque;

                var sum = planificacion.bloques.Where(x => x.bloque == miBloque).Sum(x => x.cantidad);
               if (sum == planificacion.datos.capacidadXHora)
               {
                   miBloque = miBloque + 1;
               }
            return miBloque;
        }

        private Planificacion getPlanificacion()
        {
            var planificacion = new Planificacion();
            planificacion.datos = new ModelHojaBloques(){capacidadXHora = 30,corte = "FAS-039",semana = 1,year = 2012,color = "rojo"};
            planificacion.bloques = new List<MyHojaBloques>()
                {
                    new MyHojaBloques(){bloque = 1,seccion = 1,capaBulto = "A",cantidad = 30,color = "Verde",corte = "FAS-039",serie = 6236,semana = 1,tallaCompleta = "(150-33) 2/3",year = 2012}
                   // new HojaBloques(){bloque = 1,seccion = 1,capaBulto = "B",cantidad = 24,color = "Verde",corte = "FAS-039",serie = 6237,semana = 1,tallaCompleta = "(150-33) 2/3",year = 2012},


                };
            planificacion.bultos =new List<HojaBultos>()
                {
                    new HojaBultos(){numeroBultos = 1,codigoCorte = "FAS-039",numeroSeccion = 1,capaCorte = "A",tallaCompleta = "(150-33) 2/3",cantidadRestante = 36,serie = 6236},
                    new HojaBultos(){numeroBultos = 1,codigoCorte = "FAS-039",numeroSeccion = 1,capaCorte = "B",tallaCompleta = "(150-33) 2/3",cantidadRestante = 36,serie = 6237},
                    new HojaBultos(){numeroBultos = 1,codigoCorte = "FAS-039",numeroSeccion = 1,capaCorte = "C",tallaCompleta = "(150-33) 2/3",cantidadRestante = 36,serie = 6238},
                    new HojaBultos(){numeroBultos = 1,codigoCorte = "FAS-039",numeroSeccion = 2,capaCorte = "B",tallaCompleta = "(155-35) 4/5",cantidadRestante = 36,serie = 6260},
                    new HojaBultos(){numeroBultos = 1,codigoCorte = "FAS-039",numeroSeccion = 2,capaCorte = "C",tallaCompleta = "(155-35) 4/5",cantidadRestante = 36,serie = 6261},
                    new HojaBultos(){numeroBultos = 1,codigoCorte = "FAS-039",numeroSeccion = 3,capaCorte = "C",tallaCompleta = "(175-33) 2/3",cantidadRestante = 36,serie = 6270},
                    new HojaBultos(){numeroBultos = 1,codigoCorte = "FAS-039",numeroSeccion = 4,capaCorte = "C",tallaCompleta = "(155-33) 2/3",cantidadRestante = 20,serie = 6272},
                    new HojaBultos(){numeroBultos = 1,codigoCorte = "FAS-039",numeroSeccion = 5,capaCorte = "C",tallaCompleta = "(145-33) 2/3",cantidadRestante = 5,serie = 6279},
             
                    new HojaBultos(){numeroBultos = 2,codigoCorte = "FAS-039",numeroSeccion = 1,capaCorte = "A",tallaCompleta = "(155-33) 2/3",cantidadRestante = 36,serie = 6239},
                    new HojaBultos(){numeroBultos = 2,codigoCorte = "FAS-039",numeroSeccion = 1,capaCorte = "B",tallaCompleta = "(155-33) 2/3",cantidadRestante = 36,serie = 6240},
                    new HojaBultos(){numeroBultos = 2,codigoCorte = "FAS-039",numeroSeccion = 1,capaCorte = "C",tallaCompleta = "(155-33) 2/3",cantidadRestante = 36,serie = 6241},
                    new HojaBultos(){numeroBultos = 2,codigoCorte = "FAS-039",numeroSeccion = 2,capaCorte = "B",tallaCompleta = "(160-33) 2/3",cantidadRestante = 36,serie = 6262},
                    new HojaBultos(){numeroBultos = 2,codigoCorte = "FAS-039",numeroSeccion = 2,capaCorte = "C",tallaCompleta = "(160-33) 2/3",cantidadRestante = 36,serie = 6263},
                    new HojaBultos(){numeroBultos = 2,codigoCorte = "FAS-039",numeroSeccion = 3,capaCorte = "C",tallaCompleta = "(180-35) 4/5",cantidadRestante = 36,serie = 6271},
                    new HojaBultos(){numeroBultos = 2,codigoCorte = "FAS-039",numeroSeccion = 4,capaCorte = "C",tallaCompleta = "(160-33) 2/3",cantidadRestante = 20,serie = 6273},
                    new HojaBultos(){numeroBultos = 2,codigoCorte = "FAS-039",numeroSeccion = 5,capaCorte = "C",tallaCompleta = "(145-33) 2/3",cantidadRestante = 5,serie = 6280},
                  
                };


            return planificacion;
        }
        
        
    }
}
