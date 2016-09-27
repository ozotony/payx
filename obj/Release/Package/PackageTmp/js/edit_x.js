function getCountry() {
    var x = "getCountry";
    //starting setting some animation when the ajax starts and completes
    $("#loading")
		.ajaxStart(function () {
		    $(this).show();
		})
		.ajaxComplete(function () {
		    $(this).hide();
		});

		$.ajax({
		    url: '../Handlers/edit_app.ashx',
		    secureuri: false,
		    data: { x: "getCountry" },
		    dataType: 'json',
		    type: 'GET',
		    contentType: 'application/json',
		    success: function (data) {
		        alert(data);
		        var options = '';
		        j = data;
		        for (var i = 0; i < j.length; i++) {
		            options += '<option value="' + j[i].ID + '">' + j[i].name + '</option>';
		        }
		        $("select#nationality").html(options);
		        $("select#residence").html(options);

		    },
		    error: function (data, status, e) {
		        alert(e);

		    }
		});     // end $.ajax


    return false;

}
////////////////////////////////////////////////////////////////////////////////////
function regAssignment() {
    var applicant_name = document.getElementById("applicant_name").value;
    var applicant_address = document.getElementById("applicant_address").value;
    var prevDate = document.getElementById("prevDate").value;
    var certno = document.getElementById("certno").value;
    var trademark = document.getElementById("trademark").value;
    var selectTmDesc = document.getElementById("selectTmDesc").value;
    var xclass = document.getElementById("xclass").value;    
    var xclass_description = document.getElementById("xclass_description").value;
    var assignor_name = document.getElementById("assignor_name").value;
    var assignor_address = document.getElementById("assignor_address").value;
    var assignee_name = document.getElementById("assignee_name").value;
    var assignee_address = document.getElementById("assignee_address").value;
    var agreement_date = document.getElementById("agreement_date").value;
    var agent_name = document.getElementById("agent_name").value;
    var agent_code = document.getElementById("agent_code").value;

    //starting setting some animation when the ajax starts and completes
    $("#loading")
		.ajaxStart(function () {
		    $(this).show();
		})
		.ajaxComplete(function () {
		    $(this).hide();
		});

		$.ajax({
		    url: '../Handlers/assignment_handler.ashx',
		    secureuri: false,
		    data: { x: "addAssignment", applicant_name: applicant_name, applicant_address: applicant_address, prevDate: prevDate, certno: certno, trademark: trademark, selectTmDesc: selectTmDesc, xclass: xclass, xclass_description: xclass_description, assignor_name: assignor_name, assignor_address: assignor_address, assignee_name: assignee_name, assignee_address: assignee_address, agreement_date: agreement_date, agent_name: agent_name, agent_code: agent_code },
		    dataType: 'json',
		    type: 'GET',
		    contentType: 'application/json',
		    success: function (data) {
		        window.location.href = "assignment.aspx?px=" + data + "&sh=0&d=1&b=1";
		    },
		    error: function (data, status, e) {
		       
		    }
		});    // end $.ajax
    return false;
}

////////////////////////////////////////////////////////////////////////////////////
function regNameChange() {
    var applicant_name = document.getElementById("applicant_name").value;
    var applicant_address = document.getElementById("applicant_address").value;
    var certificate_number = document.getElementById("certificate_number").value;
    var xclass = document.getElementById("xclass").value;
    var xclass_desc = document.getElementById("xclass_desc").value;
    var old_name = document.getElementById("old_name").value;
    var old_name_addy = document.getElementById("old_name_addy").value;
    var new_name = document.getElementById("new_name").value;
    var new_name_addy = document.getElementById("new_name_addy").value;
    var agent_name = document.getElementById("agent_name").value;
    var agent_code = document.getElementById("agent_code").value;

    // alert(applicant_name);
    //starting setting some animation when the ajax starts and completes
    $("#loading")
		.ajaxStart(function () {
		    $(this).show();
		})
		.ajaxComplete(function () {
		    $(this).hide();
		});

		$.ajax({
		    url: '../Handlers/nc.ashx',
		    secureuri: false,
		    data: { x: "addNameChange", applicant_name: applicant_name, applicant_address: applicant_address, certificate_number: certificate_number, xclass: xclass, xclass_desc: xclass_desc, old_name: old_name, old_name_addy: old_name_addy, new_name: new_name, new_name_addy: new_name_addy, agent_name: agent_name, agent_code: agent_code },
		    dataType: 'json',
		    type: 'GET',
		    contentType: 'application/json',
		    success: function (data) {
		      //  alert(data);
		        window.location.href = 'namechange.aspx?px=' + data + "&sh=0&d=1&b=1";
		       // $("#ncform").hide();
		       // $("#nc_xdocs").show();
		    },
		    error: function (data, status, e) {

		    }
		});     // end $.ajax
    return false;
}

