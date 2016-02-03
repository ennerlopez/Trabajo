$(document).ready(function () {

  //  $("button").addClass(".btn-primary");
   
    $("#EnviarOperacionesDepartamento").click(function () {

        var operaciones = new Array();
        $("#box2Group #view option").each(function (index, option) {

            operaciones[index] = $(this).val();

        });
        /*
        $(funciones).each(function (index) {
        alert(funciones[index]);
        });
        */


        var idDepartamento = $("#codigoDepartamento").text();
        $.ajax({
            url: '/departamentos/asignarOperaciones',
            type: "POST",
            data: JSON.stringify({ op: operaciones, id: idDepartamento }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {

                    //  alert(result.mensaje);
                    $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });

                    alert(result.mensaje);

                }
                else {
                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 5000 });


                    alert(result.mensaje);
                }


            }

        });

    });







});