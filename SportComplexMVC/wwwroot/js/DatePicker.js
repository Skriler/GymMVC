$(function () {
    const minDateValue = document.querySelector("#MinDate").value;
    const maxDateValue = document.querySelector("#MaxDate").value;

    $('.datepicker').datepicker(
        {
            dateFormat: 'yy-mm-dd',
            minDate: minDateValue,
            maxDate: maxDateValue
        }
    );
});
