///<reference path="jquery-1.4.1-vsdoc.js"/>
//var serviceURL = "http://localhost:60996/Handlers/";
var serviceURL = "http://ipo.cldng.com/Handlers/";


function showClock() {
 // new CountUp('January 01, 2011 00:00:00', 'showClock', "COUNT UP since beginning of 2011.");
//    setTimeout("showClock()", 1000)
}

function doPrev(page) {
    window.location.href = page;
}

function doView(page) {
    window.open(page);
}
function postwith(to, p) {
    var myForm = document.createElement("form");
    myForm.method = "post";
    myForm.action = to;
    for (var k in p) {
        var myInput = document.createElement("input");
        myInput.setAttribute("name", k);
        myInput.setAttribute("value", p[k]);
        myForm.appendChild(myInput);
    }
    document.body.appendChild(myForm);
    myForm.submit();
    document.body.removeChild(myForm);
}

function ContinueAccreditationDocz(to) {
    var xid = $("#xid").val();
    var myForm = document.createElement("form");
    myForm.method = "post";
    myForm.action = to;
    var myInput = document.createElement("input");
    myInput.setAttribute("name", 'x');
    myInput.setAttribute("value", xid);
    myForm.appendChild(myInput);
    document.body.appendChild(myForm);
    myForm.submit();
    document.body.removeChild(myForm);
}

function GetLoggedOnAgent(to, txt_email, txt_sys_id, isagent) {
    $('#lg_error_img').hide();
    $("#lg_success_img").hide();
    $("#lg_success_msg").empty();

    $('#acc_error_img').hide();
    $('#acc_error_msg').empty().hide();
    $('#acc_success_img').hide();
    $('#acc_success_msg').empty().hide();
  
    if (txt_email == "") {
        $("#txt_email").css('border-color', 'red');
        $('#lg_error_img').fadeIn(1000).show();
        $("#lg_error_msg").fadeIn(2000).show();
    }
    if ((txt_sys_id == "")) {
        $("#txt_username").css('border-color', 'red');
        $('#lg_error_img').fadeIn(1000).show();
        $("#lg_error_msg").fadeIn(2000).show();
    }

    if ((txt_email != "") && (txt_sys_id != "")) {
        $("#txt_email").css('border-color', 'green');
        $("#txt_sys_id").css('border-color', 'green');
        $('#lg_error_img').fadeIn(1000).hide();
        $("#lg_error_msg").fadeIn(2000).hide();

        $.blockUI({ message: '<h3><img src="../images/loading.gif" /><br/>Please wait...</h3>', css: { backgroundColor: '#1C5E55', color: '#fff', width: '50%', top: '30%', left: '25%'} });

        $.post(serviceURL + to,
        { 'email': txt_email, 'sys_id': txt_sys_id, 'isagent': isagent },
        function (data) {
            var member = data.msg;
            if ((member != null) && (member != "Could not complete the process at this time!")) {
                if (member.xid != "") {
                    if (member.Surname == null) { member.Surname = ""; } if (member.Firstname == null) { member.Firstname = ""; }
                    $('#lg_success_img').fadeIn(1000).show();
                    $("#lg_success_msg").empty().append('Details for Agent <b>' + member.Firstname + ' ' + member.Surname + '</b>.').show();

                    $('#xid').val(member.xid);
                    $('#txt_firstname').val(member.Firstname);
                    $('#txt_surname').val(member.Surname);
                    $('#txt_mail').val(member.Email);
                    $('#txt_mobile').val(member.PhoneNumber);
                    $('#txt_dob').val(member.DateOfBrith);

                    if (isagent == "Agent") {
                        $('#txt_coy_name').val(member.CompanyName);
                        $('#txt_coy_addy').val(member.CompanyAddress);
                        $('#txt_coy_web').val(member.CompanyWebsite);
                        $('#txt_doi').val(member.IncorporatedDate);
                        $('#txt_cont_fullname').val(member.ContactPerson);
                        $('#txt_cont_mobile').val(member.ContactPersonPhone);
                        $('#agt').show();
                    }

                    else {
                        $('#txt_mobile').val(member.Telephone);
                        $('#agt').hide();
                    }
                    $('#reg_tbl').show();
                    $.unblockUI();
                }
                else {
                    $('#reg_tbl').hide();
                    document.getElementById("txt_email").value = txt_email;
                    document.getElementById("txt_sys_id").value = txt_sys_id;
                    $.unblockUI();
                    $('#lg_error_img').fadeIn(1000).show();
                    $("#lg_success_msg").empty().append('No details found for <b>' + txt_sys_id + '</b> <br/>Please check the details and try again!!').show();
                }
            }

            else {
                $('#reg_tbl').hide();
                $.unblockUI();
                $('#lg_error_img').fadeIn(1000).show();
                $("#lg_success_msg").empty().append('No details found for <b>' + txt_sys_id + '</b> <br/>Please check the details and try again!!').show();
            }

        },
        "json"
    );
    }
    else {

    }
}
////////////////////////////////////////////////////////////////////////////////////


