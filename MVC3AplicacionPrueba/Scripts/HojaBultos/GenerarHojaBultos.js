
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
                total_costs += parseFloat(aaData[i][column]);
                ;

            }

            /*modify the footer row*/
            var nCells = nRow.getElementsByTagName('th');

            nCells[column].innerHTML = 'Total: ' + total_costs;
        }

        /*===============================================*/
    });


    //Para la caja de texto de la tabla
    $("input[type=text]").button().css({
        'font': 'inherit',
        'background': 'white',
        'color': 'black',
        'text-align': 'left',
        'outline': 'none',
        'cursor': 'text'
    });


    $("#generar_BotonGeneracion").click(function () {
        /*============================================================*/
        oTable.fnClearTable();

        if (validarIngresoDeSecciones()) {


            llenarTabla();
            porRecargado();
            $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
            alert("Generado Correctamente");


        } else {

            $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
            alert("Por Favor Ingrese los datos correctamente");
        }



        /*============================================================*/
    });



    /*con la validacion jquery*/

    $("#formularioGenerarHojaBulto").validationEngine('attach', {
        onValidationComplete: function (form, status) {
            if (status == true) {

                /*=============================================================================================*/

                var n = noty({
                    type: 'warning',
                    layout: "center",
                    modal: true,
                    text: '¿Deseas Guardar la Hoja Bulto para la orden: ' + $('#codigoOrden').val() + '?',
                    buttons: [

                        { addClass: 'btn btn-primary', text: 'Aceptar',
                            onClick: function ($noty) {


                                $noty.close();
                                guardarEnBaseDeDatos();
                            }
                        },
                        { addClass: 'btn btn-danger', text: 'Cancelar', onClick: function ($noty) {

                            $noty.close();

                        }
                        }
                ]
                });




            }
        }
    });



    /*==========================================================================================================*/

    //Para eliminar de la tabla
    $(".tablaAjax tbody").delegate("a", "click", function () {

        var row = $(this).closest("tr").get(0);
        var iPos = oTable.fnGetPosition(row);


        var seccion = $("#crear_SeccionHojaBulto").val();
        var nroBulto = $("#crear_nroBulto").val();
        var capa = $("#crear_capa option:selected").text();



        var n = noty({
            type: 'warning',
            layout: "center",
            modal: true,
            text: '¿Deseas Eliminar el Registro Sección:' + seccion + ', Nro Bulto:' + nroBulto + ', Capa:' + capa + '?',
            buttons: [

            { addClass: 'btn btn-primary', text: 'Aceptar',
                onClick: function ($noty) {

                    $noty.close();

                    if (iPos != null) {

                        oTable.fnDeleteRow(iPos); //Borra la fila de la tabla
                        $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                        alert("Eliminado Correctamente");
                        porRecargado();

                    }

                }
            },
            { addClass: 'btn btn-danger', text: 'Cancelar', onClick: function ($noty) {

                $noty.close();

            }
            }
      ]
        });
        console.log('html: ' + n.options.id);

    });

    /*====================================================preguntar si desea salir de ventana*/
    function porRecargado() {

        if (oTable.fnGetData().length > 0) {
            $("#listaCliente").prop("disabled", true);
            PreguntarRefrescar();
        }
        else {
            $("#listaCliente").removeAttr('disabled');
            NoPreguntarRefrescar();
        }
    }

    function PreguntarRefrescar() {
        $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
        window.onbeforeunload = function () { return "Asegurese De Guardar Todos Los Cambios!!"; };
    }

    function NoPreguntarRefrescar() {
        window.onbeforeunload = null;
    }






    /*=========================================================================================================*/

    function llenarTabla() {

        var _serieComienza = $("#serieComienza").val();
        var id = $("#codigoOrden").val();

        var _capasPorTalla = $("#generar_CapasPorTalla").val();
        var _repeticionesPorCuerpo = $("#generar_RepeticionesDeCuerpo").val();
        var _splitCuerpo = $("#generar_SplitCuerpo").val();

        $.ajax({
            url: '/hojaBultos/obtenerGeneracionDeHojaBulto',
            type: "POST",
            data: JSON.stringify({ serieComienza: _serieComienza, idOrden: id, splitCuerpo: _splitCuerpo, capasPorTalla: _capasPorTalla, repeticionesDeCuerpo: _repeticionesPorCuerpo }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {

                    $.each(result.listaHojaBultos, function (i, object) {

                        oTable.fnAddData([
                            object.serie,
                            object.nroSeccion,
                            object.numeroBulto,
                            object.capaCorte,
                            object.tallaCompleta,
                            object.cantidad]);

                    });

                } else {
                    alert(result.mensaje);
                }


            }
        });


    }

    /*==================================================================*/


    function validarIngresoDeSecciones() {

        var vf = true;

        var serieComienza = $("#serieComienza").val();
        var capasPorTalla = $("#generar_CapasPorTalla").val();
        var repeticionesDeCuerpo = $("#generar_RepeticionesDeCuerpo").val();
        var splitCuerpo = $("#generar_SplitCuerpo").val();
      
        if (!esEntero(serieComienza) || serieComienza=="") {
            vf = false;
       
        }
        if (!esEntero(capasPorTalla) || capasPorTalla == 0 || serieComienza == "") {
            vf = false;
        }

        if (!esEntero(repeticionesDeCuerpo) || repeticionesDeCuerpo == 0 || serieComienza == "") {
            vf = false;
        }

        if (!esEntero(splitCuerpo) || splitCuerpo == 0 || serieComienza == "") {
            vf = false;
        }



        return vf;

    }


    function isNotFirsElementOfCombobox(combobox) {

        var vf = true;
        var selectedIndex = combobox.get(0).selectedIndex;
        if (selectedIndex == 0) {
            vf = false;
        }

        return vf;
    }


    function esEntero(valor) {
        if (!isNaN(valor)) {
            for (var i = 0; i < valor.length; i++) {
                if (valor.charCodeAt(i) < 48 || valor.charCodeAt(i) > 57)
                    return false;
            }
        } else {
            return false;
        }

        return true;
    }



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
            var cantidad = cantidaHistorico;

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


    function guardarEnBaseDeDatos() {
        /*===============================================================================*/
        var ModelResumenHojaBultos = new Object();
        ModelResumenHojaBultos.codigoOrden = $('#codigoOrden').val();
        ModelResumenHojaBultos.cliente = $('#cliente').val();

        var detalle = obtenerDetalleOrdenes();




        $.ajax({
            url: '/HojaBultos/guardarHojaBultoGenerada',
            type: "POST",
            data: JSON.stringify({ resumenOrden: ModelResumenHojaBultos, detalleOrden: detalle }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {

                    var no = noty({
                        type: 'success',
                        layout: "center",
                        modal: true,
                        text: 'Hoja De Bultos guardada Correctamente',
                        buttons: [

                                    { addClass: 'btn btn-primary', text: 'Aceptar', onClick: function ($noty) {

                                        $noty.close();
                                        NoPreguntarRefrescar();
                                        window.location.href = result.redirectToUrl;

                                    }
                                    }
                                        ]
                    });

                    console.log('html: ' + n.options.id);


                }
                else {
                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);
                }


            }



        });
        /*==================================================================================================*/

    }

});

