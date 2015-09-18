$(document).ready(function () {

    $('.js-delete-article, .js-delete-category').click(function (e) {
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
});