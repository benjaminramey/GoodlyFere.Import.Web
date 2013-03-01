define(['jquery', 'backbone', 'models/destination-model', 'data', 'app'], function ($, Backbone, DestinationModel, Data, app) {
    return Backbone.View.extend({
        template: _.template($("#destinationform-view").html()),
        optionTemplate: _.template("<option values='{{fullTypeName}}'>{{fullTypeName}}</option>"),

        events: {
            "click input[type=submit]": "save"
        },
        
        initialize: function () {
            this.model = this.model || new DestinationModel();

            this.availableTypes = new (Backbone.Collection.extend({ url: "/api/availabledestinations" }));
            this.listenTo(this.availableTypes, "reset", this.addAllAvailable);
            
            this.destinations = (new Data()).getDestinations();
            this.listenTo(this.destinations, "sync", this.saveSuccess);
            
            this.availableTypes.fetch();
        },

        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            
            return this;
        },
        
        save: function (e) {
            e.preventDefault();

            this.model.set({
                name: this.$("input#name").val(),
                type: this.$("select#type").val()
            });

            if (this.model.isNew()) {
                this.model = this.destinations.create(this.model, { wait: true });
            } else {
                this.model.save();
            }
        },

        saveSuccess: function () {
            app.navigate("model/destination/list");
        },
        
        addAllAvailable: function (destinations) {
            destinations.each(function(d) {
                this.$("select#type").append(this.optionTemplate(d.toJSON()));
            }, this);
        }
    });
});