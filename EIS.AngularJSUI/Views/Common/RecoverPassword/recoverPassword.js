(function (undefined) {

    appEIS.factory('recoverPasswordService', recoverPasswordService);

    function recoverPasswordService($http) {
        var api = {
            getByEmp: getByEmp
        }
        return api;

        function getByEmp(emp) {
            return $http({
                method: 'Get',
                url: 'http://localhost:4676/api/Login/RecoverPassword',
                params: {
                    empStr: JSON.stringify(emp)
                }
            }).then(function(response) {
                return response;
            }, function(error) {
                return error;
            });
        }        
    }

    appEIS.controller('recoverPasswordController', function ($scope, recoverPasswordService, utilityService) {
        $scope.RecoverPassword = function (emp, isValid) {
            if (isValid) {
                recoverPasswordService.getByEmp(emp)
                    .then(function(result) {
                        if (result.status == 200) {
                            $scope.Msg = "Your credentials have been sent to the email address successfully";
                            $scope.showAlert = true;
                            $scope.serverErrorMsgs = "";
                            utilityService.slideUpAlert();
                        } else {
                            $scope.serverErrorMsgs = result.data.ModelState;
                        }
                    });
            }
        }
    });

})();