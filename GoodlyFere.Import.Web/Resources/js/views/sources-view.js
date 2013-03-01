define(['jquery', 'backbone', 'data', 'views/sourceform-view', 'views/sourcerow-view'], function ($, Backbone, Data, SourceFormView, SourceRowView) {
    return Backbone.View.extend({
        tagName: "div",
        id: "sources",
        template: _.template($("#sources-view").html()),

        events: {
            "click a.refresh": "refresh",
        },

        initialize: function () {
            this.sources = (new Data()).getSources();

            this.listenTo(this.sources, "add", this.addOne);
            this.listenTo(this.sources, "reset", this.addAll);
        },

        render: function () {
            this.$el.html(this.template());
            this.addAll(this.sources);

            return this;
        },

        addOne: function (source) {
            var row = new SourceRowView({ model: source });
            this.$('tbody').append(row.render().el);
        },
        
        addAll: function (sources) {
            this.$("tbody").empty();
            sources.each(this.addOne, this);
        },
        
        refresh: function() {
            this.sources.fetch();
        }
    });
});