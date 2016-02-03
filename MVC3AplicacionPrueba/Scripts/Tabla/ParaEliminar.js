

$(document).ready(function () {

    $("#.datatable tbody").delegate("tr", "click", function() {
        var iPos = oTable.fnGetPosition(this);
        if (iPos != null) {
            oTable.fnDeleteRow(iPos); //delete row
        }
    });
    /*

    $("a.elimina").click(function () {
    alert("mensaje");
    var cuello = $(this).parents("tr").find("td").eq(0).html();
    var manga = $(this).parents("tr").find("td").eq(1).html();
    var respuesta = confirm("Desea eliminar la talla: " + cuello + "-" + manga);
    if (respuesta) {
           
           
           
    $(this).parents("tr").fadeOut("normal", function () {
    $(this).remove();
    alert("Talla: " + cuello + "-" + manga + " eliminado");
    /*
    aqui puedes enviar un conjunto de datos por ajax
    $.post("eliminar.php", {ide_usu: id})
              
    });
    }
    });


    */



});
		
           /* function fn_dar_eliminar(){
                $("a.elimina").click(function () {
                    var cuello = $(this).parents("tr").find("td").eq(0).html();
                    var manga = $(this).parents("tr").find("td").eq(1).html();
                    var respuesta = confirm("Desea eliminar la talla: " + cuello + "-" + manga);
                    if (respuesta) {
                        $(this).parents("tr").fadeOut("normal", function () {
                            $(this).remove();
                            alert("Talla: " + cuello + "-" + manga + " eliminado");
                            /*
                            aqui puedes enviar un conjunto de datos por ajax
                            $.post("eliminar.php", {ide_usu: id})
                            
                        });
                    }
                });
            };
            */