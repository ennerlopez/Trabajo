
$(document).ready(function () {



    $('#cantida').attr('disabled', true);
    var oTable = $('.tablaAjax').dataTable({
        "bJQueryUI": true,
        "sPaginationType": "full_numbers",
        /*==========================================*/
        "fnFooterCallback": function (nRow, aaData, iStart, iEnd, aiDisplay) {

            var total_costs = 0;

            /*Calculate the total for all rows, even outside this page*/
            for (var i = 0; i < aaData.length; i++) {
                /*Have to strip out extra characters so parsefloat and parseInt work right*/
                total_costs += parseFloat(aaData[i][3]);
            }

            var pageTotal_costs = 0;

            $("#cantida").val(total_costs);

            /*calculate totals for this page*/
            for (var i = iStart; i < iEnd; i++) {

                pageTotal_costs += parseFloat(aaData[aiDisplay[i]][3].replace('$', '').replace(',', ''));
            }

            /*modify the footer row*/
            var nCells = nRow.getElementsByTagName('th');

            nCells[3].innerHTML = 'Total: ' + total_costs;
        }

        /*===============================================*/
    }); //--------------------------------------->Variable Global

    /*Color Tabla Seleccionada*/
    $(".tablaAjax tbody tr").each(function () {
        $(this).mouseover(function () {
            $(this).addClass("ui-state-hover");
        }).mouseout(function () {
            $(this).removeClass("ui-state-hover");
        });
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

    /*con la validacion jquery*/

    $("#formularioOrdenes").validationEngine('attach', {
        onValidationComplete: function (form, status) {
            if (status == true) {

                /*=============================================================================================*/
                var ModelResumenOrden = new Object();
                ModelResumenOrden.codigo = $('#codigo').val();
                ModelResumenOrden.codigoUsuario = parseInt($('#CookieUsuario').val());
                ModelResumenOrden.proyecto = parseInt($('#proyecto').val());
                ModelResumenOrden.fecha = $('#fecha').val();
                ModelResumenOrden.codigoCliente = parseInt($("#listaCliente option:selected").val());
                ModelResumenOrden.custPoCorte = $("#custPoCorte").val();
                ModelResumenOrden.cantidaTotal = parseInt($("#cantida").val());
                ModelResumenOrden.codigoEstilo = $("#listaEstilos option:selected").val();
                ModelResumenOrden.tela = $("#tela").val();
                ModelResumenOrden.pinado = $("#pinado").is(':checked');
                ModelResumenOrden.comentario = $("#comentario").val();
                ModelResumenOrden.consumoTela = $("#consumoTela").val();

                var detalle = null;
                if (oTable.fnGetData().length == 0) {

                } else {
                    detalle = obtenerDetalleOrdenes(ModelResumenOrden.codigo);
                }




                $.ajax({
                    url: '/ordenes/GuardarOrden',
                    type: "POST",
                    data: JSON.stringify({ resumenOrden: ModelResumenOrden, detalleOrden: detalle }),
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
                                        //NoPreguntarRefrescar();
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




    /*===================================================fin validacion jquery*/















    /*Agrega Las Tallas a la tabla no a la base de datos*/
    $("#AgregarTallaCliente").click(function () {


        var talla = $("#TallaDeClienteOrden option:selected").text();
        var cuello = talla.substring(1, 4);
        var manga = talla.substring(5, 7);
        var letra = talla.substring(9, talla.length);
        var cantidad = $("#cantidaDetalleOrden").val();
        var indiceTalla = $("#TallaDeClienteOrden").get(0).selectedIndex;



        if (!esEntero(cantidad) || indiceTalla == 0) {
            $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
            alert("Por Ingrese Correctamente la talla.");
        }
        else {
            if (cantidad != "") {
                var existe = existeTalla(cuello, manga, letra);

                if (existe == false) {
                    oTable.fnAddData([
                        cuello,
                        manga,
                        letra,
                        cantidad,
                        "<a class='elimina'>Eliminar</a>"]);

                    $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                    alert("Agregado Correctamente");
                   // PreguntarRefrescar();
                    //obtenerCantidadTotal();
                    //$("#cantida").val(cantidadTotal);

                } else {
                    $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
                    alert("La talla Se Encuentra Ingresada");
                }
            }
            else {
                $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
                alert("Ingrese la cantidad");
            }
        }
    });
    /*=============================================================================*/

    //Para eliminar de la tabla
    $(".tablaAjax tbody").delegate("a", "click", function () {

        var row = $(this).closest("tr").get(0);
        var iPos = oTable.fnGetPosition(row);


        var cliente = $("#CodigoCliente").text();
        var tallaCuello = $(this).closest("tr").find("td").eq(0).html();
        var tallaManga = $(this).closest("tr").find("td").eq(1).html();
        var tallaLetra = $(this).closest("tr").find("td").eq(2).html();
        var cantida = $(this).closest("tr").find("td").eq(3).html();


        var n = noty({
            type: 'warning',
            layout: "center",
            modal: true,
            text: '¿Deseas Eliminar la Talla (' + tallaCuello + '-' + tallaManga + ')' + tallaLetra + '?',
            buttons: [

            { addClass: 'btn btn-primary', text: 'Aceptar',
                onClick: function ($noty) {

                    $noty.close();

                    if (iPos != null) {

                        oTable.fnDeleteRow(iPos); //Borra la fila de la tabla
                        $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                        alert("Eliminado Correctamente");
                        //NoPreguntarRefrescar();

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
    /*=============================================================================*/
   /* $("#GuardarOrden").click(function () {






    });
    */

    $("#EditarTallaCliente").click(function () {



        var talla = $("#TallaDeClienteOrden option:selected").text();
        var cuello = talla.substring(1, 4);
        var manga = talla.substring(5, 7);
        var letra = talla.substring(9, talla.length);
        var cantidad = $("#cantidaDetalleOrden").val();




        var fila = getFilaTalla(cuello, manga, letra);

        if (fila != null) {

            if (fila == -1) {

                $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
                alert("La talla: " + talla + " no se encuentra ingresada.");
            } else {

                oTable.fnUpdate(cantidad, fila, 3); // Single cell
                $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                alert("Actualizado Correctamente");


            }
        }
        else {
            $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
            alert("La talla: " + talla + " no se encuentra ingresada.");
        }

    });
    /*=================================================================================================*/

    //Verfica si la talla ya se encuenta en la tabla
    function existeTalla(cuello, manga, letra) {

        var vf = false;
        $('.tablaAjax tbody tr').each(function (index) {

            var TallaCuelloTabla = $(this).children("td").eq(0).text();
            var TallaMangaTabla = $(this).children("td").eq(1).text();
            var TallaLetraTabla = $(this).children("td").eq(2).text();

            if ((cuello == TallaCuelloTabla) && (manga == TallaMangaTabla) && (letra == TallaLetraTabla)) {

                vf = true;

            }

        });

        return vf;

    }

    //Obtener la fila que se busca atraves de el cuello la manga y la letra
    function getFilaTalla(cuello, manga, letra) {

        //alert(cuello + "-" + manga + "-" + letra);
        var fila = -1;
        $('.tablaAjax tbody tr').each(function (index) {

            var TallaCuelloTabla = $(this).children("td").eq(0).text();
            var TallaMangaTabla = $(this).children("td").eq(1).text();
            var TallaLetraTabla = $(this).children("td").eq(2).text();




            if ((cuello == TallaCuelloTabla) && (manga == TallaMangaTabla) && (letra == TallaLetraTabla)) {
                var row = $(this).get(0);
                fila = oTable.fnGetPosition(row);

            }

        });

        return fila;

    }
    /*===================================================================================================*/


    $("#listaCliente").change(function () {
        var idCliente = $(this).val();
        $(".paraRemover").remove();
        llenarTalla(idCliente);
    })
        .change();

    /*===========================================================================================================*/



    function llenarTalla(idCliente) {



        $.ajax({
            url: '/ordenes/obtenerTallasDeCliente',
            type: "POST",
            data: JSON.stringify({ id: idCliente }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {

                    $.each(result.listaTallasCliente, function (i, object) {

                        $("#TallaDeClienteOrden").append('<option class="paraRemover" value="' + object.value + '">' + object.text + '</option>');

                    });

                }
                else {
                    alert(result.mensaje);
                }


            }

        });


    }




    function obtenerDetalleOrdenes(codigoOrden) {

        var lista = new Array();

      
        var rows = oTable.fnGetNodes();

        for (var i = 0; i < rows.length; i++) {
            var valor = $(rows[i]);


            var ModelDetalleOrden = new Object();

            var cuello = parseInt($(valor).children("td").eq(0).text());
            var manga = parseInt($(valor).children("td").eq(1).text());
            var letra = $(valor).children("td").eq(2).text();
            var cantidad = $(valor).children("td").eq(3).text();

            ModelDetalleOrden.codigoOrden = codigoOrden;
            ModelDetalleOrden.tallaCuello = cuello;
            ModelDetalleOrden.tallaManga = manga;
            ModelDetalleOrden.tallaLetra = letra;
            ModelDetalleOrden.cantidad = cantidad;

            lista[index] = ModelDetalleOrden;




        }
















        return lista;
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










});



