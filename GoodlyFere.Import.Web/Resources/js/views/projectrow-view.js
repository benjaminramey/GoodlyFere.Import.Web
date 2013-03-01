define(['jquery', 'backbone', 'views/projectactions-view'], function ($, Backbone, ProjectActionsView) {
    return Backbone.View.extend({
        tagName: "tr",
        template: _.template($("#projectrow-view").html()),
        
        initialize: function () {
            this.model.on("destroy", this.modelDestroyed, this);
        },
        
        render: function () {
            this.$el.html(this.template(this.model.toJSON()));

            var actionsView = new ProjectActionsView({ model: this.model });
            this.$el.find(".project-actions").html(actionsView.render().el);

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