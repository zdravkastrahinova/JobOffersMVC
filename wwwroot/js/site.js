// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#form-comment').on('submit', function (event) {
        event.preventDefault();

        // localhost:port/JobOffers/Details/jobOfferId

        const urlFragments = window.location.pathname.split('/');

        const model = {
            Text: $('#comment').val(),
            JobOfferId: +urlFragments[urlFragments.length - 1]
        };

        if (window.currentComment) {
            model.Id = window.currentComment.Id;

            delete window.currentComment;
        }

        const stringifiedModel = JSON.stringify(model);

        $.ajax({
            type: "POST",
            url: "/Comments/Edit",
            dataType: "text",
            data: { value: stringifiedModel },
            success: function () {
                // redirect
                window.location.reload();
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    $('.edit-link').on('click', function (event) {
        event.preventDefault();

        const Id = $(event.target).data('id');
        const Text = $(event.target).data('text');

        // set value to input
        $('#comment').val(Text);

        window.currentComment = { Id, Text };
    });

});
