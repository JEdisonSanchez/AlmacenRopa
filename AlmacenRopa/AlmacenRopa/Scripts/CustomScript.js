document.addEventListener('DOMContentLoaded', function () {
    var elems = document.querySelectorAll('.sidenav');
    var instances = M.Sidenav.init(elems);
});


$(document).ready(function () {
    $('select').formSelect();
    $('.modal').modal();


    $('.carousel').carousel();
    indicators: true;

    //fullWidth: true;

    setInterval(function () {
        $('.carousel').carousel('next');
    }, 1500);
});


function logearUsuario() {
    $.ajax({
        "url": "http://localhost:62716/Home/Login",
        "type": "post",
        "data": {
            'txtUsuario': $('#txtUsuario').val(),
            'txtPassword': $('#txtPassword').val()
        },
        "dataType": "json",
        "success": function (datos) {
            if (datos.error) {
                M.toast({ html: 'Datos Erroneos' });
            } else {
                M.toast({ html: 'Datos Correctos' });
                window.location.href = 'http://localhost:62716/Product/Index';
            }
        },
        "error": function () {
            //Hacer algo
            alert("Error!");
        }
    });
}