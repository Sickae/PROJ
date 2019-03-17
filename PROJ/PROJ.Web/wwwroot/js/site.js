$(document).ready(function () {
    $('.form-checkbox').each(function () {
        var isChecked = $(this).is(':checked') ? 'checked' : '';
        $(this).before('<div class="form-checkbox-custom ' + isChecked + '">');
    });
});

$(document).on('click', '.form-checkbox-custom', function () {
    $(this).toggleClass('checked');
    $(this).next(':checkbox').first().click();
});

$(document).on('focusout', '.form-input[data-val]', function () {
    $(this).toggleClass('form-input-invalid', !$(this).valid());
});

$(document).on('submit', function (event) {
    $('.loader').show();
});

$(document).on('click', '#close-dialog', removeDialog);

function removeDialog() {
    $('.overlay').remove();
    $('.dialog-box').fadeOut(150, () => $(this).remove());
}

function triggerDialogEvent(func) {
    func();
}



function dialog(title, text, primaryButton, secondaryButton, callback) {
    var overlay = $('<div>').addClass('overlay').prop('id', 'close-dialog');
    var dialog = $('<div>').addClass('dialog-box');
    var dialogHeader = $('<div>').addClass('dialog-box-header');
    var dialogTitle = $('<span>').addClass('dialog-box-title').text(title);
    var dialogCloseIcon = $('<div>').addClass('icon icon-large icon-static')
        .prop('id', 'close-dialog');
    var dialogCloseSpan = $('<span>').addClass('fas fa-times');
    var dialogBody = $('<div>').addClass('dialog-box-body').text(text);
    var dialogFooter = $('<div>').addClass('dialog-box-footer');
    var secButton = $('<button>').addClass('btn btn-danger').text(secondaryButton)
        .prop('id', 'close-dialog');
    var primButton = $('<button>').addClass('btn btn-teal-light')
        .text(primaryButton).prop('id', 'close-dialog').on('click', callback);

    dialogCloseIcon.prepend(dialogCloseSpan);
    dialogHeader.append(dialogTitle).append(dialogCloseIcon);
    dialogFooter.append(secButton).append(primButton);
    dialog.append(dialogHeader).append(dialogBody).append(dialogFooter);

    $('body').prepend(overlay).append(dialog);
}