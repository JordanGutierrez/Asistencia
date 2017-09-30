var sayCheese = new SayCheese('#webcam', { snapshot: true });

sayCheese.start();

//$("#btnCapturar").click(function (e) {
//    sayCheese.takeSnapshot();
//});

$("#formActividad").submit(function (event) {
    sayCheese.takeSnapshot();
    return;
});
    
sayCheese.on('snapshot', function (snapshot) {
    $('#imagen').val(snapshot.toDataURL('img/png'));
});
