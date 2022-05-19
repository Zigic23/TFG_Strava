var currentWeek = 1;

$(document).ready(function () {

    $("#detail_grid").jsGrid({
        width: "100%",

        sorting: true,
        paging: true,
        autoload: true,

        controller: {
            loadData: function () {
                var d = $.Deferred();

                $.ajax({
                    type: "GET",
                    url: urlGetDayTrainings.replace("-1", currentWeek),
                    dataType: "json"
                }).done(function (response) {
                    console.log(response);
                    if (response && !response.errorMsg) {
                        for (let i = 0; i < response.length; i++) {
                            let itemResponse = response[i];
                            let EndDate = new Date(itemResponse.Date)
                            itemResponse.DateStr = `${EndDate.getDate()}/${EndDate.getMonth() + 1}/${EndDate.getFullYear()}`;
                            response[i] = itemResponse;
                        }
                        d.resolve(response);
                    } else
                        d.reject(response.errorMsg);
                }).fail(function () {
                    d.reject();
                });

                return d.promise();
            }
        },

        fields: [
            { name: "WeekDay", title: "Dia entrenamiento", type: "number" },
            { name: "TrainingTypeName", title: "Entrenamiento", type: "text" },
            { name: "DateStr", title: "Fecha entrenamiento", type: "date" },
            { name: "Done", title: "Realizado", type: "text", itemTemplate: doneText },
            { title: "Ver resultados", type: "text", itemTemplate: seeResultsButtons }
        ]
    });
})

function seeResultsButtons(value, item) {
    if (item.TrainingTypeCode != 4 && item.TrainingTypeCode != 5) {
        debugger;
        let url = seeResultsUrl.replace("-1", item.DayTrainingCode);
        return $("<a>").attr("href", url).attr("class", "seeResults").html("Ver resultados");
    }
}

function doneText(value, item) {
    return value ? "Terminado" : "Programado";
}

function showWeek(week) {
    $(".week").removeClass("active");
    $("#week_" + week).addClass("active");
    currentWeek = week;
    $("#detail_grid").jsGrid("loadData");
}