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

        rowClass: resultsRowClass,

        data: DayTraining.ResultsDay,

        fields: [
            { name: "SerieName", title: "Parcial", type: "text" },
            { name: "DistObjective", title: "Distancia", type: "number", itemTemplate: DistObjectiveFunc },
            { name: "RithmObjectiveStr", title: "Rit. Objetivo", type: "text", },
            { name: "RithmDoneStr", title: "Rit. Realizado", type: "text" },
            { name: "Difference", title: "Diferencia", type: "number", cellRenderer: DifferenceColor },
            { name: "Desnivel", title: "Desnivel", type: "number" },
            { name: "AverageFrecuency", title: "Frecuencia media", type: "number" },
            { name: "AverageRate", title: "Cadencia media", type: "number"}
        ]
    });
});

function resultsRowClass(item, itemIndex) {
    if (item.SerieName == "Calentamiento" || item.SerieName == "Enfriamiento" || item.SerieName.startsWith("Descanso"))
        return "darker-row";
}

function DifferenceColor(value, item) {
    if ((item.SerieName == "Calentamiento" || item.SerieName == "Enfriamiento" || item.SerieName.startsWith("Descanso"))
        return $("<td>").text(value).addClass(value > 0 ? "green-cell" : value < 0 ? "red-cell" : "");
    return $("<td>").text(value);
}

function DistObjectiveFunc(value, item) {
    return (item.DistDone != undefined ? item.DistDone : item.DistObjective) + " " + ((item.SerieName == "Calentamiento" || item.SerieName == "Enfriamiento" || item.SerieName.startsWith("Descanso")) && item.DistDone != undefined ? "m" : item.DistType);
}