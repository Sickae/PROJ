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
})

function sendNewGroup(projectId, name) {
    $('.task-groups-container').children('.loader').show();
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

function renameGroup(groupId, name) {
    $('.task-group[data-id=' + groupId + '] > .task-group-header > .loader').show();
    $.post('../../TaskGroup/Rename', {
        groupId,
        name
    }).done(function(data) {
        if (data) {
            location.reload();
        }
    })
}