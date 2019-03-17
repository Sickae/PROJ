$(document).on('click', '#add-task', function () {
    var newTask = $(this).siblings('.new-task');

    if (newTask.is(':hidden')) {
        newTask.show().find('input').focus();
    } else {
        var input = newTask.find('input');
        var name = input.val();
        var groupId = $(this).closest('.task-group').data('id');

        if (name.length > 0) {
            sendNewTask(groupId, name);
        } else {
            input.focus();
        }
    }
});

$(document).on('focusout', '.new-task > input', function () {
    var name = $(this).val();

    if (name.length > 0) {
        var groupId = $(this).closest('.task-group').data('id');
        sendNewTask(groupId, name);
    }

    $(this).closest('.new-task').hide();
});

$(document).on('keydown', '.new-task > input', function (event) {
    if (event.keyCode === 13 && $(this).val().length > 0) {
        $(this).blur();
    } else if (event.keyCode === 27) {
        $(this).val('').blur();
    }
});

$(document).on('focusout', '#task-name', function () {
    var name = $(this).text();
    if (name.length > 0 && name !== $(this).data('original-name')) {
        var taskId = $(this).closest('.task').data('id');
        renameTask(taskId, name);
    }
});

$(document).on('keydown', '#task-name', function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        if ($(this).text().length > 0) {
            $(this).blur();
        }
    } else if (event.keyCode === 27) {
        $(this).text($(this).data('original-name')).blur();
    }
});

function sendNewTask(groupId, name) {
    $('.task-group[data-id=' + groupId + ']').find('.loader').last().show();
    $.post('../../TaskGroup/AddNewTask', {
        groupId,
        name
    }).done(function(data) {
        if (data) {
            $('.new-task > input').val('');
            location.reload();
        }
    });
}

function renameTask(taskId, name) {
    $('.task[data-id=' + taskId + '] > .loader').show();
    $.post('../../Task/Rename', {
        taskId,
        name
    }).done(function(data) {
        if (data) {
            location.reload();
        }
    });
}