$(document).on('click', '.toggle-task-group', function () {
    var tasks = $(this).closest('.task-group').find('.tasks-container');

    if ($(this).hasClass('fa-angle-up')) {
        tasks.slideUp();
    } else {
        tasks.slideDown();
    }

    $(this).toggleClass('fa-angle-up fa-angle-down');
});

$(document).on('click', '#add-task-group', function () {
    var newGroup = $('.new-group');

    if (newGroup.is(':hidden')) {
        newGroup.show().find('input').focus();
    } else {
        var input = newGroup.find('input');
        var name = input.val();
        var projectId = $('#Id').val();

        if (name.length > 0) {
            sendNewGroup(projectId, name);
        } else {
            input.focus();
        }
    }
});

$(document).on('focusout', '.new-group > input', function () {
    var name = $(this).val();

    if (name.length > 0) {
        var projectId = $('#Id').val();
        sendNewGroup(projectId, name);
    }

    $(this).closest('.new-group').hide();
});

$(document).on('keydown', '.new-group > input', function (event) {
    if (event.keyCode === 13 && $(this).val().length > 0) {
        $(this).blur();
    } else if (event.keyCode === 27) {
        $(this).val('').blur();
    }
});

$(document).on('focusout', '#task-group-name', function () {
    var name = $(this).text();
    if (name.length > 0 && name !== $(this).data('original-name')) {
        var groupId = $(this).closest('.task-group').data('id');
        renameGroup(groupId, name);
    }
});

$(document).on('keydown', '#task-group-name', function (event) {
    if (event.keyCode === 13) {
        console.log('Hi');
        event.preventDefault();
        if ($(this).text().length > 0) {
            $(this).blur();
        }
    } else if (event.keyCode === 27) {
        $(this).text($(this).data('original-name')).blur();
    }
});

$(document).on('click', '#task-group-delete', function () {
    var $this = $(this);
    dialog('Delete Task Group', 'Are you sure you want to delete this group?\nAll tasks will be removed as well!',
        'Yes', 'No', function () {
            var groupId = $this.closest('.task-group').data('id');
            deleteGroup(groupId);
        });
});

$(document).on('click', '#task-group-toggle-completed', function () {
    $(this).closest('.task-group').find('.task.completed').toggle();
});

function sendNewGroup(projectId, name) {
    var loader = $('.task-groups-container > .loader');
    var error = loader.siblings('.req-error');
    error.hide();
    loader.show();
    $.post('../../Project/AddNewGroup', {
        projectId,
        name
    }).done(function(data) {
        if (data.success) {
            $('.new-group > input').val('');
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

function renameGroup(groupId, name) {
    var loader = $('.task-group[data-id=' + groupId + '] > .task-group-header > .loader');
    var error = loader.siblings('.req-error');
    error.hide();
    loader.show();
    $.post('../../TaskGroup/Rename', {
        groupId,
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
            var groupName = loader.siblings('#task-group-name');
            groupName.text(groupName.data('original-name'));
        }
    });
}

function deleteGroup(groupId) {
    var loader = $('.task-group[data-id=' + groupId + '] > .task-group-header > .loader');
    var error = loader.siblings('.req-error');
    error.hide();
    loader.show();
    $.post('../../TaskGroup/Delete', { groupId })
        .done(function(data) {
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