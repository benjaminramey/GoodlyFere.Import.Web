define(['jquery', 'backbone'], function ($, Backbone) {
    return Backbone.View.extend({
        tagName: "tr",
        template: _.template($("#projectactions-view").html()),

        events: {
            "click .delete-action": "deleteProject",
            "click .run-action": "runProject"
        },
        
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },

        deleteProject: function (e) {
            e.preventDefault();
            this.model.destroy();
        },

        runProject: function (e) {
            e.preventDefault();
        }
    });
});