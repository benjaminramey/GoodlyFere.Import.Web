define(['backbone', 'models/converter-model'], function (Backbone, ConverterModel) {
    return Backbone.Collection.extend({
        url: "/api/converters",
        model: ConverterModel
    });
});