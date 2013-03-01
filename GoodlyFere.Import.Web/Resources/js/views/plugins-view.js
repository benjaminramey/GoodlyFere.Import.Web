define(['jquery','backbone', 'data'], function ($, Backbone, Data) {
    return Backbone.View.extend({
        tagName: "div",
        id: "plugins",
        template: _.template($("#plugins-view").html()),
        rowTemplate: _.template($("#pluginrow-view").html()),

        events: {
            "click a.refresh": "refresh",
            "click .delete-action": "deletePlugin",
            "click .reload-action": "reloadPlugin"
        },

        initialize: function () {
            this.plugins = (new Data()).getPlugins();

            this.listenTo(this.plugins, "add", this.addOne);
            this.listenTo(this.plugins, "reset", this.addAll);
        },

        render: function () {
            this.$el.html(this.template());
            this.addAll(this.plugins);
            
            return this;
        },

        addOne: function (plugin) {
            var row = this.rowTemplate(plugin.toJSON());
            this.$('tbody').append(row);
        },

        addAll: function (plugins) {
            this.$("tbody").empty();
            plugins.each(this.addOne, this);
        },
        
        deletePlugin: function (e) {
            e.preventDefault();
            var row = this.getRowFromPluginAction(e);
            var plugin = this.getPluginFromRowAction(e);
            plugin.destroy();
            row.fadeOut(function () { $(this).remove(); });
        },
        
        reloadPlugin: function(e) {
            e.preventDefault();
            var plugin = this.getPluginFromRowAction(e);
            plugin.reload();
        },
        
        getRowFromPluginAction: function(e) {
            return $(e.target).parents("tr:first");
        },
        
        getPluginFromRowAction: function (e) {
            var row = this.getRowFromPluginAction(e);
            var id = row.data("id");
            return this.plugins.get(id);
        },
        
        refresh: function (e) {
            e.preventDefault(); 
            this.plugins.fetch();
        }
    });
});