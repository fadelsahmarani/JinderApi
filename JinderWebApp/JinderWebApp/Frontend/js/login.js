$(document).ready(function () {
   $("#login").click(function () {

        var loginInfo = {
            "Username": $("#username").val(),
            "Password": $("#password").val()
        };

        $.ajax({
            url: "http://localhost:9888/api/users/login",
            type: "post",
            contentType: "application/json; charset=UTF-8",
            processData: false,
            data: JSON.stringify(loginInfo),
            statusCode: {
                200: function () {
                    alert("You have successfully logged in.");
                },
                400: function () {
                    alert("Invalid username/password combination.");
                }
            }
        });
    });
});

