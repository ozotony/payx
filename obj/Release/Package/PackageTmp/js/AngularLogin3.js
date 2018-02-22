var app = angular.module('myModule', ['ngCsv', 'angular-loading-bar', 'smart-table']);



//var serviceBasePayx = "http://payx.com.ng/";
var serviceBasePayx = "http://localhost:21327/";

app.controller('myController', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {

   

    $(document).ready(function () {
        //var xname = $("input#xname").val();
        //var xname2 = $("input#xname2").val();
        //var xname3 = $("input#vamount").val();
        //var xname4 = $("input#vtransactionid").val();
        //var xname5 = $("input#vtype").val();






        //alert(xname2)



    });


    $scope.Export = function () {

        var xname = $("input#fromDate").val();
        var xname2 = $("input#toDate").val();

        var formData = new FormData();

        if (xname == "") {

            alert("Select Date");
            return;

        }

        if (xname2 == "") {

            alert("Select Date");
            return;

        }

        var Encrypt = {
            vid: xname,
            vid2: xname2
        }
       
        $http({
            method: 'POST',
            url: serviceBasePayx + 'Handler/ExportToExcel.ashx',
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: Encrypt,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
        }).success(function (response) {

          //  ajaxindicatorstop();
          alasql('SELECT * INTO XLSX("Payx.xlsx",{headers:true}) FROM ?', [response]);
         // var kk = response

        alert("success")

      })
      .error(function (aa) {
          var data = aa
          // ajaxindicatorstop();
          swal("error")
      });
    }

    $scope.$on('$viewContentLoaded', function () {



    });



    //When you have entire dataset



}]);

app.controller('myController2', ['$scope', '$http', '$rootScope', function ($scope, $http, $rootScope) {
    $scope.Payment ="Interswitch";
    $scope.PaymentType = ["Interswitch", "Branchcollect"];


    $(document).ready(function () {
        //var xname = $("input#xname").val();
        //var xname2 = $("input#xname2").val();
        //var xname3 = $("input#vamount").val();
        //var xname4 = $("input#vtransactionid").val();
        //var xname5 = $("input#vtype").val();






        //alert(xname2)



    });

    $scope.Export2 = function () {

        var xname = $("input#fromDate").val();
        var xname2 = $("input#toDate").val();

        var formData = new FormData();

        if (xname == "") {

            swal("", "Select Date Range", "error");
            return;

        }

        if (xname2 == "") {

            swal("", "Select Date Range", "error");
            return;

        }

        var Encrypt = {
            vid: xname,
            vid2: xname2
        }

        if ($scope.Payment == "Interswitch") {
            $scope.displayedCollection = []
            $http({
                method: 'POST',
                url: serviceBasePayx + 'Handler/GetPayments.ashx',
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: Encrypt,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
            }).success(function (response) {


                var dd = response;

                $scope.itemsByPage = 50;

                $scope.ListAgent = response;
                $scope.displayedCollection = [].concat($scope.ListAgent);
                //  ajaxindicatorstop();
                //   alasql('SELECT * INTO XLSX("PayxTransaction.xlsx",{headers:true}) FROM ?', [response]);
                // var kk = response

                //    alert("success")

            })
          .error(function (aa) {
              var data = aa
              // ajaxindicatorstop();
              swal(" ", "error", "error")
          });

        }

        else {
            $scope.displayedCollection = []
            $http({
                method: 'POST',
                url: serviceBasePayx + 'Handler/GetPayments2.ashx',
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: Encrypt,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
            }).success(function (response) {


                var dd = response;

                $scope.itemsByPage = 50;

                $scope.ListAgent = response;
                $scope.displayedCollection = [].concat($scope.ListAgent);
                //  ajaxindicatorstop();
                //   alasql('SELECT * INTO XLSX("PayxTransaction.xlsx",{headers:true}) FROM ?', [response]);
                // var kk = response

                //    alert("success")

            })
        .error(function (aa) {
            var data = aa
            // ajaxindicatorstop();
            swal(" ", "error", "error")
        });

        }
      //  alert($scope.Payment)

    }


    $scope.Export = function () {

        var xname = $("input#fromDate").val();
        var xname2 = $("input#toDate").val();

        var formData = new FormData();

        if (xname == "") {

            alert("Select Date");
            return;

        }

        if (xname2 == "") {

            alert("Select Date");
            return;

        }

        var Encrypt = {
            vid: xname,
            vid2: xname2
        }

        if ($scope.Payment == "Interswitch") {

            $http({
                method: 'POST',
                url: serviceBasePayx + 'Handler/GetPayments.ashx',
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: Encrypt,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
            }).success(function (response) {


                //  ajaxindicatorstop();
                alasql('SELECT * INTO XLSX("PayxTransaction.xlsx",{headers:true}) FROM ?', [response]);
                // var kk = response

                alert("success")

            })
          .error(function (aa) {
              var data = aa
              // ajaxindicatorstop();
              swal(" ", "error", "error")
          });

        }

        else {
            $http({
                method: 'POST',
                url: serviceBasePayx + 'Handler/GetPayments2.ashx',
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
                data: Encrypt,
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
            }).success(function (response) {


                //  ajaxindicatorstop();
                alasql('SELECT * INTO XLSX("BranchTransaction.xlsx",{headers:true}) FROM ?', [response]);
                // var kk = response

                alert("success")

            })
       .error(function (aa) {
           var data = aa
           // ajaxindicatorstop();
           swal(" ", "error", "error")
       });
        }
    }

    $scope.$on('$viewContentLoaded', function () {



    });



    //When you have entire dataset



}]);











function doUrlPost(x_url, transID, vamount, vtranid) {


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
