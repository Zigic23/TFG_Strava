$(document).ready(function () {
    var trainings = [
        {
            numSerie: 1,
            objective: "1 min 31 seg",
            timeDone: "1 min 30 seg",
            difference: "1 seg"
        },
        {
            numSerie: 2,
            objective: "1 min 31 seg",
            timeDone: "1 min 30 seg",
            difference: "1 seg"
        },
        {
            numSerie: 3,
            objective: "1 min 31 seg",
            timeDone: "1 min 15 seg",
            difference: "16 seg"
        },
        {
            numSerie: 4,
            objective: "1 min 31 seg",
            timeDone: "1 min 20 seg",
            difference: "11 seg"
        },
        {
            numSerie: 5,
            objective: "1 min 31 seg",
            timeDone: "1 min 30 seg",
            difference: "1 seg"
        },
        {
            numSerie: 6,
            objective: "1 min 31 seg",
            timeDone: "1 min 20 seg",
            difference: "11 seg"
        },
        {
            numSerie: 7,
            objective: "1 min 31 seg",
            timeDone: "1 min 30 seg",
            difference: "1 seg"
        }
    ];

    $("#detail_grid").jsGrid({
        width: "100%",

        sorting: true,
        paging: true,

        data: trainings,

        fields: [
            { name: "numSerie", title: "Nº Serie", type: "number" },
            { name: "objective", title: "Objetivo", type: "text" },
            { name: "timeDone", title: "Tiempo Realizado", type: "text" },
            { name: "difference", title: "Diferencia", type: "text" }
        ]
    });
});