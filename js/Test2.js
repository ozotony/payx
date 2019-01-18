var app = angular.module('myModule', ['ngRoute', 'ngModal', 'angular-datepicker', 'smart-table']);
app.filter('offset', function () {
    return function (input, start) {
        start = parseInt(start, 10);
        return input.slice(start);
    };
});



app.factory('dataFactory', ['$http', '$q', function ($http, $q) {

    var urlBase = '/api/customers';
    var dataFactory = {};

    dataFactory.checkemail = function (vemail) {
        var deferred = $q.defer();
        var url7 = 'http://5.77.54.44:8080/Handlers/EmailCount.ashx';
        var vresult = true;
        // var kkk = $('#VEMAIL').val();
        var kkk = vemail;

        var AgentsData = {
            Email: kkk

        }

        var formData = new FormData();

        formData.append("vv", JSON.stringify(AgentsData));

        $http.post(url7, formData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
            .success(function (response) {

                var dd = parseInt(response);

                if (dd > 0) {

                    vresult = true;

                    deferred.resolve(true);


                }
                else {
                    vresult = false;

                    deferred.resolve(false);

                }

            })
            .error(function (err, status) {

                deferred.reject(err);

            });

        return deferred.promise;
    };



    return dataFactory;
}]);

app.directive('jqdatepicker', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            element.datepicker({
                dateFormat: 'mm/dd/yy',
                onSelect: function (date) {
                    //  scope.date = date;
                    // scope.ngModelCtrl = date;

                    ngModelCtrl.$setViewValue(date);
                    ngModelCtrl.$render();
                    scope.$apply();

                }
            });
        }
    };
});



var directiveId = 'ngMatch';
app.directive(directiveId, ['$parse', function ($parse) {

    var directive = {
        link: link,
        restrict: 'A',
        require: '?ngModel'
    };
    return directive;

    function link(scope, elem, attrs, ctrl) {
        // if ngModel is not defined, we don't need to do anything
        if (!ctrl) return;
        if (!attrs[directiveId]) return;

        var firstPassword = $parse(attrs[directiveId]);

        var validator = function (value) {
            var temp = firstPassword(scope),
                v = value === temp;
            ctrl.$setValidity('match', v);
            return value;
        }

        ctrl.$parsers.unshift(validator);
        ctrl.$formatters.push(validator);
        attrs.$observe(directiveId, function () {
            validator(ctrl.$viewValue);
        });

    }
}]);
app.run(function ($rootScope) {
    $rootScope.vhide = true;
    $rootScope.dd1 = "";

    $rootScope.dd2 = "";
    //$rootScope.Agent_Name = "";

    //$rootScope.Sys_ID = "";

    //$rootScope.greeting = "";
})


app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.when('/', {
        templateUrl: 'Index2.html',
        controller: 'myController',
        css: 'http://localhost:27280/stylesheets/main.css'
    });
    $routeProvider.when('/About', {
        templateUrl: 'About.html',
        controller: 'myController',
    });

    $routeProvider.when('/logout', {
        templateUrl: 'Index2.html',
        controller: 'myController5',
    });

    $routeProvider.when('/Service', {
        templateUrl: 'Services.html',
        controller: 'myController',
    });

    $routeProvider.when('/Online_Service', {
        templateUrl: 'Online_Service.html',
        controller: 'myController',
    });
    $routeProvider.when('/Accreditation', {
        templateUrl: 'Accreditation.html',
        controller: 'myController',
    });

    $routeProvider.when('/Contact', {
        templateUrl: 'Contact.html',
        controller: 'myController',
    });

    $routeProvider.when('/ApprovedPayment', {
        templateUrl: 'ApprovedPayment.html',
        controller: 'myController2',
    });

    $routeProvider.when('/approvedbranchcollect', {
        templateUrl: 'approvedbranchcollect.html',
        controller: 'myController3',
    });

    $routeProvider.when('/approvedbranchcollect/:Id', {
        templateUrl: 'approvedbranchcollect.html',
        controller: 'myController3',
    });

    $routeProvider.when('/DashBoard', {
        templateUrl: 'DashBoard.html',
        controller: 'myController2',
    });



    $routeProvider.when('/Register', {
        templateUrl: 'Register.html',
        controller: 'myController4',
    });

    $routeProvider.when('/Register/:Id', {
        templateUrl: 'About_Registry.html',
        controller: 'myController4',
    });

    $routeProvider.when('/Management', {
        templateUrl: 'Management.html',
        controller: 'myController',
    });

    $routeProvider.when('/GetAgent', {
        templateUrl: 'GetAgent.html',
        controller: 'myController6',
    });

    $routeProvider.when('/Trademark', {
        templateUrl: 'Trademark.html',
        controller: 'myController',
    });

    $routeProvider.when('/Patent', {
        templateUrl: 'Patent.html',
        controller: 'myController',
    });

    $routeProvider.when('/Design', {
        templateUrl: 'Design.html',
        controller: 'myController',
    });


    $routeProvider.when('/About_Registry', {
        templateUrl: 'About_Registry.html',
        controller: 'myController',
    });

    $routeProvider.when('/Preliminary_Advice', {
        templateUrl: 'Preliminary_Advice.html',
        controller: 'myController',
    });
}]);
//var scope = $rootScope;


