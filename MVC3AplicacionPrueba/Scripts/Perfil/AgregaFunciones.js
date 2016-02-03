$(document).ready(function () {

    $("#EnviarFunciones").click(function () {

        var funciones = new Array();
        $("#box2Group #view option").each(function (index, option) {

            funciones[index] = $(this).val();

        });
        /*
        $(funciones).each(function (index) {
        alert(funciones[index]);
        });
        */
        
        var idPerfil = $("#codigoPerfil").text();
        $.ajax({
            url: '/perfilentidad/asignarFunciones',
            type: "POST",
            data: JSON.stringify({ fun:funciones, id: idPerfil }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {
                    alert(result.mensaje);
                }
                else {
                    alert(result.mensaje);
                }


            }
        });

    });







});