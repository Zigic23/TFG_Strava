$(document).ready(function () {

    $("#training_grid").jsGrid({
        width: "100%",
        height: "auto",

        sorting: true,
        paging: true,
        autoload: true,

        controller: {
            loadData: function () {
                var d = $.Deferred();

                $.ajax({
                    type: "GET",
                    url: urlGetTrainings,
                    dataType: "json"
                }).done(function (response) {
                    console.log(response);
                    if (response && !response.errorMsg) {
                        for (let i = 0; i < response.length; i++) {
                            let itemResponse = response[i];
                            let StartDate = new Date(itemResponse.StartDate)
                            itemResponse.StartDateStr = `${StartDate.getDate()}/${StartDate.getMonth()}/${StartDate.getFullYear()}`;
                            let EndDate = new Date(itemResponse.EndDate)
                            itemResponse.EndDateStr = `${EndDate.getDate()}/${EndDate.getMonth()}/${EndDate.getFullYear()}`;
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
            { name: "StartDateStr", title: "Fecha inicio plan", type: "date" },
            { name: "EndDateStr", title: "Fecha fin de plan", type: "date" },
            { name: "PlanTypeName", title: "Tipo de plan", type: "text" },
            { name: "Start5kmMark", title: "Marca inicial 5km", type: "text" }
        ],
        rowDoubleClick: function (args) {
            var getData = args.item;
            detailUrl = detailUrl.replace("-1", getData.TrainingCode);

            window.location.href = detailUrl;
        }
    });
})