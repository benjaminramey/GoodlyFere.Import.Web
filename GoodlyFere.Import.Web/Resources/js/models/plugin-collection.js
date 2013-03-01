define(['backbone', 'models/plugin-model'], function (Backbone, PluginModel) {
    return Backbone.Collection.extend({
        url: "/api/plugins",
        model: PluginModel
    });
});