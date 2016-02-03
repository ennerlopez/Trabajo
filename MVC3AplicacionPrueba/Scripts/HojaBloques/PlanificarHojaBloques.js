
$(document).ready(function () {

    var oTable = $('.TablaHojasBloques').dataTable({
        "bJQueryUI": true,
        "sPaginationType": "full_numbers"

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

    $("#formularioHojaBloques").validationEngine('attach', {
        onValidationComplete: function (form, status) {
            if (status == true) {

                var corte = $("#cortes option:selected").text();
                if (existeCorte(corte)) {

                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                    alert("Orden " + corte + ":  ya se encuentra ingresado");
                } else {
                    llenarTabla("colores", "lineas");
                }


            }
        }
    });



    $("#formularioHojaBloquesEditar").validationEngine('attach', {
        onValidationComplete: function (form, status) {
            if (status == true) {

                var corte = $("#cortes option:selected").text();
                if (existeCorte(corte)) {

                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                    alert("Orden " + corte + ":  ya se encuentra ingresado");
                } else {
                    llenarTabla("color", "linea");
                }


            }
        }
    });




    function llenarTabla(nombreComboboxColores, nombreComboboxLineas) {

        var stringComboboxColores = "#" + nombreComboboxColores + " option:selected";
        var stringComboboxLineas = "#" + nombreComboboxLineas + " option:selected";

        disable(nombreComboboxColores, nombreComboboxLineas);

        var modelHojaBloques = new Object();
        modelHojaBloques.corte = $("#cortes option:selected").text();
        modelHojaBloques.capacidadXHora = $("#capacidadXHora").val();
        modelHojaBloques.color = $(stringComboboxColores).text();
        modelHojaBloques.semana = $("#semana").val(); ;
        modelHojaBloques.year = $("#year").val(); ;
        modelHojaBloques.linea = $(stringComboboxLineas).val();

        var misBloques = obtenerBloques();

        $.ajax({
            url: '/HojaBloques/agregarAPlanificacion',
            type: "POST",
            data: JSON.stringify({ model: modelHojaBloques, bloques: misBloques }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {
                    oTable.fnClearTable();

                    $.each(result.listaBloques, function (i, object) {

                        oTable.fnAddData([
                            object.bloque,
                            object.corte,
                              object.seccion,
                            object.capaBulto,
                            object.cantidad,
                            object.tallaCompleta,
                            object.color,
                            object.semana,
                            object.year,
                            object.serie
                            ]);

                    });
                    $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);

                    PreguntarRefrescar();

                } else {
                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);
                }


            }
        });


    }




    function existeCorte(corte) {

        var rows = oTable.fnGetNodes();
        var vf = false;

        for (var i = 0; i < rows.length; i++) {

            var corteTabla = $(rows[i]).find("td:eq(1)").text();

            if (corteTabla == corte) {

                vf = true;
                break;
            }

        }

        return vf;

    }



    /*===========================================================================================================*/

    function obtenerBloques(nombreLinea) {

        var lista = new Array();


        var rows = oTable.fnGetNodes();

        for (var i = 0; i < rows.length; i++) {

            var bloque = new Object();

            var numeroBloque = parseInt($(rows[i]).find("td:eq(0)").text());
            var corte = $(rows[i]).find("td:eq(1)").text();
            var seccion = parseInt($(rows[i]).find("td:eq(2)").text());
            var bulto = $(rows[i]).find("td:eq(3)").text();
            var cantidad = parseInt($(rows[i]).find("td:eq(4)").text());
            var tallaCompleta = $(rows[i]).find("td:eq(5)").text();
            var color = $(rows[i]).find("td:eq(6)").text();
            var semana = parseInt($(rows[i]).find("td:eq(7)").text());
            var anio = parseInt($(rows[i]).find("td:eq(8)").text());
            var serie = parseInt($(rows[i]).find("td:eq(9)").text());
            var codigoLinea = $("#" + nombreLinea + " option:selected").val();

            bloque.bloque = numeroBloque;
            bloque.corte = corte;
            bloque.capaBulto = bulto;
            bloque.cantidad = cantidad;
            bloque.tallaCompleta = tallaCompleta;
            bloque.color = color;
            bloque.semana = semana;
            bloque.year = anio;
            bloque.serie = serie;
            bloque.seccion = seccion;
            bloque.codigoLinea = codigoLinea;
            lista[i] = bloque;
        }

        return lista;
    }




    $("#GuardarPlanificacion").click(function () {


        var rows = oTable.fnGetNodes();
        if (rows.length != 0) {
            guardarPlanificacion();

        }

    });

    $("#EditarPlanificacion").click(function () {


        var rows = oTable.fnGetNodes();
        if (rows.length != 0) {
            editarPlanificacion();
        }

    });

    function redireccionarHome() {
        NoPreguntarRefrescar();
        location.href = "/Home/Index";
    }

    function guardarPlanificacion() {
        var misBloques = obtenerBloques("lineas");

        $.ajax({
            url: '/HojaBloques/guardarPlanificacion',
            type: "POST",
            data: JSON.stringify({ bloques: misBloques }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {

                    $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);

                    window.setTimeout(redireccionarHome, 4000);

                } else {
                    window.clearTimeout();
                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);
                }


            }
        });

    }

    function mensajeAgregadoCorrectamente() {
        $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
        alert("Agregado correctamente");
    }

    function disable(nombreComboboxColores, nombreComboboxLineas) {
        $('#' + nombreComboboxColores).prop('disabled', true);
        $('#' + nombreComboboxLineas).prop('disabled', true);

        $("#semana").attr('readonly', true);
        $("#year").attr('readonly', true);
    }


    function editarPlanificacion() {

        var modelHojaBloques = new Object();
        modelHojaBloques.color = $("#color option:selected").text();
        modelHojaBloques.semana = $("#semana").val();
        modelHojaBloques.year = $("#year").val(); ;
        modelHojaBloques.linea = $("#linea option:selected").val();
        var misBloques = obtenerBloques("linea");

        $.ajax({
            url: '/HojaBloques/editarPlanificacion',
            type: "POST",
            data: JSON.stringify({ bloques: misBloques, model: modelHojaBloques }),
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function (result) {

                if (result.success) {

                    $.noty.consumeAlert({ layout: 'top', type: 'success', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);

                    PreguntarRefrescar();

                } else {
                    $.noty.consumeAlert({ layout: 'top', type: 'error', dismissQueue: true, timeout: 2000 });
                    alert(result.mensaje);
                }


            }
        });

    }



    $("#PonerAlDia").click(function () {
        /*============================================================*/

        if (validarIngreso()) {
            var rows = oTable.fnGetNodes();
            var bloque = 0;
            var nuevoBloque = 0;

            if (rows.length == 0) {
                bloque = 1;
            } else {
                var foo = rows.length - 1;

                bloque = parseInt($(rows[foo]).find("td:eq(0)").text());
                nuevoBloque = bloque + 1;
            }

            if (bloque != 44) {

                oTable.fnAddData([
                    nuevoBloque,
                    "PonerAlDia",
                    0,
                    "P",
                    $("#capacidadXHora").val(),
                    "Talla",
                    $("#colores option:selected").text(),
                    $("#semana").val(),
                    $("#year").val(),
                    "000000"
                ]);

                disable("colores", "lineas");
                mensajeAgregadoCorrectamente();

            }
            else {

                $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
                alert("Programación completada");



            }


        } else {
            $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
            alert("Por favor ingrese los datos correctamente");

        }

        /*============================================================*/
    });



    $("#AlDia").click(function () {
        /*============================================================*/
        var capacidad = $("#capacidadXHora").val();
        if (validar() && capacidad != "") {
            var rows = oTable.fnGetNodes();
            var bloque = 0;
            var nuevoBloque = 0;


            if (rows.length == 0) {
                bloque = 1;
            } else {
                var foo = rows.length - 1;

                bloque = parseInt($(rows[foo]).find("td:eq(0)").text());
                nuevoBloque = bloque + 1;
            }

            if (bloque != 44) {

                oTable.fnAddData([
                    nuevoBloque,
                    "PonerAlDia",
                    0,
                    "P",
                    $("#capacidadXHora").val(),
                    "Talla",
                    $("#color option:selected").text(),
                    $("#semana").val(),
                    $("#year").val(),
                    "000000"
                ]);

                mensajeAgregadoCorrectamente();
            }
            else {

                $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
                alert("Programación completada");



            }


        } else {
            $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
            alert("Por favor ingrese los datos correctamente");

        }

        /*============================================================*/
    });




    function validar() {

        var vf = true;

        var capacidad = $("#capacidadXHora").val();

        if (!esEntero(capacidad)) {
            vf = false;
        }

        return vf;

    }





    function validarIngreso() {

        var vf = true;

        var capacidadXHora = $("#capacidadXHora").val();
        var colores = $("#colores");
        var lineas = $("#lineas");
        var semana = $("#semana").val(); ;
        var year = $("#year").val(); ;



        if (!esEntero(capacidadXHora) || capacidadXHora == "") {
            vf = false;
        }

        if (!esEntero(semana) || semana == "") {
            vf = false;
        }

        if (!isNotFirsElementOfCombobox(colores)) {
            vf = false;
        }

        if (!isNotFirsElementOfCombobox(lineas)) {
            vf = false;
        }



        if (!esEntero(year) || year == "") {
            vf = false;
        }

        return vf;

    }


    function isNotFirsElementOfCombobox(combobox) {

        var vf = true;
        var selectedIndex = combobox.get(0).selectedIndex;
        if (selectedIndex == 0) {
            vf = false;
        }


        return vf;
    }


    function PreguntarRefrescar() {
        $.noty.consumeAlert({ layout: 'top', type: 'warning', dismissQueue: true, timeout: 2000 });
        window.onbeforeunload = function () { return "Asegurese De Guardar Todos Los Cambios!!"; };
    }

    function NoPreguntarRefrescar() {
        window.onbeforeunload = null;
    }



});






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




