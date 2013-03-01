define(['jquery', 'backbone'], function ($, Backbone) {
    return Backbone.View.extend({
        tagName: "tr",
        template: _.template($("#sourceactions-view").html()),

        events: {
            "click .delete-action": "deleteSource"
        },
        
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },

        deleteSource: function (e) {
            e.preventDefault();
            this.model.destroy();
        }
    });
});