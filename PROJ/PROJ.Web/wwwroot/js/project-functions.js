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

$(document).on('focusout', '#project-name', function () {
    var name = $(this).text();
    if (name.length > 0 && name !== $(this).data('original-name')) {
        var projectId = $('#Id').val();
        renameProject(projectId, name);
    }
});

$(document).on('keydown', '#project-name', function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        if ($(this).text().length > 0) {
            $(this).blur();
        }
    } else if (event.keyCode === 27) {
        $(this).text($(this).data('original-name')).blur();
    }
});

$(document).on('click', '#project-delete', function () {
    dialog('Delete Project', 'Are you sure you want to delete this project?',
        'Yes', 'No', function () {
            var projectId = $('#Id').val();
            deleteProject(projectId);
        });
});

function createProject(name) {
    $.post('../../Project/Create', { name })
        .done(function(data) {
            if (data) {
                $('#search-project').val('');
                location.reload();
            }
        });
}

function renameProject(projectId, name) {
    $('.project-header').children('.loader').show();
    $.post('../../Project/Rename', {
        projectId,
        name
    }).done(function(data) {
        if (data) {
            location.reload();
        }
    })
}

function deleteProject(projectId) {
    $.post('../../Project/Delete', { projectId })
        .done(function (data) {
            if (data.success) {
                location.replace(data.redirectUrl);
            }
        });
}