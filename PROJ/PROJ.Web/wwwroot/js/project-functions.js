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
            deleteProject(projectId, true);
        });
});

$(document).on('click', '#project-toggle-completed', function () {
    var tasks = $('.task.completed');
    if (tasks.filter((idx, obj) => !$(obj).is(':hidden')).length > 0) {
        tasks.hide();
    } else {
        tasks.show();
    }
});

$(document).on('click', '#add-project', function () {
    var newProject = $('.new-project');

    if (newProject.is(':hidden')) {
        newProject.show().find('input').focus();
    } else {
        var input = newProject.find('input');
        var name = input.val();
        var teamId = $('#Id').val();;

        if (name.length > 0) {
            createProject(teamId, name);
        } else {
            input.focus();
        }
    }
});

$(document).on('focusout', '.new-project > input', function () {
    var name = $(this).val();

    if (name.length > 0) {
        var teamId = $('#Id').val();
        createProject(teamId, name);
    }

    $('.new-project').hide();
});

$(document).on('keydown', '.new-project > input', function (event) {
    if (event.keyCode === 13 && $(this).val().length > 0) {
        $(this).blur();
    } else if (event.keyCode === 27) {
        $(this).val('').blur();
    }
});

$(document).on('click', '.toggle-container', function () {
    var tasks = $(this).closest('.team-container').find('.list-container');

    if ($(this).hasClass('fa-angle-up')) {
        tasks.slideUp();
    } else {
        tasks.slideDown();
    }

    $(this).toggleClass('fa-angle-up fa-angle-down');
});

$(document).on('click', '#team-delete-project', function () {
    var $this = $(this);
    dialog('Delete Project', 'Are you sure you want to delete this project?',
    'Yes', 'No', function () {
        var projectId = $this.closest('.list-item').data('id');
        deleteProject(projectId, false);
    });
});

function createProject(teamId, name) {
    var loader = $('.page-header > .loader');
    var error = $('.page-header > .req-error');
    error.hide();
    loader.show();
    $('.new-project > input').val('');
    $.post('../../Project/Create', {
        teamId,
        name
    }).done(function(data) {
        if (data.success) {
            location.reload();
        } else {
            loader.hide();
            error.css('display', 'flex');
            if (data.errorMessage) {
                error.find('#error-message').text(data.errorMessage);
            }
        }
    });
}

function renameProject(projectId, name) {
    var loader = $('.page-header > .loader');
    var error = $('.page-header > .req-error');
    error.hide();
    loader.show();
    $.post('../../Project/Rename', {
        projectId,
        name
    }).done(function(data) {
        if (data.success) {
            location.reload();
        } else {
            loader.hide();
            error.css('display', 'flex');
            if (data.errorMessage) {
                error.find('#error-message').text(data.errorMessage);
            }
            var projectName = loader.siblings('#project-name');
            projectName.text(projectName.data('original-name'));
        }
    })
}

function deleteProject(projectId, redirect) {
    redirect = redirect || false;
    var loader = $('.page-header > .loader');
    var error = $('.page-header > .req-error');
    error.hide();
    loader.show();
    $.post('../../Project/Delete', { projectId })
        .done(function (data) {
            if (data.success) {
                if (redirect) {
                    location.replace(data.redirectUrl);
                } else {
                    location.reload();
                }
            } else {
                loader.hide();
                error.css('display', 'flex');
                if (data.errorMessage) {
                    error.find('#error-message').text(data.errorMessage);
                }
            }
        });
}