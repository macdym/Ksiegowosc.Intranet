// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#view_details').on('click', function () {
    $('#action_index').toggle();
    $('#action').toggle();
    $('#napis_kontrachent').toggle();
});

$('#filters').on('click', function () {
    $('#filters_content').toggle();
});

$("#dropDown").change(function () {
    $("#pageSize").submit();
});

$(function () {
    $('[data-toggle="tooltip"]').tooltip()
});

$(function () {
    $("table").resizableColumns();
});

$(document).ready(function () {
    $("#MyModal").modal();
});

$(function () {
    $(".table").on("click", "tr[role=\"button\"]", function (e) {
        window.location = $(this).data("href");
    });
});

$(function () {
    var current = location.pathname;
    $('.nav-tabs li a').each(function () {
        var $this = $(this);
        if (current.indexOf($this.attr('href')) !== -1) {
            $this.addClass('active');
        }
    })
});

$('#dataTable tbody tr').click(function () {
    $(this).addClass('bg-secondary').siblings().removeClass('bg-secondary');
}).change(function () {
    $(this).addClass('bg-secondary').siblings().removeClass('bg-secondary');
});

$(document).ready(function () {
    $('#fileUpload').change(function () {
        var path = $(this).val();
        if (path != '' && path != null) {
            var q = path.substring(path.lastIndexOf('\\') + 1);
            $('#labelFilename').html(q);
        }
    });
});

$(document).ready(function () {
    $('#bar').on('click', function () {
        $('#sidebar').slideToggle("slow");
    });
});

function Action(id, controller, action) {
    $.ajax({
        url: controller + "/" + action,
        type: "GET",
        data: { id: id }
    }).done(function (partialViewResult) {
        $("#partialC").html(partialViewResult);
    })
};
//function Select(obj, id) {
//    var idKontrachenta = id;
//    $(obj).addClass('active').siblings().removeClass('active');
//    var tbl = document.getElementById("kontrachentGrid")
//    var firstRow = tbl.getElementsByTagName("TR")[0];
//    var oldRow = tbl.rows[firstRow.getElementsByTagName("input")[0].value];
//    if (oldRow != null) {
//        oldRow.className = '';
//    }
//    firstRow.getElementsByTagName("input")[0].value = obj.rowIndex;
//    $.ajax({
//        url: "/Kontrachent/Details",
//        type: "GET",
//        data: { id: idKontrachenta }
//    }).done(function (partialViewResult) {
//        $("#kontrachentC").html(partialViewResult);
//    })
//}