//app.config(['$routeProvider', 
//       function ($routeProvider) {
//           $routeProvider.
//               when('/', {
//                   templateUrl: 'Index2.html',
//                   controller: 'myController',
//                   data: {
//                       animationConf: {
//                           fallback: 'slide'
//                       }
//                   }
//               }).
//               when('/About', {
//                   templateUrl: 'About.html',
//                   controller: 'myController',
//                   data: {
//                       animationConf: {
//                           fallback: 'slide'
//                       }
//                   }
//               }).
//               otherwise({
//                   redirectTo: '/'
//               });
//       }]);



app.controller('myController', ['$scope', '$http', '$rootScope', 'authService2', '$location', '$q', function ($scope, $http, $rootScope, authService2, $location, $q) {




    $scope.$on('$viewContentLoaded', function () {



        $('#homepix').innerfade({
            speed: 1500,
            timeout: 5000,
            type: 'random',
            containerheight: '275px'
        });

        $('#slider1').bxSlider({
            mode: 'vertical',
            auto: true,
            pager: true
        });
        $rootScope.vhide = true;


        var message = new Date()
        var h = message.getHours()
        if ((h < 12) && (h >= 6)) { $rootScope.greeting = "Good Morning"; }
        if ((h >= 12) && (h < 18)) { $rootScope.greeting = "Good Afternoon"; }
        if ((h >= 18) && (h <= 23)) { $rootScope.greeting = "Good Evening"; }
        //Here your view content is fully loaded !!
    });


    $scope.add6 = function () {

        $location.path("/Register")

    }

    if (Session.get("vlogin2") == true) {



        $scope.vlogin2 = true;
        $scope.vlogin = false;
        $scope.VEmail = "";

    }

    else {

        $scope.vlogin2 = false;
        $scope.vlogin = true;
    }


    $scope.Loggout = function () {

        //Session.clear();
        //$scope.vlogin2 = false;
        //$scope.vlogin = true;

    }

    $scope.add10 = function () {



        //   swal("PAGE UNDER CONSTRUCTION!", "CONSTRUCTION");

        $location.path("/GetAgent")

    }

    $scope.add2 = function () {
        Session.clear();
        $scope.vlogin2 = false;
        $scope.vlogin = true;





    }


    $scope.EditRow = function () {
        $scope.VEmail = "";
        $scope.dialogShown = true;

    }
    $scope.add3 = function (tt) {
        var SponsData = {


            email: tt,
            xpass: "",
            request: 'vlogin3'


        };

        var formData = new FormData();



        formData.append("vv", JSON.stringify(SponsData));

        swal({
            title: "Are you sure?",
            text: "This Action Will Reset Your Password",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55", confirmButtonText: "Yes, Reset it!",
            cancelButtonText: "No, cancel plx!",
            closeOnConfirm: true,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $.ajax({
                        type: "POST",

                        url: 'http://5.77.54.44:8080/Handlers/a_login2.ashx',
                        // url: 'http://localhost:4556/Handlers/a_login2.ashx',
                        data: formData,

                        contentType: false,
                        processData: false,
                        //Convert the Observable Data into JSON
                        dataType: 'json',
                        success: function (data) {

                            if (data != "false") {


                                swal("Your Password Reset Successfully", "success");


                                $scope.dialogShown = false;


                            }

                            else {

                                swal("Your Password Reset Not Successfully", "error");

                            }


                        },
                        error: function (ee) {

                            //alert(ee);
                        }
                    });


                } else {
                    swal("Cancelled", "Action Canceled :)", "error");
                }
            });




    }

    $scope.add = function () {

        var ddd = authService2;


        var SponsData = {


            email: $scope.Email,
            xpass: $scope.Password,
            request: 'vlogin'


        };






        var formData = new FormData();



        formData.append("vv", JSON.stringify(SponsData));

        $.ajax({
            type: "POST",

            url: 'http://5.77.54.44:8080/Handlers/a_login2.ashx',
            data: formData,

            contentType: false,
            processData: false,
            //Convert the Observable Data into JSON
            dataType: 'json',
            success: function (data) {

                if (data != "false") {

                    $scope.vlogin2 = true;
                    $scope.vlogin = false;

                    Session.set("vlogin2", true);
                    Session.set("vname", data.Firstname + " " + data.Surname);
                    //  $rootScope.Agent_Name = Session.get("vname");
                    $rootScope.Agent_Name = data.Firstname + " " + data.Surname;

                    Session.set("vlogin5", "true");
                    Session.set("Sys_ID", data.Sys_ID);
                    $rootScope.Sys_ID = data.Sys_ID;



                    var dd2 = "";


                    IpoTradeMarks($scope.Email, $scope.Password);

                    //authService2.Encrypt3($scope.Email).then(function (data) {

                    //    $rootScope.dd1 = data.data;

                    //    authService2.Encrypt3($scope.Password).then(function (data) {

                    //        $rootScope.dd2 = data.data;

                    //        window.location.href = "http://localhost:4556/a_login.aspx?x1=" + $rootScope.dd1 + "&x2=" + $rootScope.dd2;



                    //    });




                    //});




                    //  var pp = authService2.Encrypt3($scope.Email);

                    //  var pp2 = authService2.Encrypt3($scope.Email);

                    //       authService2.Encrypt3($scope.Email).then(function (response) {

                    //           dd1 = response;


                    //       },
                    //function (response) {
                    //    var errors = [];

                    //});


                    //         authService2.Encrypt3($scope.Password).then(function (response) {
                    //             dd2 = response;


                    //         },
                    //function (response) {
                    //    var errors = [];

                    //});



                    //   alert($scope.Agent_Name)
                    //	window.open("DashBoard.html");



                    //   $location.path("http://localhost:4556/a_login.aspx?x1=" + dd1 + "&x2=" + dd2);
                    //  $location.path("/DashBoard")

                    //  $scope.$apply()
                    // window.location.reload();
                }

                else {
                    // alert("Invalid Username /Password")



                    swal("ERROR...", "Unauthorized Access!", "error");
                    Session.set("vlogin5", "false");
                    $scope.vlogin2 = false;
                    $scope.vlogin = true;
                }

                //   self.availablesponsor(data);
                //self.EmpNo(data.EmpNo);
                //alert("The New Employee Id :" + self.EmpNo());
                //GetEmployees();
            },
            error: function (ee) {

                //alert(ee);
            }
        });


        //$http({
        //    method: 'POST',
        //    url: 'http://localhost:4556/Handlers/a_login2.ashx',
        //    data: formData,
        //    contentType: false,
        //    processData: false,
        //    //Convert the Observable Data into JSON
        //    dataType: 'json'
        //}).success(function (data, status, headers, config) {
        //    alert("sucess")
        //    $scope.states = data;
        //}).error(function (data, status, headers, config) {
        //    alert("Error")
        //    $scope.message = 'Unexpected Error';
        //});

    }
    //When you have entire dataset



}]);



