$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("active");
});

var appEIS = angular.module("appEIS", ["ngRoute", "angularUtils.directives.dirPagination", "ngCookies"]);

appEIS.config(function($routeProvider) {
    $routeProvider.when('/Home', { templateUrl: 'Views/Common/Home/Home.html', controller: 'homeController' });   
    $routeProvider.when('/RecoverPassword', { templateUrl: 'Views/Common/RecoverPassword/RecoverPassword.html', controller: 'recoverPasswordController' });
    $routeProvider.when('/EmployeeManagement', { templateUrl: 'Views/Employee/EmployeeMgmt/EmployeeMgmt.html', controller: 'employeeMgmtController' });
    $routeProvider.when('/EmployeeProfile/:EmployeeId?', { templateUrl: 'Views/Employee/EmployeeUpdate/EmployeeUpdate.html', controller: 'employeeUpdateController' });
    $routeProvider.when('/Login', { templateUrl: 'Views/Common/Login/Login.html', controller: 'loginController' });
    $routeProvider.when('/Logout',
        {
            resolve: {
                auth: function($rootScope, $cookies, $location) {
                    $cookies.put("Auth", "false");
                    $rootScope.Auth = $cookies.get("Auth");

                    $cookies.put("EmpSignIn", null);
                    $rootScope.EmpSingIn = $cookies.get("EmpSignIn");

                    $location.path('/Login');
                }
            }
        });
    $routeProvider.otherwise({ redirectTo: '/Home' });
});

appEIS.run(function($rootScope, $cookies) {
    if ($cookies.get("Auth") == null) {
        $cookies.put("Auth", "false");
    }

    $rootScope.Auth = $cookies.get("Auth");
});

appEIS.factory('utilityService', function($http) {
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

    utilityObj.uploadFile = function(file, uploadUrl, eId) {
        var fd = new FormData();
        fd.append('file', file);

        var img;

        img = $http({
            method: 'Post',
            url: uploadUrl + eId,
            data: fd,
            transformRequest: angular.identity,
            headers: { 'Content-Type': undefined }
        }).
        then(function (response) {

            return response.data;

        }, function (error) {

            return error.data;

        });

        return img;
    };

    utilityObj.getFile = function(getFileUrl, eId) {
        var img;

        img = $http({ method: 'Get', url: getFileUrl, params: { id: eId } }).
        then(function (response) {
                return response.data;
        });

        return img;
    };


    return utilityObj;
});

//ng-model doesn't support file, so I create a directive to bind the file to a scope variable
appEIS.directive('fileModel', function($parse) {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;

            element.bind('change', function() {
                scope.$apply(function() {
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    }
});
