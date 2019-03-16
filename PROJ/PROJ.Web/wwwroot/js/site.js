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