app.controller('myController2', function ($scope, $http, $rootScope) {




    $scope.$on('$viewContentLoaded', function () {


        $('#homepix').innerfade({
            speed: 1500,
            timeout: 5000,
            type: 'random',
            containerheight: '275px'
        });
        $rootScope.vhide = false;

        // alert($rootScope.Sys_ID)

        //Here your view content is fully loaded !!
    });







});






app.controller('myController3', function ($scope, $http, $rootScope, $routeParams) {

    $scope.BranchCollect = [];
    $scope.itemsPerPage = 20;
    $scope.currentPage = 0;
    $scope.items = [];

    $rootScope.Agent_Name = Session.get("vname");

    if ($routeParams.Id != null) {


        $rootScope.Sys_ID = "CLD/RA/" + $routeParams.Id;

        Session.set("Sys_ID", $rootScope.Sys_ID);

        $scope.$apply();
    }

    else {
        $rootScope.Sys_ID = Session.get("Sys_ID");

    }



    $scope.EditRow = function (emp) {
        // alert(emp.TransactionID)

        if (emp.paymentcode == "T002") {

            //  NonGenericTradeMarks(data.TransactionID, data.ItemAmount, Session.get("Sys_ID"), 'cbt', data.CustomerFirstname, applicantemail, applicantpnumber, agentname, agentemail, agentpnumber, product_title, item_code);

            NonGenericTradeMarks(emp.TransactionID, emp.ItemAmount, Session.get("Sys_ID"), 'cbt', emp.CustomerFirstname, emp.ApplicantEmail, emp.ApplicantPhone, emp.Agent_name, emp.ApplicantPhone, emp.ApplicantPhone, emp.ItemDescription, "T002");
        }


        if (emp.paymentcode.substring(0, 1) == "T" && emp.paymentcode != "T002") {

            //  NonGenericTradeMarks(data.TransactionID, data.ItemAmount, Session.get("Sys_ID"), 'cbt', data.CustomerFirstname, applicantemail, applicantpnumber, agentname, agentemail, agentpnumber, product_title, item_code);

            GenericTradeMarks(emp.TransactionID, emp.ItemAmount, Session.get("Sys_ID"), 'cbt', emp.CustomerFirstname, emp.ApplicantEmail, emp.ApplicantPhone, emp.Agent_name, emp.ApplicantEmail, emp.ApplicantPhone, emp.ItemDescription, emp.paymentcode);
        }


        if (emp.paymentcode == "P003") {

            //  NonGenericTradeMarks(data.TransactionID, data.ItemAmount, Session.get("Sys_ID"), 'cbt', data.CustomerFirstname, applicantemail, applicantpnumber, agentname, agentemail, agentpnumber, product_title, item_code);

            P003Function(emp.TransactionID, emp.ItemAmount, Session.get("Sys_ID"), 'cbt', emp.CustomerFirstname, emp.ApplicantEmail, emp.ApplicantPhone, emp.Agent_name, emp.ApplicantPhone, emp.ApplicantPhone, emp.ItemDescription, emp.paymentcode);
        }

        if (emp.paymentcode.substring(0, 1) == "P" && emp.paymentcode != "P003") {

            //  NonGenericTradeMarks(data.TransactionID, data.ItemAmount, Session.get("Sys_ID"), 'cbt', data.CustomerFirstname, applicantemail, applicantpnumber, agentname, agentemail, agentpnumber, product_title, item_code);

            GeneralPostPatent(emp.TransactionID, emp.ItemAmount, Session.get("Sys_ID"), 'cbt', emp.CustomerFirstname, emp.ApplicantEmail, emp.ApplicantPhone, emp.Agent_name, emp.ApplicantPhone, emp.ApplicantPhone, emp.ItemDescription, emp.paymentcode);
        }

        if (emp.paymentcode.substring(0, 1) == "D") {

            //  NonGenericTradeMarks(data.TransactionID, data.ItemAmount, Session.get("Sys_ID"), 'cbt', data.CustomerFirstname, applicantemail, applicantpnumber, agentname, agentemail, agentpnumber, product_title, item_code);

            GeneralPostDesign(emp.TransactionID, emp.ItemAmount, Session.get("Sys_ID"), 'cbt', emp.CustomerFirstname, emp.ApplicantEmail, emp.ApplicantPhone, emp.Agent_name, emp.ApplicantPhone, emp.ApplicantPhone, emp.ItemDescription, emp.paymentcode);
        }
    };


    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };

    $scope.prevPageDisabled = function () {
        return $scope.currentPage === 0 ? "disabled" : "";
    };

    $scope.pageCount = function () {
        return Math.ceil($scope.BranchCollect.length / $scope.itemsPerPage) - 1;
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pageCount()) {
            $scope.currentPage++;
        }
    };

    $scope.nextPageDisabled = function () {
        return $scope.currentPage === $scope.pageCount() ? "disabled" : "";
    };

    $scope.Update2 = function () {
        $rootScope.vhide = false;
        var url3 = 'http://5.77.54.44:8080/Handlers/BranchCollect3.ashx';

        var SponsData3 = {


            Agent_Code: $rootScope.Sys_ID,
            TransactionId: $scope.searchText


        };
        var formData = new FormData();

        formData.append("vv", JSON.stringify(SponsData3));
        //ajaxindicatorstart('Loading  Record.. please wait..');
        $.ajax({
            type: "POST",
            url: url3,
            data: formData,

            contentType: false,
            processData: false,
            //Convert the Observable Data into JSON
            dataType: 'json',
            success: function (data) {
                //self.paymentData(data);
                $scope.BranchCollect = data;

                $scope.$apply();
                //ajaxindicatorstop();


                //   self.availablesponsor(data);
                //self.EmpNo(data.EmpNo);
                //alert("The New Employee Id :" + self.EmpNo());
                //GetEmployees();
            },
            error: function (ee) {
                //ajaxindicatorstop();
                //alert(ee);
            }
        });


    }

    $scope.$on('$viewContentLoaded', function () {

        $rootScope.vhide = false;
        var url3 = 'http://5.77.54.44:8080/Handlers/BranchCollect3.ashx';

        var SponsData3 = {


            Agent_Code: $rootScope.Sys_ID,
            TransactionId: ""


        };
        var formData = new FormData();

        formData.append("vv", JSON.stringify(SponsData3));
        ajaxindicatorstart('Loading  Record.. please wait..');
        $.ajax({
            type: "POST",
            url: url3,
            data: formData,

            contentType: false,
            processData: false,
            //Convert the Observable Data into JSON
            dataType: 'json',
            success: function (data) {
                //self.paymentData(data);
                $scope.BranchCollect = data;

                $scope.$apply();
                ajaxindicatorstop();


                //   self.availablesponsor(data);
                //self.EmpNo(data.EmpNo);
                //alert("The New Employee Id :" + self.EmpNo());
                //GetEmployees();
            },
            error: function (ee) {
                ajaxindicatorstop();
                //alert(ee);
            }
        });

        // alert($rootScope.Sys_ID)

        //Here your view content is fully loaded !!
    });







});

