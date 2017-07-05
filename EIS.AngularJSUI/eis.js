$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("active");
});

var appEIS = angular.module("appEIS", ["ngRoute", "angularUtils.directives.dirPagination"]);

appEIS.config(function($routeProvider) {
    $routeProvider.when('/Home', { templateUrl: 'Views/Common/Home/Home.html', controller: 'homeController' });   
    $routeProvider.when('/RecoverPassword', { templateUrl: 'Views/Common/RecoverPassword/RecoverPassword.html', controller: 'recoverPasswordController' });
    $routeProvider.when('/EmployeeManagement', { templateUrl: 'Views/Employee/EmployeeMgmt/EmployeeMgmt.html', controller: 'employeeMgmtController' });
    $routeProvider.when('/EmployeeProfile', { templateUrl: 'Views/Employee/EmployeeUpdate/EmployeeUpdate.html', controller: 'employeeUpdateController' });
    $routeProvider.when('/Login', { templateUrl: 'Views/Common/Login/Login.html', controller: 'loginController' });
    $routeProvider.when('/Logout', {  });
    $routeProvider.otherwise({ redirectTo: '/Home' });
});

appEIS.factory('utilityService', function() {
    var utilityObj = {};

    utilityObj.randomPassword = function() {
        return Math.random().toString(36).substr(2, 5);
    };

    utilityObj.slideUpAlert = function () {

        $("#alert").fadeTo(2000, 500).slideUp(1000);
        //$("#alert").fadeTo(2000, 500).slideUp(1000, function() {
        //    $("#alert").slideUp(1000);
        //});
    };


    return utilityObj;
});
