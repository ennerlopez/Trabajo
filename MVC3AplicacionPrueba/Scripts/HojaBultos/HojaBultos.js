
$(document).ready(function () {

    $('#fechaCortado').datepicker();

    $('#cantida').attr('disabled', true);
    var oTable = $('.tablaAjax').dataTable({
        "bJQueryUI": true,
        "sPaginationType": "full_numbers",
        /*==========================================*/
        "fnFooterCallback": function (nRow, aaData, iStart, iEnd, aiDisplay) {


            var total_costs = 0;
            var column = 5;
            /*Calculate the total for all rows, even outside this page*/
            for (var i = 0; i < aaData.length; i++) {
                /*Have to strip out extra characters so parsefloat and parseInt work right*/
                total_costs += parseFloat(aaData[i][column]); ;

            }

            /*modify the footer row*/
            var nCells = nRow.getElementsByTagName('th');

            nCells[column].innerHTML = 'Total: ' + total_costs;
        }

        /*===============================================*/
    }).makeEditable({
        sUpdateURL: function (value, settings) {


            return (value); //Simulation of server-side response using a callback function
        },

        "aoColumns": [
        null,
        null,
        null,
        null,
        null,
             null,
        {
            oValidationOptions: { rules: { value: { number: true} },
                messages: { value: { number: "Solo Numeros"} }
            }
        }
        ]
    });  //--------------------------------------->Variable Global




    //Para la caja de texto de la tabla
    $("input[type=text]").button().css({
        'font': 'inherit',
        'background': 'white',
        'color': 'black',
        'text-align': 'left',
        'outline': 'none',
        'cursor': 'text'
    });

    /*con la validacion jquery*/

    $("#formularioConfirmarHojaBulto").validationEngine('attach', {
        onValidationComplete: function (form, status) {
            if (status == true) {

                /*=============================================================================================*/
                var ModelResumenHojaBultos = new Object();
                ModelResumenHojaBultos.codigoOrden = $('#codigoOrden').val();
                ModelResumenHojaBultos.cliente = $('#cliente').val();
                ModelResumenHojaBultos.fechaCortado = $('#fechaCortado').val();

                var detalle = obtenerDetalleOrdenes();




                $.ajax({
                    url: '/HojaBultos/confirmarOden',
                    type: "POST",
                    data: JSON.stringify({ resumenOrden: ModelResumenHojaBultos, detalleOrden: detalle }),
                    dataType: "json",
                    contentType: 'application/json; charset=utf-8',
                    success: function (result) {

                        if (result.success) {


                            /*=====================================================================================*/


                            var n = noty({
                                type: 'success',
                                layout: "center",
                                modal: true,
                                text: 'Orden Ingresada Corretamente',
                                buttons: [

                                { addClass: 'btn btn-primary', text: 'Aceptar',
                                    onClick: function ($noty) {

                                        $noty.close();

                                        location.reload();



                                    }
                                }
                        ]
                            });
                            console.log('html: ' + n.options.id);











                            /*===============================================================================*/
                        }
                        else {
                            $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                            alert(result.mensaje);
                        }


                    }

                });
                /*==================================================================================================*/








            }
        }
    });



    /*===========================================================================================================*/

    function obtenerDetalleOrdenes() {

        var lista = new Array();


        var rows = oTable.fnGetNodes();

        for (var i = 0; i < rows.length; i++) {

            var detalle = new Object();

            var serie = parseInt($(rows[i]).find("td:eq(0)").text());
            var seccion = parseInt($(rows[i]).find("td:eq(1)").text());
            var bulto = parseInt($(rows[i]).find("td:eq(2)").text());
            var capa = $(rows[i]).find("td:eq(3)").text();
            var talla = $(rows[i]).find("td:eq(4)").text();
            var cantidaHistorico = $(rows[i]).find("td:eq(5)").text();
            var cantidad = $(rows[i]).find("td:eq(6)").text();
      
            detalle.serie = serie;
            detalle.nroSeccion = seccion;
            detalle.numeroBulto = bulto;
            detalle.capaCorte = capa;
            detalle.tallaCompleta = talla.trim();
            detalle.cantidaHistorico = cantidaHistorico;
            detalle.cantidad = cantidad;

            lista[i] = detalle;
        }
        return lista;
    }



});

