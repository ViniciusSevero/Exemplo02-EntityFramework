$(document).ready(function () {
    $("#Bolsa").click(function () {
        $("div.desconto").toggle("slow", function () {
            if ($("div.desconto").css("display") == "none") {
                $("#Desconto").val("");
            }
        });
    });

    //na hora de carregar o form
    if($("#Bolsa").attr("checked")=="checked") {
        $("div.desconto").css("display", "block");
    }

    //activate selectpicker
    $('.selectpicker').selectpicker({
        style: 'btn-primary',
        size: 4
    });

    $("#DataNascimento").datepicker();


});

function passarID(id) {
    $("#Id").val(id);
}