'use strict';
app.factory('ordersService', ['$http', 'APP_SETTINGS', function ($http, settings) {

    var serviceBase = settings.apiBaseUri;
    var ordersServiceFactory = {};

    var insertOrder = function (input) {

        return $http.post(serviceBase + 'api/order/insertOrder', input).then(function (response) {
            return response;
        });
    };
    var getOrders = function () {

        return $http.get(serviceBase + 'api/order/getOrders').then(function (response) {
            return response;
        });
    };

    ordersServiceFactory.insertOrder = insertOrder;
    ordersServiceFactory.getOrders = getOrders;

    return ordersServiceFactory;
}]);