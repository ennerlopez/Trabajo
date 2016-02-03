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
             
                    /*====================================================================*/
                } else {
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
            contentType: 'application/json; charset=utf-8'

        });

    });







});