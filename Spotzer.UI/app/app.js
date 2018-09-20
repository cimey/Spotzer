var app = angular.module('spotzerApp', ['ngRoute', 'toaster', 'ui.bootstrap']);

app.config(function ($routeProvider, $httpProvider) {

    $routeProvider.when("/insertOrder", {
        controller: "insertOrderController",
        templateUrl: "/app/views/insertOrder.html"
    });
    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "/app/views/orders.html"
    });

    $routeProvider.otherwise({ redirectTo: "/orders" });

});

//app.run(['authService', function (authService) {
//    authService.fillAuthData();
//}]);
app.config(function () {
});

app.constant('APP_SETTINGS', {
    apiBaseUri: 'http://localhost:42923/',
    partners: [{ id: 1, name: "PartnerA" }, { id: 2, name: "PartnerB" }, { id: 3, name: "PartnerC" }, { id: 4, name: "PartnerD" }],
    companies: [{ id: 1, name: "Company1" }, { id: 2, name: "Company2" }, { id: 3, name: "Company3" }, { id: 4, name: "Company4" }],
    typesOfOrder: ["Type1", "Type2", "Type3"]
});