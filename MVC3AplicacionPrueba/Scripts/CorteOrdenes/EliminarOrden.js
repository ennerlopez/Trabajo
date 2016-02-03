
$(document).ready(function () {

  var oTable = $('.tablaAjax').dataTable({
        "bJQueryUI": true,
        "sPaginationType": "full_numbers",
        "fnDrawCallback": function (oSettings) {
           
            Botones();
        }
    });


function Botones() {

    $('.BotonEdit').tooltip('Editar.', { mode: 'tr', width: 100 });
    $('.BotonDelete').tooltip('Eliminar.', { mode: 'tr', width: 100 });
    $('.BotonDetails').tooltip('Mostrar.', { mode: 'tr', width: 100 });
    /*Para Cliente*/
    $('.BotonTallas').tooltip('Agregar Tallas.', { mode: 'tr', width: 100 });
    $('.BotonOperaciones').tooltip('Agregar Operaciones.', { mode: 'tr', width: 110 });
    /*Para Orden*/
    $('.BotonAgregarHojaBulto').tooltip('Agregar Hoja Bulto.', { mode: 'tr', width: 130 });
    $('.BotonGenerarHojaBultos').tooltip('Generar Hoja Bulto.', { mode: 'tr', width: 130 });
    $('.BotonEditarHojaBulto').tooltip('Editar Hoja Bulto.', { mode: 'tr', width: 130 });
}


$(".tablaAjax tbody").delegate(".elimina", "click", function () {

        var row = $(this).closest("tr").get(0);
        var iPos = oTable.fnGetPosition(row);

        var codigoCorte = unescape($(this).closest("tr").find("td").eq(0).text());
      
      

        var n = noty({
            type: 'warning',
            layout: "center",
            modal: true,
            text: '¿Deseas Eliminar la Orden '+ codigoCorte +'?',
            buttons: [

            { addClass: 'btn btn-primary', text: 'Aceptar',
                onClick: function ($noty) {

                    $noty.close();

                    if (iPos != null) {
                    /*==================================================================*/
                       //Borra la fila de la tabla
                      
                        $.ajax({
                            url: '/ordenes/EliminarOrden',
                            type: "POST",
                            data: JSON.stringify({ codigoOrden: codigoCorte}),
                            dataType: "json",
                            contentType: 'application/json; charset=utf-8',
                            success: function (result) {

                                if (result.success) {


                                    /*=====================================================================================*/

                                    oTable.fnDeleteRow(iPos); 
                                    var n = noty({
                                        type: 'success',
                                        layout: "center",
                                        modal: true,
                                        text: 'Orden Eliminada Corretamente',
                                        buttons: [

                                { addClass: 'btn btn-primary', text: 'Aceptar',
                                    onClick: function ($noty) {

                                        $noty.close();
                                        


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




                    /*=======================================================================*/
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


});