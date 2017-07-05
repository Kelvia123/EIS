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

            return empMgmtObj;
        });

    appEIS.controller('employeeMgmtController', function ($scope, employeeMgmtService) {
        $scope.msg = 'Welcom to employeeMgmt';

        employeeMgmtService.getAll().then(function(result) {
            $scope.employees = result;
        });

        $scope.Sort = function(col) {
            $scope.key = col;
            $scope.AscOrDesc = !$scope.AscOrDesc;
        };
    });

})();