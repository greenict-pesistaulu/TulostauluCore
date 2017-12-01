function updateView()
{
    $("#controls").show();
    $.getJSON("/api/status", function (data) {
    var html = "<p>";
    for (key in data) {
        $("input[name='" + key + "']").val(data[key]);
        html += key + ": " + data[key] + "<br>";
    }
    console.log(data);

    if (data['inningInsideTeam'] == 'home') {
        $('.home').show();
        $('.away').hide();

    } else {
        $('.home').hide();
        $('.away').show();
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

$("#inningChange").on("click", function () {
    $.ajax({
        type: "GET",
        url: "/api/inningchange",
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

$("#superPeriod").on("click", function () {
    $.ajax({
        type: "GET",
        url: "/api/superperiod",
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

$("#periodChange").on("click", function () {
    $.ajax({
        type: "GET",
        url: "/api/periodchange",
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
$("#undoChanges").on("click", function () {
    $.ajax({
        type: "GET",
        url: "/api/undo",
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

$("#undoChanges").on("click", function () {
    $.ajax({
        type: "GET",
        url: "/api/periodend",
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

$("#startModal .modal-body button").on("click", function (event) {
    
    $.ajax({
        type: "POST",
        url: '/api/start',
        data: {
            inningInsideTeam: $(this).data('team')
        },
        statusCode: {
            200: function () {
                updateView();
            },
            500: function () {
                $("#status").html("ERROR");
            }
        }
    });
    $('#startModal').modal('hide');
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

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});