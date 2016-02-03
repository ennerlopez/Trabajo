
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
               {
                   oValidationOptions: { rules: { value: { digits: true} },
                       messages: { value: { digits: "Solo Numeros"} }
                   }
               },
        null,
        null,
        null,
        null,

        {
            oValidationOptions: { rules: { value: { digits: true} },
                messages: { value: { digits: "Solo Numeros"} }
            }
        },
        null
        ]
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


    $("#crear_botonAgregarSeccion").click(function () {

        var serie = $("#crear_serie").val();
        var seccion = $("#crear_SeccionHojaBulto").val();
        var nroBulto = $("#crear_nroBulto").val();
        var capa = $("#crear_capa option:selected").text();
        var talla = $("#crear_talla option:selected").text();
        var cantidad = $("#crear_cantidad").val();

        if (!existeSeccion(seccion, nroBulto, capa, serie)) {

            if (validarIngresoDeSecciones()) {
                oTable.fnAddData([
                    serie,
                    seccion,
                    nroBulto,
                    capa,
                    talla,
                    cantidad,
                    "<a class='elimina'>Eliminar</a>"]);
                porRecargado();
                $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                alert("Agregado Correctamente");


            } else {

                $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
                alert("Por Favor Ingrese los datos correctamente");
            }

        }
        else {
            $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
            alert("Ya se encuentra Ingresado");
        }



    });



    /*con la validacion jquery*/

    $("#formularioCrearHojaBulto").validationEngine('attach', {
        onValidationComplete: function (form, status) {
            if (status == true) {

                /*=============================================================================================*/
                var ModelResumenHojaBultos = new Object();
                ModelResumenHojaBultos.codigoOrden = $('#codigoOrden').val();
                ModelResumenHojaBultos.cliente = $('#cliente').val();

                var detalle = obtenerDetalleOrdenes();




                $.ajax({
                    url: '/HojaBultos/guardarHojaBulto',
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
                                text: 'Hoja Bulto Ingresada Corretamente',
                                buttons: [

                                { addClass: 'btn btn-primary', text: 'Aceptar',
                                    onClick: function ($noty) {

                                        $noty.close();
                                        NoPreguntarRefrescar();
                                        window.location.href = result.redirectToUrl;



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



    /*==========================================================================================================*/

    //Para eliminar de la tabla
    $(".tablaAjax tbody").delegate("a", "click", function () {

        var row = $(this).closest("tr").get(0);
        var iPos = oTable.fnGetPosition(row);


        var seccion = oTable.fnGetData(row, 0);  //$("#crear_SeccionHojaBulto").val();
        var nroBulto = oTable.fnGetData(row, 1); // $("#crear_nroBulto").val();
        var capa = oTable.fnGetData(row, 2); // $("#crear_capa option:selected").text();



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


    function existeSeccion(seccion, nroBulto, capa, serie) {

        var rows = oTable.fnGetNodes();
        var vf = false;

        for (var i = 0; i < rows.length; i++) {

            var serieTabla = $(rows[i]).find("td:eq(0)").text();
            var seccionTabla = $(rows[i]).find("td:eq(1)").text();
            var nroBultoTabla = $(rows[i]).find("td:eq(2)").text();
            var capaTabla = $(rows[i]).find("td:eq(3)").text();

            if ((seccion == seccionTabla) && (nroBulto == nroBultoTabla) && (capa == capaTabla)) {

                vf = true;

            }

            if (serie == serieTabla) {
                vf = true;
            }
        };

        return vf;

    }
    /*==================================================================*/


    function validarIngresoDeSecciones() {

        var vf = true;


        var seccion = $("#crear_SeccionHojaBulto").val();
        seccion = seccion.replace(/\s/g, '');
        var nroBulto = $("#crear_nroBulto").val();
        var capa = $("#crear_capa");
        var talla = $("#crear_talla");
        var cantidad = $("#crear_cantidad").val();
        cantidad = cantidad.replace(/\s/g, '');

        if (!esEntero(seccion) || seccion == 0) {
            vf = false;
        }

        if (!esEntero(nroBulto) || nroBulto == 0) {
            vf = false;
        }

        if (!isNotFirsElementOfCombobox(capa)) {
            vf = false;
        }

        if (!isNotFirsElementOfCombobox(talla)) {
            vf = false;
        }

        if (!esEntero(cantidad) || cantidad == 0) {
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



});

