$(document).on('click', '#add-team', function () {
    var searchInput = $('#search-team');
    var name = searchInput.val();

    if (name.length === 0) {
        searchInput.focus();
    } else {
        createTeam(name);
    }
});

$(document).on('keydown', '#search-team', function (event) {
    var name = $(this).val();
    if (event.keyCode === 13 && name.length > 0) {
        createTeam(name);
    }
});

$(document).on('focusout', '#team-name', function () {
    var name = $(this).text();
    if (name.length > 0 && name !== $(this).data('original-name')) {
        var teamId = $('#Id').val();
        renameTeam(teamId, name);
    }
});

$(document).on('keydown', '#team-name', function (event) {
    if (event.keyCode === 13) {
        event.preventDefault();
        if ($(this).text().length > 0) {
            $(this).blur();
        }
    } else if (event.keyCode === 27) {
        $(this).text($(this).data('original-name')).blur();
    }
});

$(document).on('click', '#team-delete', function () {
    dialog('Delete Team', 'Are you sure you want to delete this team? All of its projects will be lost!',
        'Yes', 'No', function () {
            var teamId = $('#Id').val();
            deleteTeam(teamId);
        });
});

$(document).on('click', '#team-set-active', function () {
    var teamId = $('#Id').val();
    setActive(teamId);
});

$(document).on('click', '#add-member', function () {
    var newMember = $('.new-member');

    if (newMember.is(':hidden')) {
        newMember.show().find('input').focus();
    } else {
        var input = newMember.find('input');
        var name = input.val();
        var teamId = $('#Id').val();;

        if (name.length > 0) {
            addMember(teamId, name);
        } else {
            input.focus();
        }
    }
});

$(document).on('focusout', '.new-member > input', function () {
    var name = $(this).val();

    if (name.length > 0) {
        var teamId = $('#Id').val();
        addMember(teamId, name);
    }

    $('.new-member').hide();
});

$(document).on('keydown', '.new-member > input', function (event) {
    if (event.keyCode === 13 && $(this).val().length > 0) {
        $(this).blur();
    } else if (event.keyCode === 27) {
        $(this).val('').blur();
    }
});

$(document).on('click', '#team-remove-member', function () {
    var member = $(this).closest('.list-item');
    var text = 'Are you sure you want to remove ' + member.children('span').text() + ' from the team?';
    dialog('Remove Member', text, 'Yes', 'No', function () {
            var teamId = $('#Id').val();
            var userId = member.data('id');
            removeMember(teamId, userId);
        });
});

function createTeam(name) {
    $.post('../../Team/Create', { name })
        .done(function(data) {
            if (data) {
                $('#search-team').val('');
                location.reload();
            }
        });
}

function renameTeam(teamId, name) {
    $('.page-header').children('.loader').show();
    $.post('../../Team/Rename', {
        teamId,
        name
    }).done(function(data) {
        if (data) {
            location.reload();
        }
    })
}

function deleteTeam(teamId) {
    $('.page-header > .loader').show();
    $.post('../../Team/Delete', { teamId })
        .done(function (data) {
            if (data.success) {
                location.replace(data.redirectUrl);
            }
        });
}

function setActive(teamId) {
    $('.page-header > .loader').show();
    $.post('../../Team/SetActive', { teamId })
        .done(function (data) {
            if (data) {
                location.reload();
            }
        });
}

function addMember(teamId, username) {
    $('.page-header > .loader').show();
    $.post('../../Team/AddMember', {
        teamId,
        username
    }).done(function (data) {
        if (data) {
            $('.new-member > input').val('');
            location.reload();
        }
    });
}

function removeMember(teamId, userId) {
    $('.page-header > .loader').show();
    $.post('../../Team/RemoveMember', {
        teamId,
        userId
    }).done(function (data) {
        if (data.success) {
            if (data.redirectUrl) {
                location.replace(data.redirectUrl);
            } else {
                location.reload();
            }
        }
    });
}