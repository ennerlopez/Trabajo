$(document).ready(function () {

    var oTable = $('.tabla').dataTable({
        "bJQueryUI": true,
        "sPaginationType": "full_numbers",
        "fnDrawCallback": function (oSettings) {
           
            Botones();
        }
    });




    /*
    $(".tabla tbody").delegate("a", "click", function () {
    alert("dd");

    var row = $(this).closest("tr").get(0);
    alert(row);
    var iPos = oTable.fnGetPosition(row);
    if (iPos != null) {
    oTable.fnDeleteRow(iPos); //delete row
    }
    });
    */

    //  $(".tabla tbody tr")
    /*   $(oTable).find("tr").each(function () {
    $(this).mouseover(function () {
    $(this).addClass("ui-state-hover");
    }).mouseout(function () {
    $(this).removeClass("ui-state-hover");
    });
    });
    */
});

function Botones() {

    $('.BotonEdit').tooltip('Editar.', { mode: 'tr', width: 100 });
    $('.BotonDelete').tooltip('Eliminar.', { mode: 'tr', width: 100 });
    $('.BotonDetails').tooltip('Mostrar.', { mode: 'tr', width: 100 });
    /*Para Cliente*/
    $('.BotonTallas').tooltip('Agregar Tallas.', { mode: 'tr', width: 100 });
    $('.BotonOperaciones').tooltip('Agregar Operaciones.', { mode: 'tr', width: 110 });
    /*Para Orden*/
    $('.BotonAgregarHojaBulto').tooltip('Crear Hoja Bulto.', { mode: 'tr', width: 130 });
    $('.BotonGenerarHojaBultos').tooltip('Generar Hoja Bulto.', { mode: 'tr', width: 130 });
    $('.agregarFunciones').tooltip('Agregar Funciones.', { mode: 'tr', width: 130 });

}