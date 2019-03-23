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

$(document).on('click', '#task-delete', function() {
    var $this = $(this);
    dialog('Delete Task', 'Are you sure you want to delete this task?',
        'Yes', 'No', function () {
            var taskId = $this.closest('.task').data('id') | $('.task-details').data('id');
            deleteTask(taskId);
        });
});

$(document).on('change', '#complete-task:checkbox', function () {
    var taskId = $(this).closest('.task').data('id');
    var state = $(this).is(':checked');
    completeTask(taskId, state);
});

$(document).on('click', '#task-toggle-priority', function () {
    $(this).children('span').hide();
    $(this).find('.priority-icon').css('display', 'inline-block');
});

$(document).on('mouseleave', '#task-toggle-priority', function () {
    $(this).find('.priority-icon').hide();
    $(this).children('span').show();
});

$(document).on('click', '.priority-icon', function () {
    var taskId = $(this).closest('.task').data('id');
    var priority = $(this).data('priority');
    setPriority(taskId, priority);
});

$(document).on('click', '#task-join', function () {
    var taskId = $(this).closest('.task').data('id') | $('.task-details').data('id');
    joinTask(taskId);
});

$(document).on('click', '#task-leave', function () {
    var taskId = $(this).closest('.task').data('id') | $('.task-details').data('id');
    leaveTask(taskId);
});

$(document).on('click', '#task-details', function () {
    var taskId = $(this).closest('.task').data('id');
    taskDetails(taskId);
});

$(document).on('click', '#close-task-details', closeTaskDetails);

function sendNewTask(groupId, name) {
    var loader = $('.task-group[data-id=' + groupId + ']').find('.loader').last();
    var error = loader.siblings('.req-error');
    error.hide();
    loader.show();
    $.post('../../TaskGroup/AddNewTask', {
        groupId,
        name
    }).done(function(data) {
        if (data.success) {
            $('.new-task > input').val('');
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

function renameTask(taskId, name) {
    var loader = $('.task[data-id=' + taskId + '] > .loader');
    var error = loader.siblings('.req-error');
    error.hide();
    loader.show();
    $.post('../../Task/Rename', {
        taskId,
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
            var taskName = loader.siblings('#task-name');
            taskName.text(taskName.data('original-name'));
        }
    });
}

function deleteTask(taskId) {
    var loader = $('.task-details-container > .loader');
    if (loader.length === 0) {
        loader = $('.task[data-id=' + taskId + '] > .loader');
    }
    var error = loader.siblings('.req-error');
    error.hide();
    loader.show();
    $.post('../../Task/Delete', { taskId })
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

function completeTask(taskId, state) {
    var loader = $('.task[data-id=' + taskId + '] > .loader');
    var error = loader.siblings('.req-error');
    error.hide();
    loader.show();
    $.post('../../Task/ToggleComplete', {
        taskId,
        state
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

function setPriority(taskId, priority) {
    var loader = $('.task[data-id=' + taskId + '] > .loader');
    var error = loader.siblings('.req-error');
    error.hide();
    loader.show();
    $.post('../../Task/SetPriority', {
        taskId,
        priority
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

function joinTask(taskId) {
    var detailsOpen = $('.task-details').is(':visible');
    var loader = $('.task[data-id=' + taskId + '] > .loader');
    var error = loader.siblings('.req-error');

    if (detailsOpen) {
        loader = $('.task-details').closest('.loader');
    }

    error.hide();
    loader.show();

    if (detailsOpen) {
        var idx = $('[id^=AssignedUsers]').length;
        var inputId = 'AssignedUsers_' + idx + '__Id';
        var inputName = 'AssignedUsers[' + idx + '].Id';
        var hiddenInput = $('<input type="hidden">')
            .prop('id', inputId)
            .prop('name', inputName)
            .val(_USERID);
        $('#task-details-form').prepend(hiddenInput);
        reloadTaskDetails();
    } else {
        $.post('../../Task/JoinTask', { taskId})
            .done(function (data) {
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
}

function leaveTask(taskId) {
    var detailsOpen = $('.task-details').is(':visible');
    var loader = $('.task[data-id=' + taskId + '] > .loader');
    var error = loader.siblings('.req-error');

    if (detailsOpen) {
        loader = $('.task-details').closest('.loader');
    }
    
    error.hide();
    loader.show();

    if (detailsOpen) {
        $('[id^=AssignedUsers]').filter((idx, obj) => parseInt($(obj).val()) === _USERID).remove();
        reloadTaskDetails();
    } else {
        $.post('../../Task/LeaveTask', { taskId})
            .done(function (data) {
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
}

function taskDetails(taskId) {
    var loader = $('.task-details-container > .loader');
    var detailsContainer = $('.task-details-container');
    $('body').append('<div class="overlay details-overlay" id="close-task-details">');
    detailsContainer.show();
    loader.show();
    $.get('../../Task/Details', { taskId })
        .done(function (data) {
            if (data !== null) {
                detailsContainer.append(data);
                formatCheckboxes();
            }
            loader.hide();
        });
}

function closeTaskDetails() {
    $('#task-details-form').submit();
}

function reloadTaskDetails() {
    var detailsForm = $('#task-details-form');
    var data = detailsForm.serializeArray();
    $.post('../../Task/Edit', data)
        .done(function () {
            var taskId = detailsForm.find('.task-details').data('id');
            detailsForm.remove();
            $('.details-overlay').remove();
            taskDetails(taskId);
        });
}