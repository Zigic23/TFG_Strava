var currentWeek = 1;

$(document).ready(function () {

    $("#detail_grid").jsGrid({
        width: "100%",

        sorting: true,
        paging: true,
        autoload: true,

        //rowClass: editRowClass,

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

                            //Cogemos el día mas bajo, que será el primero de la semana
                            let lowestDateObj = Math.min.apply(null, response.map(a => new Date(a.Date)));
                            let lowestDate = new Date(lowestDateObj);
                            $("#dateWeekStart").html(`${lowestDate.getDate()}/${lowestDate.getMonth() + 1}/${lowestDate.getFullYear()}`);
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
            { name: "WeekDay", title: "Dia entrenamiento", type: "text", itemTemplate: seeWeekDay },
            { name: "TrainingTypeName", title: "Entrenamiento", type: "text" },
            { name: "DateStr", title: "Fecha entrenamiento", type: "date" },
            { name: "Done", title: "Realizado", type: "text", itemTemplate: doneText },
            { name: "SensationName", title: "Sensaciones", type: "text", type: "text" },
            { title: "Ver resultados", type: "text", itemTemplate: seeResultsButtons }
        ]
    });
})

//function editRowClass(item, itemIndex) {
//    if (item.TrainingTypeCode == 4 || item.TrainingTypeCode == 5)
//        return "darker-row";
//}

function seeWeekDay(value, item) {
    return capitalizeFirstLetter(new Date(item.Date).toLocaleDateString("Es-es", { weekday: 'long' }));
}

function seeResultsButtons(value, item) {
    if (item.TrainingTypeCode != 4 && item.TrainingTypeCode != 5) {
        let url = seeResultsUrl.replace("-1", item.DayTrainingCode);
        return $("<a>").attr("href", url).attr("class", "seeResults").html("Ver resultados");
    } else if ((item.TrainingTypeCode === 4 || item.TrainingTypeCode === 5) && !item.Done) {
        return $("<button>").addClass("seeResults").attr("id", `complete_${item.DayTrainingCode}`).on("click", () => setFinished(item.DayTrainingCode)).html("Completar");
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

function setFinished(DayTrainingCode) {
    $.ajax({
        type: "PUT",
        url: `/Trainings/SetCompleted?DayTrainingCode=${DayTrainingCode}`,
        dataType: "json"
    }).done(function (response) {
        console.log(response);
        if (response.result) {
            $("#detail_grid").jsGrid("loadData");
        } else if (response.message)
            alert(response.message);
        
    }).fail(function () {

    });
}

function capitalizeFirstLetter(string) {
    return string.charAt(0).toUpperCase() + string.slice(1);
}