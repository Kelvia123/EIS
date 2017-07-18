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

            empMgmtObj.createMultipleEmployee = function (fileName) {
                 
                return $http({
                            method: 'Post',
                            url: 'http://localhost:4676/api/Employee/CreateMultipleEmployees',
                            params: { fileName: fileName }
                        })
                        .then(function(response) {
                                return response;
                            },
                            function(error) {
                                return error;
                            });
            }

            empMgmtObj.deleteEmployeeById = function(empId) {
                var employee;

                employee = $http({ method: 'Delete', url: 'http://localhost:4676/api/Employee', params: { id: empId } })
                            .then(function(response) {
                                return response.data;
                            });

                return employee;
            }

            empMgmtObj.remindEmployeeById = function(empId, msg) {
                return $http({
                            method: 'Get',
                            url: 'http://localhost:4676/api/Employee/Remind',
                            params: { id: empId, message: msg }
                        })
                        .then(function(response) {
                            return response;
                        });
            }

            return empMgmtObj;
        });

    appEIS.controller('employeeMgmtController', function ($scope, employeeMgmtService, utilityService, $window) {

        employeeMgmtService.getAll().then(function(result) {
            $scope.employees = result;
        });

        $scope.Sort = function(col) {
            $scope.key = col;
            $scope.AscOrDesc = !$scope.AscOrDesc;
        };

        $scope.CreateEmployee = function (Emp, IsValid) {

            if (IsValid) {
                //Emp.Password = utilityService.randomPassword();
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

        $scope.CreateMultipleEmployee = function() {
            var file = $scope.myFile;
            var uploadUrl = 'http://localhost:4676/api/Upload/';

            utilityService.uploadFile(file, uploadUrl, null)
                .then(function(fileName) {
                    employeeMgmtService.createMultipleEmployee(fileName)
                        .then(function(result) {
                            if (result.status == 200) {
                                $scope.Msg = 'You have successfully created ' + result.data + 'record(s)';
                                $scope.showAlert = true;
                                $scope.serverErrorMsgs = '';
                                utilityService.slideUpAlert();

                                employeeMgmtService.getAll().then(function(result) {
                                    $scope.employees = result;
                                });

                            } else {
                                $scope.serverErrorMsgs = result.data.ModelState;
                            }
                        });
                });
        }

        $scope.DeleteEmployeeById = function (EmpId) {

            if ($window.confirm("Do you want to delete Employee with id: " + EmpId + "?")) {
                employeeMgmtService.deleteEmployeeById(EmpId)
                    .then(function (result) {
                        if (result.ModelState == null) {
                            $scope.Msg = "You have successfully deleted Employee with Id: " + EmpId;
                            $scope.showAlert = true;
                            utilityService.slideUpAlert();

                            employeeMgmtService.getAll().then(function (result) {
                                $scope.employees = result;
                            });
                        }
                        else {
                            $scope.serverErrorMsgs = result.ModelState;
                        }
                    });
            }
        }

        $scope.RemindEmployeeById = function(emp) {
            var msg = $window.prompt('Please enter your message:', 'Need your info!');
            employeeMgmtService.remindEmployeeById(emp.EmployeeId, msg)
                .then(function(result) {
                    if (result.status == 200) {
                        $scope.Msg = 'Reminder has been sent successfully';
                        $scope.showAlert = true;
                        $scope.serverErrorMsgs = "";
                        utilityService.slideUpAlert();
                    } else {
                        $scope.serverErrorMsgs = result.data.ModelState;
                    }
                });
        }
    });

})();