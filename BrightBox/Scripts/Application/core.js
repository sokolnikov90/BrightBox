$(document).ready(function () {

    var сonstOptionText = "Сейчас все работает штатно, но на дд.мм.гггг запланированы работы";
    $("#divLogout").hide();
    $("#editStatus").hide();
    $.getJSON("/api/technicalworks", function (value) {
        $("#selStatus")[0].selectedIndex = value.Status;
        changeOptionText(value);
    });

    $("#saveTechnicalworks").click(function () {
        var selStatus = document.getElementById("selStatus");
        var status = selStatus.options[selStatus.selectedIndex].value;
        var datetime = (status === "2") ? $("#datePicker")[0].value : null;

        var request = {
            Token: localStorage["BrightBoxToken"],
            Status: status,
            Datetime: datetime
        }
        $.post("api/technicalworks",
            request,
            function (data) {
                changeOptionText(data);
            },
            "json"
        );
    });

    $("#selStatus").change(function () {
        if ($("#selStatus")[0].selectedIndex === 2) {
            var days = 1;
            var tomorrow = new Date(Date.now() + days * 24 * 60 * 60 * 1000);
            var day = ("0" + tomorrow.getDate()).slice(-2);
            var month = ("0" + (tomorrow.getMonth() + 1)).slice(-2);
            var tomorrowStr = tomorrow.getFullYear() + "-" + (month) + "-" + (day);
            $("#datePicker").val(tomorrowStr);
            $("#editDatetime").show();
        } else {
            $("#selStatus option[value=2]").text(сonstOptionText);
            $("#editDatetime").hide();
        }
    });

    function changeOptionText(value) {
        if (value.Status === 2) {
            var date = new Date(value.Datetime);
            var rusDatetime = ("0" + date.getDate()).slice(-2) + "." + ("0" + (date.getMonth() + 1)).slice(-2) + "." + date.getFullYear();
            $("#selStatus option:selected").text(сonstOptionText.replace("дд.мм.гггг", rusDatetime));
            $("#datePicker").val(value.Datetime);
        } else {
            $("#editDatetime").hide();
        }
    }
});