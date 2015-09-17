$(document).ready(function () {

    $('.js-article-delete').click(function (e) {
        e.preventDefault();
        var flag = confirm('Are you sure?'), 
            article = $(this).parent().parent();

        if (flag) {
            $.ajax({
                url: '/Admin/DeleteArticle',
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