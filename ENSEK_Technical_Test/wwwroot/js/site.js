// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {

    $('#MeterReadingsUpload').on('change', function () {
        $(this).find('.loading').removeClass('d-none');
        var formData = new FormData();

        formData.append('csv', this.files[0]);
        console.log(formData);

        $.ajax({
            type: "POST",
            url: "/Home/UploadMeterReadings",
            data: formData,
            contentType: false,
            processData: false,
            success: function (data) {
                $('#MeterReadings').html(data);
            },
            error: function (error) {
                $('#MeterReadings').html(error);
            },
            complete: function (data) {
                $(this).find('.loading').addClass('d-none');
            }
        });

    });

});