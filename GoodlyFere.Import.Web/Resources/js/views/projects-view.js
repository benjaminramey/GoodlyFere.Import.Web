define(['jquery', 'backbone', 'data', 'views/projectrow-view'], function ($, Backbone, Data, ProjectRowView) {
    return Backbone.View.extend({
        tagName: "div",
        id: "projects",
        template: _.template($("#projects-view").html()),

        events: {
            "click a.refresh": "refresh",
        },

        initialize: function() {
            this.projects = (new Data()).getProjects();

            this.listenTo(this.projects, "add", this.addOne);
            this.listenTo(this.projects, "reset", this.addAll);
        },

        render: function() {
            this.$el.html(this.template());
            this.addAll(this.projects);

            return this;
        },

        addOne: function(project) {
            var projectRow = new ProjectRowView({ model: project });
            this.$("tbody").append(projectRow.render().el);
        },
        
        addAll: function(projects) {
            this.$("tbody").empty();
            projects.each(function(p) { this.addOne(p); }, this);
        },
        
        refresh: function() {
            this.projects.fetch();
        }
    });
});