app.controller('myController4', function ($scope, $http, $rootScope, $routeParams, dataFactory) {
    $scope.vemail = "";
    $scope.processing = false;
    if ($routeParams.Id != null) {


        var vk = $routeParams.Id;
        ajaxindicatorstart('Loading  Record.. please wait..');

        var serviceBase = 'http://5.77.54.44:8080/Handlers/GetRegistration.ashx';




        var Encrypt = {
            vid: vk
        }

        $http({
            method: 'POST',
            url: serviceBase,
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: Encrypt,
            headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8;' }
        })
            .success(function (response) {

                IpoTradeMarks2(response.Email, response.Firstname, response.CompanyAddress, response.xid, response.PhoneNumber)
                ajaxindicatorstop();

            })
            .error(function (response) {
                ajaxindicatorstop();
            });


    }


    $scope.$on('$viewContentLoaded', function () {

        $rootScope.vhide = true;
        $scope.items = ['Corporate'];

        $('#homepix').innerfade({
            speed: 1500,
            timeout: 5000,
            type: 'random',
            containerheight: '275px'
        });
        // alert($rootScope.Sys_ID)

        //Here your view content is fully loaded !!
    });





    $scope.change = function () {
        var url7 = 'http://5.77.54.44:8080/Handlers/EmailCount.ashx';

        var kkk = $('#VEMAIL').val();

        var AgentsData = {
            Email: kkk

        }

        var formData = new FormData();

        formData.append("vv", JSON.stringify(AgentsData));

        $http.post(url7, formData, {
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        })
            .success(function (response) {

                var dd = parseInt(response);

                if (dd > 0) {
                    swal("Email Already Exist")

                    $scope.vemail = "";


                }
                else {


                }

            })
            .error(function () {

                swal("error")
            });

    };

    $scope.submitForm = function (isValid) {
        $scope.processing = true;
        // check to make sure the form is completely valid

        var kkk = $('#VEMAIL').val();



        // var kkk2 = checkemail(kkk, $http);





        var error = $scope.userForm.$error;
        angular.forEach(error.pattern, function (field) {
            if (field.$invalid) {
                var fieldName = field.$name;
                alert(fieldName)
            }
        });

        if (isValid) {
            ajaxindicatorstart('Submitting   Record.. please wait..');
            var formData = new FormData();



            var totalFiles = document.getElementById("cac").files.length;
            if (totalFiles == 0) {
                alert("Upload File")
                //  self.cac("");

                return;

            }

            else {

                var ext = $('#cac').val().split('.').pop().toLowerCase();

                if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) > -1) {
                    alert('invalid extension!');
                    return;
                }

            }
            var totalFiles2 = document.getElementById("Letter_Intro").files.length;

            if (totalFiles2 == 0) {
                alert("Upload File")
                //  self.loi("");

                return;

            }

            else {

                var ext = $('#Letter_Intro').val().split('.').pop().toLowerCase();

                if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) > -1) {
                    alert('invalid extension!');
                    return;
                }

            }

            var totalFiles3 = document.getElementById("passport").files.length;

            if (totalFiles3 == 0) {
                alert("Upload File")
                //   self.passport("");
                ajaxindicatorstop();
                return;

            }

            else {

                var ext = $('#passport').val().split('.').pop().toLowerCase();

                if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                    alert('invalid extension!');
                    ajaxindicatorstop();
                    return;
                }

            }

            for (var i = 0; i < totalFiles; i++) {
                var file = document.getElementById("cac").files[i];



                formData.append("FileUpload", file);
            }

            for (var i = 0; i < totalFiles2; i++) {
                var file = document.getElementById("Letter_Intro").files[i];

                formData.append("FileUpload2", file);
            }

            for (var i = 0; i < totalFiles3; i++) {
                var file = document.getElementById("passport").files[i];

                formData.append("FileUpload3", file);
            }





            var AgentsData = {


                AccountType: $scope.AccountType,
                FirstName: $scope.firstname,
                Surname: $scope.surname,
                Nationality: $scope.country,
                State: $scope.state,
                dob: $scope.date,
                CompName: $scope.CompName,
                CompAddress: $scope.CompAddress,
                CompEmail: $scope.CompEmail,
                CompPhone: $scope.CompPhone,
                CompPerson: $scope.CompPerson,
                ContactPhone: $scope.ContactPhone,
                DobIncorp: $scope.DobIncorp,
                Email: $scope.vemail,

                password: $scope.password



            };

            formData.append("vv", JSON.stringify(AgentsData));
            //    ajaxindicatorstart('Submitting Record.. please wait..');
            var url7 = 'http://5.77.54.44:8080/Handlers/SaveAgent.ashx';
            //$.ajax({
            //    type: "POST",
            //    url: url7,
            //    data: formData,

            //    contentType: false,
            //    processData: false,
            //    //Convert the Observable Data into JSON
            //    dataType: 'json',
            //    success: function (data) {
            //        //   ajaxindicatorstop();
            //        swal("Record Added Successfully")

            //    },
            //    error: function (ee) {
            //        //  ajaxindicatorstop();

            //        swal(ee.responseText)

            //    }
            //});
            var url9 = "http://5.77.54.44:8080/Handlers/SaveAgent.ashx";

            // var url9 = "http://localhost:4556/Handlers/SaveAgent.ashx";

            var dataObj = {
                vv: JSON.stringify(AgentsData),
                employees: $scope.employees,
                headoffice: $scope.headoffice
            };


            var custs = dataFactory.checkemail(kkk).then(function (response) {

                var cust = response;

                if (cust) {
                    ajaxindicatorstop();
                    swal("Email Already Exist")
                    $scope.processing = false;
                    return;

                }

                else {

                    $http.post(url9, formData, {
                        transformRequest: angular.identity,
                        headers: { 'Content-Type': undefined }
                    })
                        .success(function (response) {

                            ajaxindicatorstop();

                            swal("You have successfully submitted your form,please check your email for next steps")
                            $scope.processing = false;

                        })
                        .error(function () {
                            ajaxindicatorstop();
                            swal("error")
                            $scope.processing = false;
                        });

                }

            }, function (response) {
                $scope.processing = false;
            });;








        }

    };

    GetCountries();
    function GetCountries() {
        $http({
            method: 'GET',
            url: 'http://5.77.54.44:8080/Handlers/Getcountry.ashx'
        }).success(function (data, status, headers, config) {
            var dd = data;
            $scope.countries = data;
        }).error(function (data, status, headers, config) {
            $scope.message = 'Unexpected Error';
        });
    }

    $scope.GetStates2 = function () {

    }


    $scope.GetStates = function () {
        var countryId = $scope.country;
        var formData = new FormData();
        formData.append("vid", countryId);
        if (countryId) {
            //$http({
            //    method: 'GET',
            //    url: 'http://localhost:4556/Handlers/GetState.ashx',
            //    data: formData,
            //    contentType: false,
            //    processData: false,
            //    //Convert the Observable Data into JSON
            //    dataType: 'json'
            //    //JSON.stringify({ vid: countryId })
            //}).success(function (data, status, headers, config) {
            //    $scope.states = data;
            //}).error(function (data, status, headers, config) {
            //    $scope.message = 'Unexpected Error';
            //});

            $.ajax({
                type: "POST",
                url: 'http://5.77.54.44:8080/Handlers/GetState.ashx',
                data: formData,

                contentType: false,
                processData: false,
                //Convert the Observable Data into JSON
                dataType: 'json',
                success: function (data) {
                    //self.paymentData(data);
                    $scope.states = data;

                    $scope.$apply();
                    //ajaxindicatorstop();


                    //   self.availablesponsor(data);
                    //self.EmpNo(data.EmpNo);
                    //alert("The New Employee Id :" + self.EmpNo());
                    //GetEmployees();
                },
                error: function (ee) {
                    //ajaxindicatorstop();
                    //alert(ee);
                }
            });
        }
        else {
            $scope.states = null;
        }
    }





});

