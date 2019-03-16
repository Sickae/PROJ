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

function sendNewGroup(projectId, name) {
    $.post('../../Project/AddNewGroup', {
        projectId,
        name
    }).done(function(data) {
        if (data) {
            $('.new-group > input').val('');
            location.reload();
        }
    });
}