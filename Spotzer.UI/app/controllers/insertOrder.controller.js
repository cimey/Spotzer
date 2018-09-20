'use strict';
app.controller('insertOrderController', ['$scope', 'ordersService', 'toaster', 'APP_SETTINGS', '$uibModal', function ($scope, ordersService, toaster, app_settings, $modal) {

    $scope.company = { id: 1, name: "Company1" };
    $scope.partners = app_settings.partners;
    $scope.companies = app_settings.companies;
    $scope.typesOfOrder = app_settings.typesOfOrder;
    //$scope.addWebsite = false;
    $scope.website = {};

    $scope.input = {
        PartnerId: 0,
        TypeOfOrder: "Type1",
        CompanyId: 0,
        CompanyName: "",
        SubmittedBy: "Cuma Kılınç",
        AdditionalOrderInfo: {},
        Websites: [],
        PaidProducts: [],
    };
    $scope.cancel = function () {
        if (confirm("Are you sure?")) {
            window.location = "#!/orders";
        }
    };
    $scope.deleteWebsite = function (idx) {

        $scope.input.Websites.splice(idx, 1);
    }
    $scope.deletePaidProduct = function (idx) {

        $scope.input.PaidProducts.splice(idx, 1);
    }
    $scope.insertOrder = function () {

        $scope.input.AdditionalOrderInfo = JSON.stringify($scope.input.AdditionalInfo);
        $scope.input.CompanyId = $scope.company.id;
        $scope.input.CompanyName = $scope.company.name;

        if ($scope.input.PartnerId == 2 || $scope.input.PartnerId == 3) {
            if ($scope.input.Websites.length == 0) {
                toaster.pop("warning", "Please add at least one website product");
                return;
            }

            if ($scope.input.PaidProducts.length == 0) {
                toaster.pop("warning", "Please add at least one paid product");
                return;
            }
        }
        if ($scope.input.PartnerId == 1) {
            if ($scope.input.Websites.length == 0) {
                toaster.pop("warning", "Please add at least one website product");
                return;
            }
        }

        if ($scope.input.PartnerId == 4) {
            if ($scope.input.PaidProducts.length == 0) {
                toaster.pop("warning", "Please add at least one paid product");
                return;
            }
        }
        ordersService.insertOrder($scope.input)
            .then(function (results) {
                console.log(results);
                toaster.pop("success", "Order added!");
                window.location = "#!/orders";
            }, function (error) {
                toaster.pop("error", error.data.ExceptionMessage);
            });
    }

    $scope.addWebsite = function () {
        $modal.open({
            animation: true,
            templateUrl: 'app/views/website.modal.html',
            controller: 'websiteModalController',
            size: 'md',
            backdrop: false,
            resolve: {
                input: function () {
                    return $scope.input;
                }
            }
        }).result.then(function (result) {
            $scope.input.Websites.push(result);
        });;
    }

    $scope.addPaidProduct = function () {
        $modal.open({
            animation: true,
            templateUrl: 'app/views/paidProduct.modal.html',
            controller: 'paidProductModalController',
            size: 'md',
            backdrop: false,
            resolve: {
                input: function () {
                    return $scope.input;
                }
            }
        }).result.then(function (result) {
            $scope.input.PaidProducts.push(result);
        });;
    }

    /* Private methods */
    function loadDetails() {
    };

    /* Constructor */
    init();
    function init() {
        loadDetails();
    };

}]);