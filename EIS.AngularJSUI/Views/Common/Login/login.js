(function (undefined) {
    appEIS.factory('loginService', function($http) {
        var loginObj = {};

        loginObj.getByEmp = function(employee) {
            var Emp;

            Emp = $http({ method: 'Post', url: 'http://localhost:4676/api/Login', data: employee })
            .then(function(response) {
                    return response.data;
                }, function(error) {
                    return error.data;
                });

            return Emp;
        };

        return loginObj;
    });

    appEIS.controller('loginController', function ($scope, $rootScope, $cookies, loginService, $location) {
        
        $scope.Login = function(emp, IsValid) {
            if (IsValid) {
                loginService.getByEmp(emp)
                    .then(function(result) {
                        if (result.ModelState == null) {

                            $scope.Emp = result;
                            $scope.serverErrorMsgs = "";

                            $cookies.put("Auth", "true");
                            $rootScope.Auth = $cookies.get("Auth");

                            $cookies.putObject("EmpSignIn", $scope.Emp);
                            $rootScope.EmpSignIn = $cookies.getObject("EmpSignIn");

                            $location.path("/");
                        } else {
                            $scope.serverErrorMsgs = result.ModelState;
                        }
                    });
            }
        }

    });

})();