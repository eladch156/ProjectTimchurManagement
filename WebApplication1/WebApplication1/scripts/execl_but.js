$(function () {

    $('#exc_but').click(function () {
        $('#combtab').table2excel({
            exclude: ".noExl",
            name: "TichurExist Name",
            filename: "TichurExist"
        });
    });
});