app.controller('myController5', function ($scope, $http, $rootScope, $routeParams, $location) {


    Session.clear();
    $scope.vlogin2 = false;
    $scope.vlogin = true;
    $location.path("/")









});


app.controller('myController6', function ($scope, $http, $rootScope, $routeParams, $location) {


    $scope.$on('$viewContentLoaded', function () {

        $rootScope.vhide = true;
        $scope.items = ['Corporate'];

        $('#homepix').innerfade({
            speed: 1500,
            timeout: 5000,
            type: 'random',
            containerheight: '275px'
        });
        // alert($rootScope.Sys_ID)

        //Here your view content is fully loaded !!
    });

    $http({
        method: 'GET',
        url: 'http://5.77.54.44:8080/Handlers/GetRegistration2.ashx'

        //   url: 'http://localhost:4556/Handlers/GetRegistration2.ashx'
    }).success(function (data, status, headers, config) {
        var dd = data;
        $scope.itemsByPage = 100;
        $scope.ListAgent = data;
        $scope.displayedCollection = [].concat($scope.ListAgent);
    }).error(function (data, status, headers, config) {
        alert(data)
        $scope.message = 'Unexpected Error';
    });









});




if (JSON && JSON.stringify && JSON.parse) var Session = Session || (function () {

    // window object
    var win = window.top || window;

    // session store
    var store = (win.name ? JSON.parse(win.name) : {});

    // save store on page unload
    function Save() {
        win.name = JSON.stringify(store);
    };

    // page unload event
    if (window.addEventListener) window.addEventListener("unload", Save, false);
    else if (window.attachEvent) window.attachEvent("onunload", Save);
    else window.onunload = Save;

    // public methods
    return {

        // set a session variable
        set: function (name, value) {
            store[name] = value;
        },

        // get a session value
        get: function (name) {
            return (store[name] ? store[name] : undefined);
        },

        // clear session
        clear: function () { store = {}; },

        // dump session data
        dump: function () { return JSON.stringify(store); }

    };

})();


