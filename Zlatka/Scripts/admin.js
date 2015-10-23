$(document).ready(function () {

    $('.js-delete').click(function (e) {
        e.preventDefault();

        var flag = confirm('Are you sure?'), 
            article = $(this).parent().parent(), 
            action = $(this).data('action');

        if (flag) {
            $.ajax({
                url: '/Admin/' + action,
                type: 'POST',
                data: { id: e.target.id },
                dataType: 'json',
                success: function (result) {
                    article.remove();
                },
                error: function () { alert('Error!'); }
            });
        }
    });


    tinymce.init({
        selector: '.js-textarea'
    });

    $('.js-tab-select').change(function () {
        var i = $(this)[0].selectedIndex + 1;

        $('.js-tab').hide();
        $('#tab-' + i).show();
    });

});