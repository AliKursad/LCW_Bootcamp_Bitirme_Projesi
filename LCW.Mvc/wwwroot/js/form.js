// Document is ready


$(document).ready(function () {
    $("#passCheck").hide();

});


function validate() {
    var email = $("#login").val();
    var pass = $("#password").val();

    var email_regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    var password_regex1 = /([a-z].*[A-Z])|([A-Z].*[a-z])([0-9])+([!,%,&,@,#,$,^,*,?,_,~])/;
    var password_regex2 = /([0-9])/;
    var password_regex3 = /([!,%,&,@,#,$,^,*,?,_,~])/;

    if (email_regex.test(email) == false) {
        $("#passCheck").text("Please Enter Correct Email");
        $("#passCheck").show();
        return false;
    }
    else if (pass.length < 8 || pass.length > 20) {
        $("#passCheck").text("Please Enter Strong Pass");
        $("#passCheck").show();
        return false;
    }
    else {
        return true;
    }
}


$("#submitbtn").click(function () {
    var form = $('#loginForm');
    if (validate()) {
        form.submit();
    }
});