
$(document).ready(function () {
    tblFormate();

});
    function tblFormate() {

        var table = $('#datatablelist').DataTable(
         {
             "searching": true,
             "lengthMenu": [[10, 30, 50, 100, -1], [10, 30, 50, 100, "All"]],
             "ordering": false
         });
        

    }

    $(document).ready(function () {
        $("#datedifpicker").datepicker({
            yearRange: "-50:+0",
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-M-yy',
            numberOfMonths: 1,
            onSelect: function (selected) {
                $("#datedifpickera").datepicker("option", "minDate", selected);
            }
        });
        $("#datedifpickera").datepicker({
            //maxDate: 0,
            yearRange: "-50:+3",
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-M-yy',
            numberOfMonths: 1,
            onSelect: function (selected) {
                $("#datedifpicker").datepicker("option", "maxDate", selected);
            }
        });
    });
