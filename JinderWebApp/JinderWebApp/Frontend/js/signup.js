$(document).ready(function () {
    $("#seeker").click(function () {
        if ($("#seekerDetailsDiv").css("display") == "none")
            $("#seekerDetailsDiv").slideToggle();

        $("#seekerDetailsDiv").slideDown();
    });

    $("#poster").click(function () {
        $("#seekerDetailsDiv").slideUp();
    });

    $("#signup").click(function () {

        var signupInfo = {
            "Username": $("#username").val(),
            "FullName": $("#full_name").val(),
            "DateOfBirth": $("#date_of_birth").val(),
            "Gender": $("input[name='gender']:checked").attr("id"),
            "Address": $("#address").val(),
            "UserType": $("input[name='user_type']:checked").attr("id"),
            "Password": $("#password").val(),
            "Skills": $("#skills").val(),
            "Education": $("#education").val(),
            "Experience": $("#experience").val(),
            "Certification": $("#certifications").val()
        };

        $.ajax({
            url: "http://localhost:9888/api/users/signup",
            type: "post",
            contentType: "application/json; charset=UTF-8",
            processData: false,
            data: JSON.stringify(signupInfo),
            statusCode: {
                201: function () {
                    alert("You have successfully signed up for Jinder.");
                    window.location.href = "login.html";
                },
                409: function () {
                    alert("Username: " + signupInfo.Username + " is already registered.");
                }
            }
        });
    });
});

