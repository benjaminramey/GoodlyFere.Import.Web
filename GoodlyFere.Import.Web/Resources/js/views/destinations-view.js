define(['jquery', 'backbone', 'data', 'views/destinationrow-view'], function ($, Backbone, Data, DestinationRowView) {
    return Backbone.View.extend({
        tagName: "div",
        id: "destinations",
        template: _.template($("#destinations-view").html()),

        events: {
            "click a.refresh": "refresh",
        },

        initialize: function () {
            this.destinations = (new Data()).getDestinations();
            
            this.listenTo(this.destinations, "add", this.addOne);
            this.listenTo(this.destinations, "reset", this.addAll);
        },

        render: function () {
            this.$el.html(this.template());
            this.addAll(this.destinations);

            return this;
        },

        addOne: function (destination) {
            var row = new DestinationRowView({ model: destination });
            this.$("tbody").append(row.render().el);
        },

        addAll: function (destinations) {
            this.$("tbody").empty();
            destinations.each(function (d) { this.addOne(d); }, this);
        },
        
        refresh: function() {
            this.destinations.fetch();
        }
    });
});