function doPost(transID, amt, agent, xgt, cname, agentemail, agentpnumber, applicantname, product_title) {
    postwith('http://tm.cldng.com/xindex.aspx', { transID: transID, amt: amt, agent: agent, xgt: xgt, cname: cname, agentemail: agentemail, agentpnumber: agentpnumber, applicantname: applicantname, product_title: product_title });
}

function IpoTradeMarks(email, password) {

    postwith('http://5.77.54.44:8080/a_login.aspx', {
        x3: email,
        x2: password
    });



}



function IpoTradeMarks2(email, name, address, vid, PhoneNumber) {

    postwith('http://5.77.54.44/Payx/A/m_payx.aspx', {
        //        postwith('http://payx.com.ng/A/m_payx.aspx', {

        xname: name,
        agentType: 'Agent',
        address: address,
        email: email,
        PhoneNumber: PhoneNumber,
        pwalletID: vid
    });



}


// http://tm.cldng.com/xtm/xindex.aspx and Generic Form URL : http://tm.cldng.com/xtm/gf/xindex.aspx 

function NonGenericTradeMarks(transID, amt, agt, xgt, applicantname, applicantemail, applicantpnumber, agentname, agentemail, agentpnumber, product_title, item_code) {
    postwith('http://tm.cldng.com/xind.aspx', {
        transID: transID,
        amt: amt,
        agent: agt,
        xgt: xgt,
        applicantname: applicantname,
        applicantemail: applicantemail,
        applicantpnumber: applicantpnumber,
        agentname: agentname,
        agentemail: agentemail,
        agentpnumber: agentpnumber,
        product_title: product_title,
        item_code: item_code
    });
}


