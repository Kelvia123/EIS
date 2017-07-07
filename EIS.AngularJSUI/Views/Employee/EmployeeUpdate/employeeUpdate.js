(function (undefined) {
    appEIS.factory('employeeUpdateService', function($http) {
        var empUpdateObj = {};

        empUpdateObj.getById = function (empId) {
            var employee;

            employee = $http({ method: 'Get', url: 'http://localhost:4676/api/Employee', params: { id: empId } })
                        .then(function(response) { return response.data; });

            return employee;
        }

        return empUpdateObj;
    });

    appEIS.controller('employeeUpdateController', function ($scope, $routeParams, employeeUpdateService) {

        $('#profilePanel a').click(function (e) {
            e.preventDefault();
        });

        $scope.eid = $routeParams.EmployeeId;
        employeeUpdateService.getById($scope.eid)
                             .then(function(result) {
                                 $scope.Emp = result;
                                 //For DOJ's proper presentation in date input
                                 $scope.Emp.DateOfJoin = new Date($scope.Emp.DateOfJoin);
                             });
    });

})();