$(document).ready(function () {

    $("#AgregarTallaCliente").click(function () {


        var cliente = $("#CodigoCliente").text();
        var tallasCuello = $("#tallasCuello option:selected").text();
        var tallasManga = $("#tallasManga option:selected").text();
        var tallasLetra = $("#tallasLetra option:selected").text();

        // alert(tallasCuello + tallasManga + tallasLetra);
        $.ajax({
            url: '/cliente/asignarTallas',
            type: "POST",
            data: JSON.stringify({ idCliente: cliente, cuello: tallasCuello, manga: tallasManga, letra: tallasLetra }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {
                    
                    $('.tabla').dataTable().fnAddData([
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


        var cliente = $("#CodigoCliente").text();
        var tallasCuello = $("#tallasCuello option:selected").text();
        var tallasManga = $("#tallasManga option:selected").text();
        var tallasLetra = $("#tallasLetra option:selected").text();


        $.ajax({
            url: '/cliente/editarTallas',
            type: "POST",
            data: JSON.stringify({ idCliente: cliente, cuello: tallasCuello, manga: tallasManga, letra: tallasLetra }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {


                    var contador = 0;

                    var textoCampo1 = "";
                    var textoCampo2 = "";
                    $('.tabla tbody tr').each(function (index) {

                        var actualTallaCuello = $(this).children("td").eq(0).text();
                        var actualTallaManga = $(this).children("td").eq(1).text();

                        if ((tallasCuello == actualTallaCuello) && (tallasManga == actualTallaManga)) {
                            // $(this).children("td").eq(2).text(tallasLetra);
                            var oTable = $('.tabla').dataTable();
                            oTable.fnUpdate(tallasLetra, contador, 2); // Single cell

                        }
                        contador++;
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