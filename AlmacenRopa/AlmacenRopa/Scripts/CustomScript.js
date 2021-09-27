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
