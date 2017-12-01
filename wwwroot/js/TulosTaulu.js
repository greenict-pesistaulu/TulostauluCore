﻿function updateView()
{
    $("#controls").show();
        $.getJSON("/api/status", function (data) {
        var html = "<p>";
        for (key in data) {
        $("input[name='" + key + "']").val(data[key]);
            html += key + ": " + data[key] + "<br>";
        }
        html += "</p>";
        $("#status").html(html);
    });
    $.getJSON("/api/serial", function (data) {
        var html = "<p>";
        html += data;
        html += "</p>";
        $("#serial").html(html);
    });
}
$("#btn-start").on("click", function () {
        $.ajax({
            type: "GET",
            url: "/api/start",
            statusCode: {
                200: function () {
                    updateView();
                },
                500: function () {
                    $("#status").html("ERROR");
                }
            }
        });
    });
$("#updateTulosTaulu button").on("click", function (event) {
    event.preventDefault();
    event.stopPropagation();

    changeInputValue($(this).data('target'), $(this).data('method'));

    updateTulosTaulu();
});

function updateTulosTaulu() {
    $.ajax({
        type: "POST",
        url: $("#updateTulosTaulu").attr("action"),
        data: $("#updateTulosTaulu").serialize(),
        statusCode: {
            200: function () {
                updateView();
            },
            500: function () {
                $("#status").html("ERROR");
            }
        }
    });
}
function changeInputValue(target, method) {
    switch (method)
    {
        case 'minus':
            var tmp = +$("input[name='" + target + "']").val() - 1;
            $("input[name='" + target + "']").val(tmp);
            break;
        case 'plus':
            var tmp = +$("input[name='" + target + "']").val() + 1;
            $("input[name='" + target + "']").val(tmp);
            break;

    }
}