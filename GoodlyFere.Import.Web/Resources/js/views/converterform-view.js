define(['jquery', 'backbone', 'models/converter-model', 'data', 'app'], function ($, Backbone, ConverterModel, Data, app) {
    return Backbone.View.extend({
        template: _.template($("#converterform-view").html()),
        optionTemplate: _.template("<option values='{{fullTypeName}}'>{{fullTypeName}}</option>"),

        events: {
            "click input[type=submit]": "save"
        },
        
        initialize: function () {
            this.model = this.model || new ConverterModel();

            this.availableTypes = new (Backbone.Collection.extend({ url: "/api/availableconverters" }));
            this.listenTo(this.availableTypes, "reset", this.addAllAvailable);
            
            this.converters = (new Data()).getConverters();
            this.listenTo(this.converters, "sync", this.saveSuccess);
            
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
                this.model = this.converters.create(this.model, { wait: true });
            } else {
                this.model.save();
            }
        },

        saveSuccess: function () {
            app.navigate("model/converter/list");
        },
        
        addAllAvailable: function (converters) {
            converters.each(function(d) {
                this.$("select#type").append(this.optionTemplate(d.toJSON()));
            }, this);
        }
    });
});