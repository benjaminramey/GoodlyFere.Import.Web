define(['jquery', 'backbone', 'data', 'models/project-model', 'app'], function ($, Backbone, Data, ProjectModel, app) {
    return Backbone.View.extend({
        template: _.template($("#projectform-view").html()),
        optionTemplate: _.template("<option value='{{id}}'>{{name}}</option>"),

        events: {
            "click input[type=submit]": "save"
        },

        initialize: function () {
            this.model = this.model || new ProjectModel();

            var data = new Data();
            this.projects = data.getProjects();
            this.sources = data.getSources();
            this.destinations = data.getDestinations();
            this.converters = data.getConverters();

            this.listenTo(this.sources, "reset", this.addAllSources);
            this.listenTo(this.sources, "add", this.addOneSource);
            this.listenTo(this.destinations, "reset", this.addAllDestinations);
            this.listenTo(this.destinations, "add", this.addOneDestination);
            this.listenTo(this.converters, "reset", this.addAllConverters);
            this.listenTo(this.converters, "add", this.addOneConverters);

            this.listenTo(this.projects, "sync", this.saveSuccess);
        },

        render: function () {
            this.$el.html(this.template(this.model.toJSON()));
            this.addAllSources(this.sources);
            this.addAllDestinations(this.destinations);
            this.addAllConverters(this.converters);
            
            return this;
        },

        addAllSources: function (sources) {
            sources.each(function (s) { this.addOneSource(s); }, this);
            this.$("select#source").val(this.model.get("sourceDefinitionId"));
        },

        addAllDestinations: function (destinations) {
            destinations.each(function (d) { this.addOneDestination(d); }, this);
            this.$("select#destination").val(this.model.get("destinationDefinitionId"));
        },

        addAllConverters: function (converters) {
            converters.each(function (c) { this.addOneConverter(c); }, this);
            this.$("select#converter").val(this.model.get("converterDefinitionId"));
        },

        addOneSource: function (source) {
            var sourceDDL = this.$("select#source");
            sourceDDL.append(this.optionTemplate(source.toJSON()));
        },

        addOneDestination: function (destination) {
            var destinationDDL = this.$("select#destination");
            destinationDDL.append(this.optionTemplate(destination.toJSON()));
        },

        addOneConverter: function (converter) {
            var converterDDL = this.$("select#converter");
            converterDDL.append(this.optionTemplate(converter.toJSON()));
        },

        save: function (e) {
            e.preventDefault();

            this.model.set({
                name: this.$("input#name").val(),
                destinationDefinitionId: this.$("select#destination").val(),
                sourceDefinitionId: this.$("select#source").val(),
                converterDefinitionId: this.$("select#converter").val()
            });
            
            if (this.model.isNew()) {
                this.model = this.projects.create(this.model, { wait: true });
            } else {
                this.model.save();
            }
        },

        saveSuccess: function () {
            app.navigate("model/project/list");
        }
    });
});