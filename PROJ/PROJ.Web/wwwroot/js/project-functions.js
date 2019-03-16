$(document).on('click', '#add-project', function () {
    var searchInput = $('#search-project');
    var name = searchInput.val();

    if (name.length === 0) {
        searchInput.focus();
    } else {
        $.post('../../Project/Create', { name })
            .done(function(data) {
                if (data) {
                    location.reload(true);
                }
            });
    }
});