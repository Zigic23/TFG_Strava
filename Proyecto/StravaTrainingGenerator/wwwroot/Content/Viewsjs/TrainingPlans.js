$(document).ready(function () {
    var clients = [
        {
            code: 1,
            fcStart : "23-11-2020",
            fcEnd : "08-12-2020",
            planType : "10 km (12 semanas)",
            initialMark : "21 min. 5 seg."
        },
        {
            code: 2,
            fcStart : "23-11-2020",
            fcEnd : "08-12-2020",
            planType : "10 km (12 semanas)",
            initialMark : "21 min. 5 seg."
        },
        {
            code: 3,
            fcStart : "23-11-2020",
            fcEnd : "08-12-2020",
            planType : "10 km (12 semanas)",
            initialMark : "21 min. 5 seg."
        },
        {
            code: 4,
            fcStart : "23-11-2020",
            fcEnd : "08-12-2020",
            planType : "10 km (12 semanas)",
            initialMark : "21 min. 5 seg."
        },
        {
            code: 5,
            fcStart : "23-11-2020",
            fcEnd : "08-12-2020",
            planType : "10 km (12 semanas)",
            initialMark : "21 min. 5 seg."
        },
        {
            code: 6,
            fcStart : "23-11-2020",
            fcEnd : "08-12-2020",
            planType : "10 km (12 semanas)",
            initialMark : "21 min. 5 seg."
        }
    ];

    $("#training_grid").jsGrid({
        width: "100%",

        sorting: true,
        paging: true,

        data: clients,

        fields: [
            { name: "fcStart", title: "Fecha inicio plan", type: "date" },
            { name: "fcEnd", title: "Fecha fin de plan", type: "date" },
            { name: "planType", title: "Tipo de plan", type: "text" },
            { name: "initialMark", title: "Marca inicial 5km", type: "text" }
        ],
        rowDoubleClick: function (args) {
            var getData = args.item;
            detailUrl = detailUrl.replace("-1", getData.code);

            window.location.href = detailUrl;
        }
    });
})