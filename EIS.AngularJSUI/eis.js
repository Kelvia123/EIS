$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("active");
});

var appEIS = angular.module("appEIS", ["ngRoute", "angularUtils.directives.dirPagination", "ngCookies"]);

appEIS.config(function ($routeProvider, $httpProvider) {
    $httpProvider.interceptors.push('myHttpInterceptor');
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
                    $rootScope.EmpSignIn = $cookies.get("EmpSignIn");

                    $location.path('/Login');
                }
            }
        });
    $routeProvider.otherwise({ redirectTo: '/Home' });
});

appEIS.run(function($rootScope, $cookies, $http) {
    if ($cookies.get("Auth") == null) {
        $cookies.put("Auth", "false");
    }

    $rootScope.Auth = $cookies.get("Auth");

    // This is the API Key that's assigned by Service Provider, namely the server side
    // Anyone who sends an API request without a Token, namely not from a Client known by the server, will be reject.
    $http.defaults.headers.common['my_Token'] = "123456789";
});

appEIS.factory('myHttpInterceptor', function($q, $window) {
    return {
      response: function(response) {
          return response;
      },
      responseError: function(response) {
          if (response.status == 500) {
              $window.alert(response.statusText);
          }
          return $q.reject(response);
      }
    };
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

appEIS.controller('appEISController', function ($rootScope, $location, utilityService, $cookies) {

     $rootScope.$on('$routeChangeStart', function(event, next, current) {
         var guestRoutes = ['/Home', '/Login', '/RecoverPassword'];
         var userRoutes = ['/Home', '/Logout', '/EmployeeProfile/:EmployeeId?'];
         var adminRoutes = ['/Home', '/Logout', '/EmployeeProfile/:EmployeeId?', '/EmployeeManagement'];

         //If user hasn't login and try to get access to routes that's not a guest Route
         if ($rootScope.Auth == 'false' && $.inArray(next.$$route.originalPath, guestRoutes) == -1) {
             $location.path('/Login');
         }
         //If user already login
         if ($rootScope.Auth == 'true') {
             $rootScope.EmpSignIn = $cookies.getObject("EmpSignIn");
             var role = $rootScope.EmpSignIn.Role.RoleCode;

             // If user try to get access to route that's not a regular user Route
             if (role == 'U' && $.inArray(next.$$route.originalPath, userRoutes) == -1) {
                 $location.path('/Home');
             }
             // If admin try to get access to route that's not in adminRoutes collection
             if (role == 'A' && $.inArray(next.$$route.originalPath, adminRoutes) == -1) {
                 $location.path('/Home');
             }

             utilityService.getFile("http://localhost:4676/api/Upload/", $rootScope.EmpSignIn.EmployeeId)
                 .then(function(result) {
                     $rootScope.imgSideBar = result;
                 });
         }
     });
});
