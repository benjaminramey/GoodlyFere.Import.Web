define(['backbone', 'models/project-model'], function (Backbone, SourceModel) {
    return Backbone.Collection.extend({
        url: "/api/sources",
        model: SourceModel
    });
});