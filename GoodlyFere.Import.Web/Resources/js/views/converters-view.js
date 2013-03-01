define(['jquery', 'backbone', 'data', 'views/converterrow-view'], function ($, Backbone, Data, ConverterRowView) {
    return Backbone.View.extend({
        tagName: "div",
        id: "converters",
        template: _.template($("#converters-view").html()),

        events: {
            "click a.refresh": "refresh",
        },

        initialize: function () {
            this.converters = (new Data()).getConverters();
            
            this.listenTo(this.converters, "add", this.addOne);
            this.listenTo(this.converters, "reset", this.addAll);
        },

        render: function () {
            this.$el.html(this.template());
            this.addAll(this.converters);

            return this;
        },

        addOne: function (converter) {
            var row = new ConverterRowView({ model: converter });
            this.$("tbody").append(row.render().el);
        },

        addAll: function (converters) {
            this.$("tbody").empty();
            converters.each(function (d) { this.addOne(d); }, this);
        },
        
        refresh: function() {
            this.converters.fetch();
        }
    });
});