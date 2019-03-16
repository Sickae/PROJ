$(document).on('click', '#add-project', function () {
    var searchInput = $('#search-project');
    var name = searchInput.val();

    if (name.length === 0) {
        searchInput.focus();
    } else {
        createProject(name);
    }
});

$(document).on('keydown', '#search-project', function (event) {
    var name = $(this).val();
    if (event.keyCode === 13 && name.length > 0) {
        createProject(name);
    }
});

function createProject(name) {
    console.log(name);
    $.post('../../Project/Create', { name })
        .done(function(data) {
            if (data) {
                $('#search-project').val('');
                location.reload();
            }
        });
}