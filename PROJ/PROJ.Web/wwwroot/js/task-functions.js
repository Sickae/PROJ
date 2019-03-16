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

function sendNewTask(groupId, name) {
    $.post('../../TaskGroup/AddNewTask', {
        groupId,
        name
    }).done(function(data) {
        if (data) {
            $('.new-task > input').val('');
            location.reload();
        }
    })
}