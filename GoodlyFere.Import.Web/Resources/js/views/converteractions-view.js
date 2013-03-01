define(['jquery', 'backbone'], function ($, Backbone) {
    return Backbone.View.extend({
        tagName: "tr",
        template: _.template($("#converteractions-view").html()),

        events: {
            "click .delete-action": "deleteConverter"
        },
        
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            return this;
        },

        deleteConverter: function (e) {
            e.preventDefault();
            this.model.destroy();
        }
    });
});