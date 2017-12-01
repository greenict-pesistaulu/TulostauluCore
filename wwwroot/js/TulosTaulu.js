function updateView()
{
    $("#controls").show();
    $.getJSON("/api/status", function (data) {
    // var html = "<p>";
    for (key in data) {
        $("input[name='" + key + "']").val(data[key]);
        // html += key + ": " + data[key] + "<br>";
    }

    if (data['inningInsideTeam'] == 'home') {
        $("input[name='homeLastHitter']").show();
        $("input[name='awayLastHitter']").hide();
    } else {
        $("input[name='homeLastHitter']").hide();
        $("input[name='awayLastHitter']").show();
    }
        
    // html += "</p>";
    $("#status").html(html);
    });
    $.getJSON("/api/serial", function (data) {
        var html = "<p>";
        html += data;
        html += "</p>";
        $("#serial").html(html);
    });
}
$("#startGame").on("click", function () {

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

    //Sidebar
    $("#sidebar").mCustomScrollbar({
        theme: "minimal"
    });

    $('#dismiss, .overlay').on('click', function () {
        $('#sidebar').removeClass('active');
        $('.overlay').fadeOut();
    });

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').addClass('active');
        $('.overlay').fadeIn();
        $('.collapse.in').toggleClass('in');
        $('a[aria-expanded=true]').attr('aria-expanded', 'false');
    });

});