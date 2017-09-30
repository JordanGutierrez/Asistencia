$('#BiometricoID').on('change', function () {
    var biometrico = $(this).val();
    $('#CarreraID').removeAttr('disabled');
    $('#CarreraID').empty()
    if (biometrico != "") {
        $.ajax({
            url: '../../Carrera/getCarrerabyBiometrico',
            dataType: "json",
            type: "POST",
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify({ biometrico: biometrico }),
            async: false,
            processData: false,
            cache: false,
            success: function (data) {
                $.each(data.carreras, function (i, item) {
                    $('#CarreraID').append($('<option>', {
                        value: item.CarreraID,
                        text: item.Descripcion
                    }));
                });
            },
            error: function (xhr) {
                alert('error');
            }
        })
    }
});