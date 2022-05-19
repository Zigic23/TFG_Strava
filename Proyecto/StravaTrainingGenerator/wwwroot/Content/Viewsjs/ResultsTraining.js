$(document).ready(function () {

    for (let i = 0; i < DayTraining.ResultsDay.length; i++) {
        let tempResult = DayTraining.ResultsDay[i];

        if (tempResult.RithmDone)
            tempResult.Difference = tempResult.RithmDone - tempResult.RithmObjective;

        DayTraining.ResultsDay[i] = tempResult;
    }

    $("#detail_grid").jsGrid({
        width: "100%",

        sorting: true,
        paging: true,

        data: DayTraining.ResultsDay,

        fields: [
            { name: "SerieName", title: "Parcial", type: "text" },
            { name: "DistObjective", title: "Dist", type: "number" },
            { name: "Desnivel", title: "Desnivel", type: "number" },
            { name: "MaxFrecuency", title: "FCMx", type: "number" },
            { name: "RithmObjectiveStr", title: "Rit. Objetivo", type: "text",  },
            { name: "RithmDoneStr", title: "Rit. Realizado", type: "text" },
            { name: "Difference", title: "Diferencia", type: "number" }
        ]
    });
});