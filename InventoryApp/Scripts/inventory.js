(function () { // Angular encourages module pattern, good!
    var app = angular.module('myApp', []),
        uri = 'api/inventory',
        errorMessage = function (data, status) {
            return 'Error: ' + status +
                (data.Message !== undefined ? (' ' + data.Message) : '');
        } 

    app.controller('myCtrl', ['$http', '$scope', function ($http, $scope) {
        
        $scope.deleteOne = function (item) {
            $http.delete(uri + '/' + 3) 
                .success(function (data, status) {
                    $scope.errorToDelete = null;
                })
                .error(function (data, status) {
                    $scope.errorToDelete = errorMessage(data, status);
                })
        };
      
    }]);
})();
