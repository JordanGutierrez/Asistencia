var Pasantes = function (value) {
    ConsultarPasante(value, function (data) {
        if (data.mensaje == "OK")
        {
            var pasante = data.persona;
            $("#Pasante").val(pasante.PrimerNombre + " " + pasante.SegundoNombre + " " + pasante.ApellidoPaterno + " " + pasante.ApellidoMaterno);
            $("#PasanteID").val(pasante.PersonaID);
        }
    });
}

var Tutores = function (value) {
    ConsultarTutor(value, function (data) {
        if (data.mensaje == "OK") {
            var pasante = data.persona;
            $("#Tutor").val(pasante.PrimerNombre + " " + pasante.SegundoNombre + " " + pasante.ApellidoPaterno + " " + pasante.ApellidoMaterno);
            $("#TutorID").val(pasante.PersonaID);
        }
    });
}

var Instituciones = function (value) {
    ConsultarInstitucion(value, function (data) {
        if (data.mensaje == "OK") {
            var institucion = data.institucion;
            $("#Institucion").val(institucion.RazonSocial);
            $("#InstitucionID").val(institucion.InstitucionID);
        }
    });
}