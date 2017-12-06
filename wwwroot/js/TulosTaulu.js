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
    $.getJSON("/api/score", function (data) {
        var html = "<div style='display:inline-block; text-align:right'><br>home<br>away</div>";
        for (key in data) {
            html += "<div style='display:inline-block; text-align:center; padding: 0px 5px'>";
            html += data[key]['periodInning'] + "<br>";
            html += data[key]['homeRuns'] + "<br>";
            html += data[key]['awayRuns'];
            html += "</div>";
        }
        $("#score").html(html);
    });
}

$("#inningChange").on("click", function () {
    disableButtons();
    $.ajax({

        type: "GET",
        url: "/api/inningchange",
        statusCode: {
            200: function () {
                updateView();
            },
            500: function (jqXHR) {
                $("#status").html(jqXHR.responseText);
            }
        },
        complete: enableButtons()
    });
});


$("#undoChanges").on("click", function () {
    disableButtons();
    $.ajax({
        type: "GET",
        url: "/api/undo",
        statusCode: {
            200: function () {
                updateView();
            },
            500: function (jqXHR) {
                $("#status").html(jqXHR.responseText);
            }
        },
        complete: enableButtons()
    });
});

$('#editControl').on("click", function () {
    $('.editControl').toggle();
    $('#debug').toggle();

    $(this).toggle(
        function () {
            $(this).html("Debug On");
        },
        function () {
            $(this).html("Debug Off");
        }
    );
    $(this).show();
});


$("#updateTulosTaulu button").on("click", function (event) {
    disableButtons();
    event.preventDefault();
    event.stopPropagation();

    if ($(this).data('target') == 'periodInning'){

        var tmpTurn = $("input[name='inningTurn']").val();

        switch ($(this).data('method')) {
            case 'minus':
                if (tmpTurn == 'L') {
                    $("input[name='inningTurn']").val('A');
                } else {
                    $("input[name='inningTurn']").val('L');
                    var tmp = +$("input[name='periodInning']").val() - 1;
                    $("input[name='periodInning']").val(tmp);
               }

                break;
            case 'plus':
                if (tmpTurn == 'A') {
                    $("input[name='inningTurn']").val('L');
                } else {
                    $("input[name='inningTurn']").val('A');
                    var tmp = +$("input[name='periodInning']").val() + 1;
                    $("input[name='periodInning']").val(tmp);
                }
                break;
        }

    } else {
        changeInputValue($(this).data('target'), $(this).data('method'));
    }

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
            500: function (jqXHR) {
                $("#status").html(jqXHR.responseText);
            }
        }
    });
    $('#startModal').modal('hide');
});

$("#superModal .modal-body button").on("click", function (event) {

    $("input[name='inningInsideTeam']").val($(this).data('team'));
    updateTulosTaulu();

    $('#superModal').modal('hide');
});


function updateTulosTaulu() {
    disableButtons();

    $.ajax({
        type: "POST",
        url: $("#updateTulosTaulu").attr("action"),
        data: $("#updateTulosTaulu").serialize(),
        statusCode: {
            200: function () {
                updateView();
            },
            500: function (jqXHR) {
                $("#status").html(jqXHR.responseText);
            }
        },
        complete: enableButtons()
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

function disableButtons() {
    $('button').prop("disabled", true);
}
function enableButtons() {
    setTimeout(function () {
        $('button').prop("disabled", false);
    }, 500);
   
}

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});
