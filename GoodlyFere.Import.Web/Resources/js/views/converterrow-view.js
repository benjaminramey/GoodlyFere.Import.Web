define(['jquery', 'backbone', 'views/converteractions-view'], function ($, Backbone, ConverterActionsView) {
    return Backbone.View.extend({
        tagName: "tr",
        template: _.template($("#converterrow-view").html()),
        
        initialize: function () {
            this.model.on("destroy", this.modelDestroyed, this);
        },
        
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));

            var actionsView = new ConverterActionsView({ model: this.model });
            this.$el.find(".converter-actions").html(actionsView.render().el);

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