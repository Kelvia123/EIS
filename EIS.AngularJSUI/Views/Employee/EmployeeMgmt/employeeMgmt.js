(function (undefined) {

    appEIS.factory('employeeMgmtService',
        function($http) {
            var empMgmtObj = {};

            empMgmtObj.getAll = function() {
                var employees;

                employees = $http({ method: 'Get', url: 'http://localhost:4676/api/Employee' })
                    .then(function(result) {
                        return result.data;
                    });

                return employees;
            };

            empMgmtObj.createEmployee = function(emp) {
                var employee;

                employee = $http({ method: 'Post', url: 'http://localhost:4676/api/Employee', data: emp })
                    .then(
                        function(response) {
                            return response.data;
                        },
                        function(error) {
                            return error.data;
                        }
                    );

                return employee;
            }

            return empMgmtObj;
        });

    appEIS.controller('employeeMgmtController', function ($scope, employeeMgmtService, utilityService) {

        employeeMgmtService.getAll().then(function(result) {
            $scope.employees = result;
        });

        $scope.Sort = function(col) {
            $scope.key = col;
            $scope.AscOrDesc = !$scope.AscOrDesc;
        };

        $scope.CreateEmployee = function (Emp, IsValid) {

            if (IsValid) {
                Emp.Password = utilityService.randomPassword();

                employeeMgmtService.createEmployee(Emp).then(function (result) {
                    if (result.ModelState == null) {
                        $scope.Msg = "You have successfully created " + result.EmployeeId;
                        $scope.showAlert = true;
                        $scope.serverErrorMsgs = null;

                        employeeMgmtService.getAll().then(function(result) {
                            $scope.employees = result;
                        });

                        utilityService.slideUpAlert();
                    } else {
                        $scope.serverErrorMsgs = result.ModelState;
                    }
                });
            }
        }
    });

})();