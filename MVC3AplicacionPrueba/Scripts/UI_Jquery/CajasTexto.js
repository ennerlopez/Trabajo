$(document).ready(function () {

    /* $("input:submit, button").button();*/
    $("input:submit, button").addClass("btn btn-primary");
    $("input[type=text]").button().css({
        'font': 'inherit',
        'background': 'white',
        'color': 'black',
        'text-align': 'left',
        'outline': 'none',
        'cursor': 'text'
    });

    $("input[type=password]").button().css({
        'font': 'inherit',
        'background': 'white',
        'color': 'inherit',
        'text-align': 'left',
        'outline': 'none',
        'cursor': 'text'
    });

    $("input[type=textarea]").button().css({
        'font': 'inherit',
        'background': 'white',
        'color': 'inherit',
        'text-align': 'left',
        'outline': 'none',
        'cursor': 'text'
    });

});