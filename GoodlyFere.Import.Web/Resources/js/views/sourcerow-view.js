define(['jquery', 'backbone', 'views/sourceactions-view'], function ($, Backbone, SourceActionsView) {
    return Backbone.View.extend({
        tagName: "tr",
        template: _.template($("#sourcerow-view").html()),
        
        initialize: function () {
            this.model.on("destroy", this.modelDestroyed, this);
        },
        
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));

            var actionsView = new SourceActionsView({ model: this.model });
            this.$el.find(".source-actions").html(actionsView.render().el);

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