function GetAgent(to) {
    $('#lg_error_img').hide();
    $("#lg_success_img").hide();
    $("#lg_success_msg").empty();

    $('#acc_error_img').hide();
    $('#acc_error_msg').empty().hide();
    $('#acc_success_img').hide();
    $('#acc_success_msg').empty().hide();

    var txt_email = $("#txt_email").val();
    var txt_sys_id = $("#txt_sys_id").val();
    var isagent = $("#selectagentType").val();

    if (txt_email == "") {
        $("#txt_email").css('border-color', 'red');
        $('#lg_error_img').fadeIn(1000).show();
        $("#lg_error_msg").fadeIn(2000).show();
    }
    if ((txt_sys_id == "")) {
        $("#txt_username").css('border-color', 'red');
        $('#lg_error_img').fadeIn(1000).show();
        $("#lg_error_msg").fadeIn(2000).show();
    }

    if ((txt_email != "") && (txt_sys_id != "")) {
        $("#txt_email").css('border-color', 'green');
        $("#txt_sys_id").css('border-color', 'green');
        $('#lg_error_img').fadeIn(1000).hide();
        $("#lg_error_msg").fadeIn(2000).hide();

        $.blockUI({ message: '<h3><img src="../images/loading.gif" /><br/>Please wait...</h3>', css: { backgroundColor: '#1C5E55', color: '#fff', width: '50%', top: '30%', left: '25%'} });

        $.post(serviceURL + to,
        { 'email': txt_email, 'sys_id': txt_sys_id, 'isagent': isagent },
        function (data) {
            var member = data.msg;
            if ((member != null) && (member != "Could not complete the process at this time!")) {
                if (member.xid != "") {
                    if (member.Surname == null) { member.Surname = ""; } if (member.Firstname == null) { member.Firstname = ""; }
                    $('#lg_success_img').fadeIn(1000).show();
                    $("#lg_success_msg").empty().append('Details for Agent <b>' + member.Firstname + ' ' + member.Surname + '</b>.').show();

                    $('#xid').val(member.xid);
                    $('#txt_firstname').val(member.Firstname);
                    $('#txt_surname').val(member.Surname);
                    $('#txt_mail').val(member.Email);
                    $('#txt_mobile').val(member.PhoneNumber);
                    $('#txt_dob').val(member.DateOfBrith);

                    if (isagent == "Agent") {
                        $('#txt_coy_name').val(member.CompanyName);
                        $('#txt_coy_addy').val(member.CompanyAddress);
                        $('#txt_coy_web').val(member.CompanyWebsite);
                        $('#txt_doi').val(member.IncorporatedDate);
                        $('#txt_cont_fullname').val(member.ContactPerson);
                        $('#txt_cont_mobile').val(member.ContactPersonPhone);
                        $('#agt').show();
                    }

                    else {
                        $('#txt_mobile').val(member.Telephone);
                        $('#agt').hide();
                    }
                    $('#reg_tbl').show();
                    $.unblockUI();
                }
                else {
                    $('#reg_tbl').hide();
                    document.getElementById("txt_email").value = txt_email;
                    document.getElementById("txt_sys_id").value = txt_sys_id;
                    $.unblockUI();
                    $('#lg_error_img').fadeIn(1000).show();
                    $("#lg_success_msg").empty().append('No details found for <b>' + txt_sys_id + '</b> <br/>Please check the details and try again!!').show();
                }
            }

            else {
                $('#reg_tbl').hide();
                $.unblockUI();
                $('#lg_error_img').fadeIn(1000).show();
                $("#lg_success_msg").empty().append('No details found for <b>' + txt_sys_id + '</b> <br/>Please check the details and try again!!').show();
            }

        },
        "json"
    );
    }
    else {

    }
}
////////////////////////////////////////////////////////////////////////////////////
function updateAccreditation(to) {
    $('#acc_error_img').hide();
    $("#acc_success_img").hide();
    $("#acc_success_msg").empty();

    var xid = $("#xid").val();
    var txt_sys_id = $("#txt_sys_id").val();
    var isagent = $("#selectagentType").val();
    var txt_firstname = $("#txt_firstname").val();
    var txt_surname = $("#txt_surname").val();
    var txt_mail = $("#txt_mail").val();
    var txt_mobile = $("#txt_mobile").val();
    var txt_dob = $("#txt_dob").val();
    var txt_coy_name = $("#txt_coy_name").val();
    var txt_coy_addy = $("#txt_coy_addy").val();
    var txt_coy_web = $("#txt_coy_web").val();
    var txt_doi = $("#txt_doi").val();
    var txt_cont_fullname = $("#txt_cont_fullname").val();
    var txt_cont_mobile = $("#txt_cont_mobile").val();
    var txt_xpass = $("#txt_xpass").val();
    var txt_conxpass = $("#txt_conxpass").val();

    if ((txt_mail == "") || (validate_email(txt_mail) == false)) {
        $("#txt_mail").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $('#acc_success_msg').empty().fadeIn(1000).show().append("The email field cannot be empty and must be in the correct format E.g: 'agent@x.com'");
    }
    if ((txt_mobile == "") || (validate_mobile(txt_mobile) == false)) {
        $("#txt_mobile").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $('#acc_success_msg').empty().fadeIn(1000).show().append("The mobile field cannot be empty and must be a mobile GSM number E.g: '08080000000'");
    }
    if (txt_xpass == "") {
        $("#txt_xpass").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $('#acc_success_msg').empty().fadeIn(1000).show().append("Password field cannot be empty!!");
    }
    /////////
    if (txt_firstname == "") {
        $("#txt_firstname").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $("#acc_error_msg").fadeIn(2000).show();
    }
    if (txt_surname == "") {
        $("#txt_surname").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $("#acc_error_msg").fadeIn(2000).show();
    }
    if (txt_dob == "") {
        $("#txt_dob").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $("#acc_error_msg").fadeIn(2000).show();
    }

    if ((txt_conxpass == "")) {
        $("#txt_conxpass").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $('#acc_success_msg').empty().fadeIn(1000).show().append("Comfirm Password field cannot be empty!!");
    }
    if ((txt_xpass != "") && (txt_conxpass != "")) {
        if (txt_xpass != txt_conxpass) {
            $("#txt_xpass").css('border-color', 'red');
            $("#txt_conxpass").css('border-color', 'red');
            $('#acc_success_msg').empty().fadeIn(1000).show().append("Passwords do not match");
            xval = false;
        }
    }

    if ((txt_mail != "")
    && (txt_mobile != "")
    && (txt_xpass != "")
    && (txt_conxpass != "")
    && (txt_xpass == txt_conxpass)
    && (txt_firstname != "")
    && (txt_surname != "")
    && (txt_dob != "")   
    && (validate_mobile(txt_mobile) == true)
    && (validate_email(txt_mail) == true)
    ) {
        $("#txt_email").css('border-color', 'green');
        $("#txt_sys_id").css('border-color', 'green');
        $("#txt_firstname").css('border-color', 'green');
        $("#txt_surname").css('border-color', 'green');
        $("#txt_dob").css('border-color', 'green');      
        $("#txt_xpass").css('border-color', 'green');
        $("#txt_conxpass").css('border-color', 'green');
        $('#lg_error_img').fadeIn(1000).hide();

        $.blockUI({ message: '<h3><img src="../images/loading.gif" /><br/>Please wait...</h3>', css: { backgroundColor: '#1C5E55', color: '#fff', width: '50%', top: '30%', left: '25%'} });

        $.post(serviceURL + to,
        { 'email': txt_mail, 'sys_id': txt_sys_id, 'isagent': isagent, 'firstname': txt_firstname, 'surname': txt_surname, 'mobile': txt_mobile,
            'dob': txt_dob, 'coy_name': txt_coy_name, 'coy_addy': txt_coy_addy, 'coy_web': txt_coy_web, 'doi': txt_doi, 'cont_fullname': txt_cont_fullname,
            'cont_mobile': txt_cont_mobile, 'xpass': txt_xpass, 'xid': xid
        },
        function (data) {
            var member = data.msg;
            if ((member != null) && (member == "updated")) {
                $('#acc_error_img').fadeOut(1000).hide(); $('#acc_error_msg').empty().fadeOut(1000).hide();
                $('#acc_success_img').fadeIn(1000).show();
                $("#acc_success_msg").empty().append('Details for Agent <b>' + txt_firstname + ' ' + txt_surname + '</b> were updated successfully!!').show();
                $('#btnUpdateApplication').hide();
                if (isagent != null) {
                    if (isagent == "Agent") {
                        $("#btnContinue").show();
                    }
                    else {
                        $("#btnSubContinue").show();
                    }

                }
              //  doPrev('./profile.aspx')
                $.unblockUI();
            }
            else {
                $.unblockUI();
                $('#acc_error_img').fadeIn(1000).show();
                $("#acc_success_msg").empty().append('Details for Agent <b>' + txt_firstname + ' ' + txt_surname + '</b> could not be updated. Please try again later!!').show();
            }
        },
        "json"
    );
    }
    else {

    }
}
////////////////////////////////////////////////////////////////////////////////////
function updateLoggedOnAccreditation(to, xid, txt_sys_id, isagent) {
    $('#acc_error_img').hide();
    $("#acc_success_img").hide();
    $("#acc_success_msg").empty();

    var txt_firstname = $("#txt_firstname").val();
    var txt_surname = $("#txt_surname").val();
    var txt_mail = $("#txt_mail").val();
    var txt_mobile = $("#txt_mobile").val();
    var txt_dob = $("#txt_dob").val();
    var txt_coy_name = $("#txt_coy_name").val();
    var txt_coy_addy = $("#txt_coy_addy").val();
    var txt_coy_web = $("#txt_coy_web").val();
    var txt_doi = $("#txt_doi").val();
    var txt_cont_fullname = $("#txt_cont_fullname").val();
    var txt_cont_mobile = $("#txt_cont_mobile").val();
    var txt_xpass = $("#txt_xpass").val();
    var txt_conxpass = $("#txt_conxpass").val();

    if ((txt_mail == "") || (validate_email(txt_mail) == false)) {
        $("#txt_mail").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $('#acc_success_msg').empty().fadeIn(1000).show().append("The email field cannot be empty and must be in the correct format E.g: 'agent@x.com'");
    }
    if ((txt_mobile == "") || (validate_mobile(txt_mobile) == false)) {
        $("#txt_mobile").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $('#acc_success_msg').empty().fadeIn(1000).show().append("The mobile field cannot be empty and must be a mobile GSM number E.g: '08080000000'");
    }
    if (txt_xpass == "") {
        $("#txt_xpass").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $('#acc_success_msg').empty().fadeIn(1000).show().append("Password/Comfirm Password field(s) cannot be empty!!");
    }
    /////////
    if (txt_firstname == "") {
        $("#txt_firstname").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $("#acc_error_msg").fadeIn(2000).show();
    }
    if (txt_surname == "") {
        $("#txt_surname").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $("#acc_error_msg").fadeIn(2000).show();
    }
    if (txt_dob == "") {
        $("#txt_dob").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $("#acc_error_msg").fadeIn(2000).show();
    }

    if ((txt_conxpass == "")) {
        $("#txt_conxpass").css('border-color', 'red');
        $('#acc_error_img').fadeIn(1000).show();
        $('#acc_success_msg').empty().fadeIn(1000).show().append("Password/Comfirm Password field(s) cannot be empty!!");
    }
    if ((txt_xpass != "") && (txt_conxpass != "")) {
        if (txt_xpass != txt_conxpass) {
            $("#txt_xpass").css('border-color', 'red');
            $("#txt_conxpass").css('border-color', 'red');
            $('#acc_success_msg').empty().fadeIn(1000).show().append("Passwords do not match");
            xval = false;
        }
    }

    if ((txt_mail != "")
    && (txt_mobile != "")
    && (txt_xpass != "")
    && (txt_conxpass != "")
    && (txt_xpass == txt_conxpass)
    && (txt_firstname != "")
    && (txt_surname != "")
    && (txt_dob != "")
    && (validate_mobile(txt_mobile) == true)
    && (validate_email(txt_mail) == true)
    ) {
        $("#txt_mail").css('border-color', 'green');
        $("#txt_mobile").css('border-color', 'green');
        $("#txt_sys_id").css('border-color', 'green');
        $("#txt_firstname").css('border-color', 'green'); 
        $("#txt_surname").css('border-color', 'green');
        $("#txt_dob").css('border-color', 'green');
        $("#txt_xpass").css('border-color', 'green');
        $("#txt_conxpass").css('border-color', 'green');
        $('#lg_error_img').fadeIn(1000).hide();

        $.blockUI({ message: '<h3><img src="../images/loading.gif" /><br/>Please wait...</h3>', css: { backgroundColor: '#1C5E55', color: '#fff', width: '50%', top: '30%', left: '25%'} });

        $.post(serviceURL + to,
        { 'email': txt_mail, 'sys_id': txt_sys_id, 'isagent': isagent, 'firstname': txt_firstname, 'surname': txt_surname, 'mobile': txt_mobile,
            'dob': txt_dob, 'coy_name': txt_coy_name, 'coy_addy': txt_coy_addy, 'coy_web': txt_coy_web, 'doi': txt_doi, 'cont_fullname': txt_cont_fullname,
            'cont_mobile': txt_cont_mobile, 'xpass': txt_xpass, 'xid': xid
        },
        function (data) {
            var member = data.msg;
            if ((member != null) && (member == "updated")) {
                $('#acc_error_img').fadeOut(1000).hide(); $('#acc_error_msg').empty().fadeOut(1000).hide();
                $('#acc_success_img').fadeIn(1000).show();
                $("#acc_success_msg").empty().append('Details for Agent <b>' + txt_firstname + ' ' + txt_surname + '</b> were updated successfully!!').show();
                $("#btnUpdateApplication").hide();

                if (isagent != null) {
                    if (isagent == "Agent") {
                        $("#btnContinue").show();
                    }
                    else {
                        $("#btnSubContinue").show();
                    }
                }
                $.unblockUI();
            }
            else {
                $.unblockUI();
                $('#acc_error_img').fadeIn(1000).show();
                $("#acc_success_msg").empty().append('Details for Agent <b>' + txt_firstname + ' ' + txt_surname + '</b> could not be updated. Please try again later!!').show();
            }
        },
        "json"
    );
    }
    else {

    }
}
////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////VALIDATION SECTION////////////////////////////////////
function validate_email(inputstr) {
    var pattern =
/^[a-zA-Z0-9_\-.]+@([a-zA-Z0-9\-]+\.)+[a-zA-Z]{2,3}$/;
    if (inputstr.search(pattern) > -1) {
        return true;
    } else {
        return false;
    }
}

function validate_mobile(inp) {
    var pattern = /^\d{11}$/;
    if (inp.search(pattern) > -1) {
        return true;
    } else {
        return false;
    }
}

function validate_ban(inp) {
    var pattern = /^\d{1,10}$/;
    if (inp.search(pattern) > -1) {
        return true;
    }
    else {
        return false;
    }
}


