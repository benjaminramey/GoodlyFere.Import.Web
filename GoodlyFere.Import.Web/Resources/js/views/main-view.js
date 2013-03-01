define(['jquery', 'backbone', 'views/projects-view'], function ($, Backbone, ProjectsView) {
    return Backbone.View.extend({
        el: function () { return $("#page"); },
        template: _.template($("#main-view").html()),
        
        render: function () {
            this.$el.html(this.template());

            var projectsView = new ProjectsView();
            this.$el.find("#content").html(projectsView.render().el);
            
            return this;
        }
    });
});