////////////////////////////////////////////////////////////////////////////////////
function regRenewal() {
    var applicant_name = document.getElementById("applicant_name").value;
    var applicant_address = document.getElementById("applicant_address").value;
    var prevDate = document.getElementById("prevDate").value;
    var certno = document.getElementById("certno").value;
    var trademark = document.getElementById("trademark").value;
    var selectTmDesc = document.getElementById("selectTmDesc").value;
    var xclass = document.getElementById("xclass").value;
    var xclass_description = document.getElementById("xclass_description").value;
    var agent_name = document.getElementById("agent_name").value;
    var agent_code = document.getElementById("agent_code").value;

    // alert(applicant_name);
    //starting setting some animation when the ajax starts and completes
    $("#loading")
		.ajaxStart(function () {
		    $(this).show();
		})
		.ajaxComplete(function () {
		    $(this).hide();
		});

    $.ajax({
        url: '../Handlers/rwl.ashx',
        secureuri: false,
        data: { x: "addRenewal", applicant_name: applicant_name, applicant_address: applicant_address, prevDate: prevDate, certno: certno, trademark: trademark, selectTmDesc: selectTmDesc, xclass: xclass, xclass_description: xclass_description, agent_name: agent_name, agent_code: agent_code },
        dataType: 'json',
        type: 'GET',
        contentType: 'application/json',
        success: function (data) {
            window.location.href = 'renewal.aspx?px=' + data + "&sh=0&d=1&b=1";
            // if (selectTmDesc != 2) { $("#logopic").show(); }
        },
        error: function (data, status, e) {

        }
    });    // end $.ajax
    return false;
}


////////////////////////////////////////////////////////////////////////////////////
function rLm() {
//alert("Dest");
    var xname = document.getElementById("name").value;
    var xtelephone = document.getElementById("telephone").value;
    var xmail = document.getElementById("email").value;
    var xidtype = document.getElementById("selectIdType").value;
    var xidnum = document.getElementById("idnumber").value;
    var xpword = document.getElementById("pword").value;
    var xpword2 = document.getElementById("pword2").value;
    //var pic = document.getElementById("pic").value;
    //starting setting some animation when the ajax starts and completes
    $("#loading")
		.ajaxStart(function () {
		    $(this).show();
		})
		.ajaxComplete(function () {
		    $(this).hide();
		});

    $.ajaxFileUpload({
        url: 'Handlers/regLm.ashx',
        secureuri: false,
        fileElementId: 'pic',
        data: { xname: xname, xtelephone: xtelephone, xmail: xmail, xidtype: xidtype, xidnum: xidnum, xpword: xpword},
        dataType: 'json',
        type: 'POST',
        contentType: 'application/json',
        success: function (data) {
        //alert(data.msg);
            $('#statMessage').html("<div id='message' style='text-align:center;'></div>");
            $('#message').html("<h3>THE DATA HAS BEEN ADDED!</h3>")
        .append("<p>SUCCESSFULLY!!</p>").hide()
        .fadeIn(3000, function () {
            $('#message').append("<br/><a href='a_pic.php'><img id='checkmark' src='images/check.png' /></a>").fadeOut(5000, function () { });
            $("#upBio").show();
            $("#table2").hide();
        });

        },
        error: function (data, status, e) {
            // alert(e);
            $('#statMessage').html("<div id='message' style='text-align:center;'></div>");
            $('#message').html("<h3>THE PICTURE HAS NOT BEEN ADDED!</h3>")
        .append("<p>PLEASE TRY AGAIN LATER!!</p>")
        .hide()
        .fadeIn(1500, function () {
            $('#message').append("<br/><a href='a_pic.php'><img id='checkmark' src='images/delete.png' /></a>").fadeOut(2000, function () { window.location.href = 'a_pic.php' });
        });
        }
    });  // end $.ajax


    return false;

}

//////////////////////////////////////////////////////////////////////

