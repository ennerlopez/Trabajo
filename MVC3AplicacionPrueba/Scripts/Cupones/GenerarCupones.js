$(document).ready(function () {

    $('#VerPdfCupones').click(function () {
        // get the url of the data-pdf-url property of the button
        var codigo = $('#codigoOrden').val().toUpperCase();
        var url = '/cupon/report?codigo=' + codigo;

        location.href = url;
        return false;
    });

    $('#eliminarCupones').click(function () {
     
        var n = noty({
            type: 'warning',
            layout: "center",
            modal: true,
            text: '¿Deseas Eliminar Cupones para la orden: ' + $('#codigoOrden').val().toUpperCase() + '?',
            buttons: [
                        {
                            addClass: 'btn btn-primary',
                            text: 'Aceptar',
                            onClick: function ($noty) {


                                $noty.close();
                                eliminarBaseDeDatos();
                            }
                        },
                        {
                            addClass: 'btn btn-danger',
                            text: 'Cancelar',
                            onClick: function ($noty) {

                                $noty.close();

                            }
                        }
                    ]
        });


        return false;
    });


    $("#formularioGeneracionCupones").validationEngine('attach', {
        onValidationComplete: function (form, status) {
            if (status == true) {

                /*=============================================================================================*/


                var n = noty({
                    type: 'warning',
                    layout: "center",
                    modal: true,
                    text: '¿Deseas Guardar Cupones para la orden: ' + $('#codigoOrden').val().toUpperCase() + '?',
                    buttons: [
                        {
                            addClass: 'btn btn-primary',
                            text: 'Aceptar',
                            onClick: function ($noty) {


                                $noty.close();
                                guardarEnBaseDeDatos();
                            }
                        },
                        {
                            addClass: 'btn btn-danger',
                            text: 'Cancelar',
                            onClick: function ($noty) {

                                $noty.close();

                            }
                        }
                    ]
                });


            }
        }
    });

    function guardarEnBaseDeDatos() {
        /*===============================================================================*/

        var codigoOrden = $('#codigoOrden').val().toUpperCase();

        $.ajax({
            url: '/cupon/generarCupones',
            type: "POST",
            data: JSON.stringify({ codigoCorte: codigoOrden }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {

                    var no = noty({
                        type: 'success',
                        layout: "center",
                        modal: true,
                        text: result.mensaje,
                        buttons: [
                            {
                                addClass: 'btn btn-primary',
                                text: 'Aceptar',
                                onClick: function ($noty) {
                                    $noty.close();
                                }
                            }
                        ]
                    });

                    console.log('html: ' + n.options.id);


                } else {
                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);
                }


            }
        });
    }

    /*==================================================================================================*/


    function eliminarBaseDeDatos() {
        /*===============================================================================*/

        var codigoOrden = $('#codigoOrden').val().toUpperCase();

        $.ajax({
            url: '/cupon/eliminarCupones',
            type: "POST",
            data: JSON.stringify({ codigoCorte: codigoOrden }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {

                    var no = noty({
                        type: 'success',
                        layout: "center",
                        modal: true,
                        text: result.mensaje,
                        buttons: [
                                {
                                    addClass: 'btn btn-primary',
                                    text: 'Aceptar',
                                    onClick: function ($noty) {
                                        $noty.close();
                                    }
                                }
                            ]
                    });

                    console.log('html: ' + n.options.id);


                } else {
                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);
                }


            }
        });

    }

});