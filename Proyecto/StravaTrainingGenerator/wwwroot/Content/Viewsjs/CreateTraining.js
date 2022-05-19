function onCreateSubmit(event) {
    let daysTrainings = {
        lunes: $("#lunes").val(),
        martes: $("#martes").val(),
        miercoles: $("#miercoles").val(),
        jueves: $("#jueves").val(),
        viernes: $("#viernes").val(),
        sabado: $("#sabado").val(),
        domingo: $("#domingo").val(),
    };

    let previousTraining = undefined;
    let hasError = false;

    let keys = Object.keys(daysTrainings);
    for (let i = 0; i < keys.length; i++) {
        let currentTraining = daysTrainings[keys[i]];

        hasError = hasError || (!currentTraining || (isMainTraining(currentTraining) && isMainTraining(previousTraining)));

        previousTraining = currentTraining;
    }

    let dateStart = new Date($("#start_plan").val());
    hasError = hasError || dateStart.getDay() !== 1;

    let planType = $("#planType").val();
    hasError = hasError || !planType;

    if (hasError) {
        event.preventDefault();
        alert("No se cumplen las condiciones establecidas para crear un entrenamiento, revise los días asignados, si comienza en lunes o si esta seleccionado un tipo de entrenamiento.");
    }
}

function isMainTraining(training) {
    return training == 1 || training == 2 || training == 3;
}