app.controller('paidProductModalController', ['$scope', '$uibModalInstance', 'input', function ($scope, $modalInstance, input) {

    $scope.paidProduct = {};
    $scope.addPaidProduct = function () {
        $modalInstance.close($scope.paidProduct);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss();
    };
}]);
