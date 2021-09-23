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

$("#createBtn").on("click", function () {
    $.ajax({
        url: "/Kontrachent/Create",
        type: "GET"
    })
        .done(function (partialViewResult) {
            $("#kontrachentC").html(partialViewResult);
        })
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

function Select(id) {
    $.ajax({
        url: "/Kontrachent/Details",
        type: "GET",
        dataType: 'JSON',
        data: { id: id },
        success: function (partialViewResult) {
            convertJsonToHtml
            $("#kontrachentC").html(partialViewResult);
        }
    })
};

//function Select(obj) {
//    obj.className = 'selected';
//    var tbl = document.getElementById("table1")
//    var firstRow = tbl.getElementsByTagName("TR")[0];
//    var oldRow = tbl.rows[firstRow.getElementsByTagName("input")[0].value];
//    if (oldRow != null) {
//        oldRow.className = '';
//    }
//    firstRow.getElementsByTagName("input")[0].value = obj.rowIndex;
//}


//obj.className = 'selected';
//var tbl = document.getElementById("table1")
//var firstRow = tbl.getElementsByTagName("TR")[0];
//var oldRow = tbl.rows[firstRow.getElementsByTagName("input")[0].value];
//if (oldRow != null) {
//    oldRow.className = '';
//}
//firstRow.getElementsByTagName("input")[0].value = obj.rowIndex;

//$(function HighLight(obj) {
    //    var firstRow = document.getElementById("table1").getElementsByTagName("TR")[0];
    //    if (firstRow.getElementsByTagName("input")[0].value != obj.rowIndex) {
    //        obj.className = 'highlight';
    //    }
    //});
    //$(function UnHighLight(obj) {
    //    var firstRow = document.getElementById("table1").getElementsByTagName("TR")[0];
    //    if (firstRow.getElementsByTagName("input")[0].value != obj.rowIndex) {
    //        obj.className = '';
    //    }
    //});