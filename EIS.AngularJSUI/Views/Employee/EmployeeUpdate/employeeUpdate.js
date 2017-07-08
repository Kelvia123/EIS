(function (undefined) {
    appEIS.factory('employeeUpdateService', function($http) {
        var empUpdateObj = {};

        empUpdateObj.getEmpById = function (empId) {
            var Emp;

            Emp = $http({ method: 'Get', url: 'http://localhost:4676/api/Employee', params: { id: empId } })
                        .then(function(response) { return response.data; });

            return Emp;
        }

        empUpdateObj.updateEmployee = function (employee) {
            var Emp;
            Emp =  $http({ method: 'Put', url: 'http://localhost:4676/api/Employee', data: employee })
                            .then(function (response) { return response.data; },
                                  function (error) { return error.data; });

            return Emp;
        };

        return empUpdateObj;
    });

    appEIS.controller('employeeUpdateController', function ($scope, $routeParams, employeeUpdateService, utilityService) {
        
        $scope.eid = $routeParams.EmployeeId;
        employeeUpdateService.getEmpById($scope.eid)
                             .then(function(result) {
                                 $scope.Emp = result;
                                 //For DOJ's proper presentation in date input
                                 $scope.Emp.DateOfJoin = new Date($scope.Emp.DateOfJoin);
                             });

        $scope.UpdateEmployee = function(Emp, IsValid) {
            if (IsValid) {
                employeeUpdateService.updateEmployee(Emp).then(function(result) {
                        if (result.ModelState == null) {
                            $scope.Emp = result;
                            $scope.Msg = "You have successfully updated Employee with Id: " + $scope.Emp.EmployeeId;
                            $scope.showAlert = true;
                            $scope.serverErrorMsgs = "";
                            utilityService.slideUpAlert();
                        } else {
                            $scope.serverErrorMsgs = result.ModelState;
                        }
                });
            }
        };

        $scope.UploadFile = function() {
            var file = $scope.myFile;
            var uploadUrl = 'http://localhost:4676/api/Upload/';
            utilityService.uploadFile(file, uploadUrl, $scope.eid).then(function(result) {

            });
        };

        $('#profilePanel a').click(function (e) {
            e.preventDefault();
        });
    });

})();