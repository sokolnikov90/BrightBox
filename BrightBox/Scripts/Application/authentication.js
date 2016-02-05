$("#adminLogin").click(function () {
    $.post("api/authentication",
        $("#authenticationForm").serialize(),
        function (token, status) {
            if (token != null) {
                localStorage["BrightBoxToken"] = token;
                $("#divLogin").hide();
                $("#divLogout").show();
                $("#selStatus")[0].disabled = false;
                $("#tbLogin").val("");
                $("#tbPassword").val("");
                $("#editStatus").show();
            }
        },
        "json"
    );
});

$("#adminLogout").click(function () {
    window.localStorage.removeItem("BrightBoxToken");
    $("#divLogin").show();
    $("#divLogout").hide();
    $("#editStatus").hide();
    $("#selStatus")[0].disabled = true;
    $.getJSON("/api/technicalworks", function (value) {
        $("#selStatus")[0].selectedIndex = value.Status;
        changeOptionText(value);
    });
});