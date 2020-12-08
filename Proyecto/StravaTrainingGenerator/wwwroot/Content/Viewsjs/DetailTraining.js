$(document).ready(function () {
    var trainings = [
        {
            code: 1,
            trainingDay : "Lunes",
            trainingType : "Entr. Cruzado",
            sensations : "Buena",
            done : "Si"
        },
        {
            code: 2,
            trainingDay: "Martes",
            trainingType: "Series",
            sensations: "Mala",
            done: "Si"
        },
        {
            code: 3,
            trainingDay: "Miercoles",
            trainingType: "Entr. Cruzado",
            sensations: "Regular",
            done: "No"
        },
        {
            code: 4,
            trainingDay: "Jueves",
            trainingType: "Carrera lenta",
            sensations: "Buena",
            done: "No"
        },
        {
            code: 5,
            trainingDay: "Viernes",
            trainingType: "Entr. Cruzado",
            sensations: "Buena",
            done: "No"
        },
        {
            code: 6,
            trainingDay: "Sabado",
            trainingType: "Descanso",
            sensations: "Buena",
            done: "No"
        },
        {
            code: 7,
            trainingDay: "Domingo",
            trainingType: "Carrera larga",
            sensations: "Buena",
            done: "No"
        }
    ];

    $("#detail_grid").jsGrid({
        width: "100%",

        sorting: true,
        paging: true,

        data: trainings,

        fields: [
            { name: "trainingDay", title: "Dia entrenamiento", type: "text" },
            { name: "trainingType", title: "Entrenamiento", type: "text" },
            { name: "sensations", title: "Sensaciones", type: "text" },
            { name: "done", title: "Realizado", type: "text" },
            { title: "Ver resultados", type: "text", itemTemplate: seeResultsButtons }
        ]
    });
})

function seeResultsButtons(value, item) {
    let url = seeResultsUrl.replace("-1", item.code);
    return $("<a>").attr("href", url).attr("class", "seeResults").html("Ver resultados");
}