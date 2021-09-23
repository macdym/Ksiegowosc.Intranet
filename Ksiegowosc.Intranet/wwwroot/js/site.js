// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
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
    $(function () {
        var current = location.pathname;
        $('.nav-tabs li a').each(function () {
            var $this = $(this);
            if (current.indexOf($this.attr('href')) !== -1) {
                $this.addClass('active');
            }
        })
    });
    $(function () {
        $(".table").on("click", "tr[role=\"button\"]", function (e) {
            window.location = $(this).data("href");
        });
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
});