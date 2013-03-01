define(['jquery', 'backbone', 'views/destinationactions-view'], function ($, Backbone, DestinationActionsView) {
    return Backbone.View.extend({
        tagName: "tr",
        template: _.template($("#destinationrow-view").html()),
        
        initialize: function () {
            this.model.on("destroy", this.modelDestroyed, this);
        },
        
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));

            var actionsView = new DestinationActionsView({ model: this.model });
            this.$el.find(".destination-actions").html(actionsView.render().el);

            return this;
        },
        
        modelDestroyed: function (data) {
            var view = this;
            this.$el.fadeOut(function() {
                view.remove();
            });
        }
    });
});