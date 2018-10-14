$(document).ready(function () {
    $('.content-place-holder').each(function (evt) {
        var url = $(this).data('url');
        var placeHolder = this;
        $.ajax({
            async: true,
            url: url,
            data: {}
        }).done(function (result) {
            $(placeHolder).html(result);
        });
    });
    $('body').on('click', ".btn-open-modal", function (evt) {
        var url = $(this).data('url');
        var placeHolder = $('.modal-content');
        $.ajax({
            async: true,
            url: url,
            data: {}
        }).done(function (result) {
            $(placeHolder).html(result);
        });
    });
    $('#main-modal').on('hidden.bs.modal', function () {
        var placeHolder = $('.modal-content');
        placeHolder.html("<div class='spinner'></div> <br /> <br /></div >");
    });
});