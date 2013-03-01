define(['jquery', 'backbone'], function ($, Backbone) {
    return Backbone.View.extend({
        tagName: "tr",
        template: _.template($("#destinationactions-view").html()),

        events: {
            "click .delete-action": "deleteDestination"
        },
        
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },

        deleteDestination: function (e) {
            e.preventDefault();
            this.model.destroy();
        }
    });
});