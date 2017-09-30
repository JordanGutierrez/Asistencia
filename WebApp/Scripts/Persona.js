var codCandidatoIndex = $('#PersonaID').val();
var fotoUrl = "../..//Pasante/Foto/" + codCandidatoIndex;
var urlBase = "../../"
$("#Foto").fileinput({
    showCaption: false,
    showClose: false,
    showBrowse: false,
    browseOnZoneClick: true,
    uploadUrl: fotoUrl,
    language: "es",
    maxFileSize: 2048,
    removeLabel: '',
    removeIcon: '<i class="glyphicon glyphicon-remove"></i>',
    removeTitle: "Cancelar/Deshacer",
    msgErrorClass: 'alert alert-block alert-danger',
    defaultPreviewContent: '<img src="' + urlBase + "Pasante/Image/" + codCandidatoIndex + "?random=" + ((new Date()).getMilliseconds() - 1) + '" class="center-block img-responsive" width="160" /><span class="text-primary center-block">Click para cambiar foto</span>',
    layoutTemplates: { main2: '{preview} {remove} {browse}' },
    allowedFileExtensions: ["jpg", "png", "gif"]
});

$("#Foto").on('fileuploaded', function (event, data, previewId, index) {

    var fotoFileName = data.files[0].name;

    if (fotoFileName.length <= 164) {
        $('#Foto').fileinput('destroy');
    }

    $("#Foto").fileinput({
        showCaption: false,
        showClose: false,
        showBrowse: false,
        browseOnZoneClick: true,
        uploadUrl: fotoUrl,
        language: "es",
        maxFileSize: 2048,
        removeLabel: '',
        removeIcon: '<i class="glyphicon glyphicon-remove"></i>',
        removeTitle: "Cancelar/Deshacer",
        msgErrorClass: 'alert alert-block alert-danger',
        defaultPreviewContent: '<img src="' + urlBase + "Pasante/Image/" + codCandidatoIndex + "?random=" + (new Date()).getMilliseconds() + '" class="center-block img-responsive" width="160" /><span class="text-primary center-block">Click para cambiar foto</span>',
        layoutTemplates: { main2: '{preview} {remove} {browse}' },
        allowedFileExtensions: ["jpg", "png", "gif"]
    });
});