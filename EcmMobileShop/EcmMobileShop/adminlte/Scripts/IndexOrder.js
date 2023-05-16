$(function () {
    $("#example1").DataTable({
        "responsive": true, "lengthChange": true, "autoWidth": false,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
});
$(function () {
    // Kiểm tra giá trị khi vừa load trang
    var selectedValue = $('#IdTinhTrangDH').val();
    $('#IdTinhTrangDH option').each(function () {
        if ($(this).val() < selectedValue) {
            $(this).hide();
        }
        if (selectedValue >= 3 && $(this).val() == 5) {
            $(this).hide();
        }
    });
});
