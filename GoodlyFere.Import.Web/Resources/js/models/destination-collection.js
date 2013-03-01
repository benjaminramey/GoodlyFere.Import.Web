define(['backbone', 'models/destination-model'], function (Backbone, DestinationModel) {
    return Backbone.Collection.extend({
        url: "/api/destinations",
        model: DestinationModel
    });
});