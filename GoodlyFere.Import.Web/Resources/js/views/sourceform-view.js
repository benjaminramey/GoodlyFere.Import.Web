define(['jquery', 'backbone', 'models/source-model', 'data', 'app'], function ($, Backbone, SourceModel, Data, app) {
    return Backbone.View.extend({
        template: _.template($("#sourceform-view").html()),
        optionTemplate: _.template("<option value='{{fullTypeName}}'>{{fullTypeName}}</option>"),

        events: {
            "click input[type=submit]": "save"
        },

        initialize: function () {
            this.model = this.model || new SourceModel();
            
            this.availableTypes = new (Backbone.Collection.extend({ url: "/api/availablesources" }));
            this.listenTo(this.availableTypes, "reset", this.addAllAvailable);
            
            this.sources = (new Data()).getSources();
            this.listenTo(this.sources, "sync", this.saveSuccess);

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
                this.model = this.sources.create(this.model, { wait: true });
            } else {
                this.model.save();
            }
        },
        
        saveSuccess: function() {
            app.navigate("model/source/list");
        },

        addAllAvailable: function (sources) {
            sources.each(function (s) {
                this.$("select#type").append(this.optionTemplate(s.toJSON()));
            }, this);
        }
    });
});