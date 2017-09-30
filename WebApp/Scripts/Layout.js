(function () {
    jQuery.validator.methods.date = function (value, element) {
        var formats = ["DD/MM/YYYY", "DD/MM/YYYY HH:mm"];
        return moment(value, formats, true).isValid();
    };
})(jQuery, moment);

PNotify.removeAll();

$(document).ready(function () {
    $('.date').datetimepicker({
        viewMode: 'days',
        format: 'DD/MM/YYYY',
        locale: 'es',
        ignoreReadonly: true
    });

    $('.table').DataTable({
        language: {
            url: "../Scripts/DataTables/Spanish.json",
            decimal: ",",
            thousands: "."
        }
    })
});

var soloNumeros = function (e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

function soloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toLowerCase();
    letras = " áéíóúabcdefghijklmnñopqrstuvwxyz";
    especiales = "8-37-39-46";

    tecla_especial = false
    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        return false;
    }
}

var ConsultarPasante = function (value, callback) {
    $.ajax({
        url: '../../Pasante/ConsultarPasante',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: value}),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            callback(data);
            $("#ModalPasantes").modal("hide");
        },
        error: function (xhr) {
            alert('error');
        }
    })
}

var ConsultarTutor = function (value, callback) {
    $.ajax({
        url: '../../Tutor/ConsultarTutor',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: value }),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            callback(data);
            $("#ModalTutores").modal("hide");
        },
        error: function (xhr) {
            alert('error');
        }
    })
}

var ConsultarInstitucion = function (value, callback) {
    $.ajax({
        url: '../../Institucion/ConsultarInstitucion',
        dataType: "json",
        type: "POST",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: value }),
        async: true,
        processData: false,
        cache: false,
        success: function (data) {
            callback(data);
            $("#ModalInstituciones").modal("hide");
        },
        error: function (xhr) {
            alert('error');
        }
    })
}