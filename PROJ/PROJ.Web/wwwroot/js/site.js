$(document).ready(function () {
    $('.form-checkbox').before('<div class="form-checkbox-custom">');
});

$(document).on('click', '.form-checkbox-custom', function () {
    $(this).toggleClass('checked');
    $(this).next(':checkbox').first().click();
});