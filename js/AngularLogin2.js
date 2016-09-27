var app = angular.module('myModule', []);



var serviceBaseCld = "http://45.40.139.163/EinaoTestEnvironment.CLD";
//var serviceBaseCld = "http://localhost:49703";

app.controller('myController', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {

    $scope.BranchCollect = [];
    $scope.itemsPerPage = 10;
    $scope.currentPage = 0;
    $scope.items = [];
    var url3 = 'http://ipo.cldng.com/Handlers/GetRegistration2.ashx';

    // var url3 = ' http://localhost:21936/home/GetAgent';

    $(document).ready(function () {
        var xname = $("input#xname").val();
        var xname2 = $("input#xname2").val();
        var xname3 = $("input#vamount").val();
        var xname4 = $("input#vtransactionid").val();
        var xname5 = $("input#vtype").val();
        

        

        

        //alert(xname2)
       

        if (xname == "Success") {


            swal({
                title: "PAYMENT SUCCESS",
                text: "Your Payment was Successful and an invoice has been sent to your email . Kindly proceed to Form now",
                type: "success",
                showCancelButton: false,
                confirmButtonColor: "#DD6B55", confirmButtonText: "PROCEED!",
                cancelButtonText: "No!",
                closeOnConfirm: true,
                closeOnCancel: true
            },
function (isConfirm) {
    if (isConfirm) {

        if (xname5 == "Name") {
            doUrlPost(serviceBaseCld +"/admin/tm/Change_ApplicantName.aspx", xname2, xname3, xname4)
        }

        if (xname5 == "Address") {
            doUrlPost(serviceBaseCld + "/admin/tm/Change_ApplicantAddress.aspx", xname2, xname3, xname4)
        }

        if (xname5 == "Agent") {
            doUrlPost(serviceBaseCld + "/admin/tm/Change_ApplicantAgent.aspx", xname2, xname3, xname4)
        }

        if (xname5 == "Rectification") {
            doUrlPost(serviceBaseCld + "/admin/tm/Change_Rectification.aspx", xname2, xname3, xname4)
        }
       

        if (xname5 == "Assignment") {
            doUrlPost(serviceBaseCld + "/admin/tm/Change_Assignment2.aspx", xname2, xname3, xname4)
        }

        if (xname5 == "Assignment2") {
            doUrlPost( serviceBaseCld + "/admin/tm/Change_Assignment3.aspx", xname2, xname3, xname4)
        }
       
        if (xname5 == "Renewal") {
            doUrlPost(serviceBaseCld + "/admin/tm/Change_Renewal.aspx", xname2, xname3, xname4)
        }

        if (xname5 == "TradeMarkAmendment") {
            doUrlPost(serviceBaseCld + "/admin/tm/x_unit/edit_apps4.aspx", xname2, xname3, xname4)
        }

       
       


    } else {
        swal("Cancelled", "Action Canceled :)", "error");
    }
});

        }


    });



    $scope.$on('$viewContentLoaded', function () {



    });



    //When you have entire dataset



}]);











function doUrlPost(x_url, transID,vamount,vtranid) {


    postwith(x_url, {
        transID: transID, vamount: vamount, vtranid: vtranid
    });
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
