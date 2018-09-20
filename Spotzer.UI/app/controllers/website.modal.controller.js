app.controller('websiteModalController', ['$scope', '$uibModalInstance', 'input', function ($scope, $modalInstance, input) {

    $scope.website = {};
    $scope.addWebsite = function () {
        $modalInstance.close($scope.website);
    };

    $scope.cancel = function () {
        $modalInstance.dismiss();
    };
}]);
