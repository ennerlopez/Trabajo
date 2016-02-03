$(document).ready(function () {



    /*Crear Tabla*/
    var oTable = $('.tablaAjax').dataTable({
        "bJQueryUI": true,
        "sPaginationType": "full_numbers"
    }); //--------------------------------------->Variable Global

    /*Color Tabla Seleccionada*/
    $(".tablaAjax tbody tr").each(function () {
        $(this).mouseover(function () {
            $(this).addClass("ui-state-hover");
        }).mouseout(function () {
            $(this).removeClass("ui-state-hover");
        });
    });


    $("input[type=text]").button().css({
        'font': 'inherit',
        'background': 'white',
        'color': 'black',
        'text-align': 'left',
        'outline': 'none',
        'cursor': 'text'
    });














    $("#AgregarTallaCliente").click(function () {


        var cliente = $("#CodigoCliente").text();
        var tallasCuello = $("#tallasCuello option:selected").text();
        var tallasManga = $("#tallasManga option:selected").text();
        var tallasLetra = "";
        var indice = $("#tallasLetra option:selected").index();
       
       
        if (indice != 0) {
            tallasLetra = $("#tallasLetra option:selected").text();
        }

        // alert(tallasCuello + tallasManga + tallasLetra);
        $.ajax({
            url: '/cliente/asignarTallas',
            type: "POST",
            data: JSON.stringify({ idCliente: cliente, cuello: tallasCuello, manga: tallasManga, letra: tallasLetra }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {

                    oTable.fnAddData([
                    tallasCuello,
                    tallasManga,
                    tallasLetra,
                    "<a class='elimina'>Eliminar</a>"]);

                    $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);

                    /*====================================================================*/
                } else {
                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);
                }


            }
        });

    });


    $("#EditarTallaCliente").click(function () {


       /* var cliente = $("#CodigoCliente").text();
        var tallasCuello = $("#tallasCuello option:selected").text();
        var tallasManga = $("#tallasManga option:selected").text();
        var tallasLetra = $("#tallasLetra option:selected").text();
        */


        var cliente = $("#CodigoCliente").text();
        var tallasCuello = $("#tallasCuello option:selected").text();
        var tallasManga = $("#tallasManga option:selected").text();
        var tallasLetra = "";
        var indice = $("#tallasLetra option:selected").index();


        if (indice != 0) {
            tallasLetra = $("#tallasLetra option:selected").text();
        }


        $.ajax({
            url: '/cliente/editarTallas',
            type: "POST",
            data: JSON.stringify({ idCliente: cliente, cuello: tallasCuello, manga: tallasManga, letra: tallasLetra }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {

                    $('.tablaAjax tbody tr').each(function (index) {

                        var actualTallaCuello = $(this).children("td").eq(0).text();
                        var actualTallaManga = $(this).children("td").eq(1).text();

                        if ((tallasCuello == actualTallaCuello) && (tallasManga == actualTallaManga)) {

                            var row = $(this).get(0);
                            // alert(row);
                            var iPos = oTable.fnGetPosition(row);

                            oTable.fnUpdate(tallasLetra, iPos, 2); // Single cell

                        }

                    });

                    /*====================================================================*/

                    $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);
                } else {
                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);

                }


            }

        });

    });

    $(".tablaAjax tbody").delegate("a", "click", function () {
        //alert("dd");
        var row = $(this).closest("tr").get(0);
        // alert(row);
        var iPos = oTable.fnGetPosition(row);
        /*======================================*/
        var cliente = $("#CodigoCliente").text();
        var tallaCuello = $(this).closest("tr").find("td").eq(0).html();
        var tallaManga = $(this).closest("tr").find("td").eq(1).html();
        var tallaLetra = $(this).closest("tr").find("td").eq(2).html();
        // alert(tallaCuello + "==" + tallaManga);

        var n = noty({
            type: 'warning',
            layout: "center",
            modal: true,
            text: '¿Deseas Eliminar la Talla (' + tallaCuello + '-' + tallaManga + ')' + tallaLetra + '?',
            buttons: [

            { addClass: 'btn btn-primary', text: 'Aceptar',
                onClick: function ($noty) {
                    $noty.close();
                    /*==========================================================*/



                    $.ajax({
                        url: '/cliente/eliminarTallas',
                        type: "POST",
                        data: JSON.stringify({ idCliente: cliente, cuello: tallaCuello, manga: tallaManga }),
                        dataType: "json",
                        contentType: 'application/json; charset=utf-8',
                        success: function (result) {

                            if (result.success) {


                                if (iPos != null) {
                                    oTable.fnDeleteRow(iPos); //delete row
                                }

                                $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                                alert(result.mensaje);
                            }
                            else {
                                $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                                alert(result.mensaje);

                            }


                        }

                    });






                    /*==========================================================*/
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






    /*
    $('a.elimina').click(function () {
    alert("dsds");
    var cliente = $("#CodigoCliente").text();
    var tallaCuello = $(this).parents("tr").find("td").eq(0).html();
    var tallaManga = $(this).parents("tr").find("td").eq(1).html();

    var respuesta = confirm("Desea eliminar la talla: " + tallaCuello + "-" + tallaManga);
    if (respuesta) {
    /*=========================================================
    $.ajax({
    url: '/cliente/eliminarTallas',
    type: "POST",
    data: JSON.stringify({ idCliente: cliente, cuello: tallaCuello, manga: tallaManga }),
    dataType: "json",
    contentType: 'application/json; charset=utf-8',
    success: function (result) {

    if (result.success) {


    $(this).parents("tr").fadeOut("normal", function () { $(this).remove(); });

    $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
    alert(result.mensaje);
    }
    else {
    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
    alert(result.mensaje);

    }


    }

    });


  
           






           
    /*
    $(this).parents("tr").fadeOut("normal", function () {
    $(this).remove();
    /*alert("Talla: " + tallaCuello + "-" + tallaManga + " eliminado");
    /*
    aqui puedes enviar un conjunto de datos por ajax
    $.post("eliminar.php", {ide_usu: id})
                
    });
            

    }
    });*/






});