function GenericTradeMarks(transID, amt, agt, xgt, applicantname, applicantemail, applicantpnumber, agentname, agentemail, agentpnumber, product_title, item_code) {
    postwith('http://tm.cldng.com/gf/xindex.aspx', {
        transID: transID,
        amt: amt,
        agt: agt,
        xgt: xgt,
        applicantname: applicantname,
        applicantemail: applicantemail,
        applicantpnumber: applicantpnumber,
        agentname: agentname,
        agentemail: agentemail,
        agentpnumber: agentpnumber,
        product_title: product_title,
        item_code: item_code
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

function GeneralPostPatent(transID, amt, agt, xgt, cname, agentemail, agentpnumber, applicantname, product_title, item_code) {
    postwith('http://pt.cldng.com/xindex.aspx', { transID: transID, amt: amt, agent: agt, xgt: xgt, agentname: cname, agentemail: agentemail, agentpnumber: agentpnumber, applicantname: applicantname, product_title: product_title, pc: item_code });

    // postwith('http://localhost:55482/xindex.aspx', { transID: transID, amt: amt, agent: agt, xgt: xgt, agentname: cname, agentemail: agentemail, agentpnumber: agentpnumber, applicantname: applicantname, product_title: product_title, pc: item_code });
}

function GeneralPostDesign(transID, amt, agt, xgt, cname, agentemail, agentpnumber, applicantname, product_title, item_code) {
    postwith('http://ds.cldng.com/xindex.aspx', { transID: transID, amt: amt, agent: agt, xgt: xgt, agentname: cname, agentemail: agentemail, agentpnumber: agentpnumber, applicantname: applicantname, product_title: product_title, pc: item_code });
}
function P003Function(transID, amt, agt, xgt, applicantname, applicantemail, applicantpnumber, agentname, agentemail, agentpnumber, product_title, item_code) {
    postwith('http://pt.cldng.com/xindex_ren.aspx', {
        transID: transID,
        amt: amt,
        agt: agt,
        xgt: xgt,
        applicantname: applicantname,
        applicantemail: applicantemail,
        applicantpnumber: applicantpnumber,
        agentname: agentname,
        agentemail: agentemail,
        agentpnumber: agentpnumber,
        product_title: product_title,
        item_code: item_code
    });
}


function ajaxindicatorstart(text) {

    if (jQuery('body').find('#resultLoading').attr('id') != 'resultLoading') {

        jQuery('body').append('<div id="resultLoading" style="display:none"><div><img src="ajax-loader.jpg"><div>' + text + '</div></div><div class="bg"></div></div>');

    }



    jQuery('#resultLoading').css({

        'width': '100%',

        'height': '100%',

        'position': 'fixed',

        'z-index': '10000000',

        'top': '0',

        'left': '0',

        'right': '0',

        'bottom': '0',

        'margin': 'auto'

    });



    jQuery('#resultLoading .bg').css({

        'background': '#000000',

        'opacity': '0.7',

        'width': '100%',

        'height': '100%',

        'position': 'absolute',

        'top': '0'

    });



    jQuery('#resultLoading>div:first').css({

        'width': '250px',

        'height': '75px',

        'text-align': 'center',

        'position': 'fixed',

        'top': '0',

        'left': '0',

        'right': '0',

        'bottom': '0',

        'margin': 'auto',

        'font-size': '16px',

        'z-index': '10',

        'color': '#ffffff'



    });



    jQuery('#resultLoading .bg').height('100%');

    jQuery('#resultLoading').fadeIn(300);

    jQuery('body').css('cursor', 'wait');

}

function ajaxindicatorstop() {

    jQuery('#resultLoading .bg').height('100%');

    jQuery('#resultLoading').fadeOut(300);

    jQuery('body').css('cursor', 'default');

}


function checkemail(vemail, vhttp) {
    var url7 = 'http://5.77.54.44:8080/Handlers/EmailCount.ashx';

    // var kkk = $('#VEMAIL').val();
    var kkk = vemail;

    var AgentsData = {
        Email: kkk

    }

    var formData = new FormData();

    formData.append("vv", JSON.stringify(AgentsData));

    vhttp.post(url7, formData, {
        transformRequest: angular.identity,
        headers: { 'Content-Type': undefined }
    })
        .success(function (response) {

            var dd = parseInt(response);

            if (dd > 0) {
                swal("Email Already Exist")

                $scope.vemail = "";

                return false;


            }
            else {

                return true;


            }

        })
        .error(function () {

            swal("error")

            return false;
        });


}
