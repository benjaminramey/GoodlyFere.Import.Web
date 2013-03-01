define(['backbone', 'models/project-model'], function (Backbone, ProjectModel) {
    return Backbone.Collection.extend({
        url: "/api/projects",
        model: ProjectModel
    });
});