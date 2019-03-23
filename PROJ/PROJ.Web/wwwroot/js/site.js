$(document).ready(function () {
    formatCheckboxes();
});

$(document).on('click', '.form-checkbox-custom', function () {
    $(this).toggleClass('checked');
    $(this).next(':checkbox').first().click();
});

$(document).on('focusout', '.form-input[data-val]', function () {
    $(this).toggleClass('form-input-invalid', !$(this).valid());
});

$(document).on('submit', function (event) {
    if ($(event.target).hasClass('page-login-form')) {
        $('.loader').show();
    } else {
        $(event.target).siblings('.loader').show();
    }
});

$(document).on('click', '#close-dialog', removeDialog);

$(document).on('click', '.side-menu-toggle', function () {
    $(this).toggleClass('fa-angle-left fa-angle-right');
    $(this).closest('.side-menu').toggleClass('closed');
});

// https://codepen.io/vsync/pen/frudD
$(document).on('focus.auto-expand', 'textarea.auto-expand', function () {
    var savedValue = this.value;
    this.value = '';
    this.baseScrollHeight = this.scrollHeight;
    this.value = savedValue;
}).on('input.auto-expand', 'textarea.auto-expand', function () {
    var minRows = this.getAttribute('data-min-rows')|0, rows;
    this.rows = minRows;
    rows = Math.ceil((this.scrollHeight - this.baseScrollHeight) / 16);
    this.rows = minRows + rows;
});

function formatCheckboxes() {
    $('.form-checkbox').each(function () {
        if ($(this).siblings('div.form-checkbox-custom').length === 0) {
            var isChecked = $(this).is(':checked') ? 'checked' : '';
            $(this).before('<div class="form-checkbox-custom ' + isChecked + '">');
        }
    });
}

function removeDialog() {
    $('.dialog-overlay').remove();
    $('.dialog-box').fadeOut(150, () => $(this).remove());
}

function triggerDialogEvent(func) {
    func();
}

function dialog(title, text, primaryButton, secondaryButton, callback) {
    var overlay = $('<div>').addClass('overlay dialog-overlay').prop('id', 'close-dialog');
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

function setupAjaxCSRFToken(fieldName, token) {
    $.ajaxSetup({
        headers: {
            [fieldName]: token
        }
    });
}