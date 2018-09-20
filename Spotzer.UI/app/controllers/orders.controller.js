'use strict';
app.controller('ordersController', ['$scope', 'ordersService', 'toaster', '$location', function ($scope, ordersService, toaster, $location) {

    $scope.orders = [];

    $scope.getOrders = function () {
        ordersService.getOrders()
            .then(function (results) {
                $scope.orders = results.data;
            }, function (error) {
                toaster.pop("error", error.data.exceptionMessage);
            });
    }
    $scope.insertOrder = function () {
        $location.path("#/insertOrder");
    }
    /* Private methods */
    function loadDetails() {
        $scope.getOrders();
    };

    /* Constructor */
    init();
    function init() {
        loadDetails();
    };

}]);