define(['jquery','backbone',
        'app',
        'models/run-configuration-model',
        'models/run-configuration-collection',
        'models/parameter-instance-model'
],
    function ($, Backbone, app, RunConfigModel, RunConfigCollection, ParameterInstanceModel) {
        return Backbone.View.extend({
            className: "project-run",
            template: _.template($("#projectrun-view").html()),
            paramTemplate: _.template("<div class='param'><label>{{name}}</label><br /><textarea name='{{name}}' cols='50' rows='4'></textarea></div>"),
            optionTemplate: _.template("<option value='{{id}}'>{{name}}</option>"),

            events: {
                "click button#run-button": "run",
                "click button#clear-params": "clearParams",
                "click button#save-new-config": "saveNewConfig",
                "change select#saved-configs": "handleChooseConfig",
                "click button#save-current-config": "saveCurrentConfig"
            },

            initialize: function () {
                var source = this.model.get("sourceDefinition");
                var destination = this.model.get("destinationDefinition");
                var converter = this.model.get("converterDefinition");

                this.savedConfigs = new RunConfigCollection();
                this.sourceParams = new Backbone.Collection();
                this.sourceParams.url = "/api/sourceparameters?type=" + source.type;
                this.destinationParams = new Backbone.Collection();
                this.destinationParams.url = "/api/destinationparameters?type=" + destination.type;
                this.converterParams = new Backbone.Collection();
                this.converterParams.url = "/api/converterparameters?type=" + converter.type;

                this.listenTo(this.savedConfigs, "add", this.addConfig);
                this.listenTo(this.savedConfigs, "reset", this.addAllConfigs);
                this.listenTo(this.sourceParams, "reset", this.addSourceParams);
                this.listenTo(this.destinationParams, "reset", this.addDestinationParams);
                this.listenTo(this.converterParams, "reset", this.addConverterParams);

                this.sourceParams.fetch();
                this.destinationParams.fetch();
                this.converterParams.fetch();
                this.savedConfigs.fetchForProject(this.model.get("id"));
            },

            render: function () {
                this.$el.html(this.template(this.model.toJSON()));
                return this;
            },

            handleChooseConfig: function (e) {
                var configId = $(e.target).val();
                if (!configId) {
                    this.clearCurrentConfig(e);
                    return;
                }

                this.applySavedConfig(configId);
            },

            clearCurrentConfig: function (e) {
                this.clearParams(e);
                this.$("button#save-current-config").attr("disabled", "disabled");
            },

            applySavedConfig: function (configId) {
                this.$("button#save-current-config").removeAttr("disabled");
                this.currentConfig = this.savedConfigs.get(configId);
                _.each(this.currentConfig.get("parameterInstances"), function (i) {
                    this.$("[name='" + i.name + "']").val(i.value);
                }, this);
            },

            addConfig: function (config) {
                this.$("#saved-configs").append(this.optionTemplate(config.toJSON()));

                if (this.currentConfig) {
                    this.$("#saved-configs").val(this.currentConfig.get("id"));
                }
            },

            addAllConfigs: function (configs) {
                this.$("#saved-configs option:not(:first-child)").remove();
                configs.each(function (c) {
                    this.addConfig(c);
                }, this);
                
                if (this.currentConfig) {
                    this.$("#saved-configs").val(this.currentConfig.get("id"));
                }
            },

            addSourceParams: function (params) {
                params.each(function (p) {
                    this.$("#source-params").append(this.paramTemplate(p.toJSON()));
                }, this);
            },

            addDestinationParams: function (params) {
                params.each(function (p) {
                    this.$("#destination-params").append(this.paramTemplate(p.toJSON()));
                }, this);
            },
            
            addConverterParams: function (params) {
                params.each(function (p) {
                    this.$("#converter-params").append(this.paramTemplate(p.toJSON()));
                }, this);
            },

            saveCurrentConfig: function (e) {
                e.preventDefault();

                var parameters = this.currentConfig.get("parameterInstances");

                if (parameters && parameters.length > 0) {
                    for (var k = 0; k < parameters.length; k++) {
                        var name = parameters[k].name;
                        var value = this.$("fieldset :input[name=" + name + "]").val();
                        parameters[k].value = value;
                    }
                } else {
                    parameters = $.map(this.$("fieldset :input"), this.paramMapper);
                    _.each(parameters, function(p) {
                        p["runConfigurationId"] = this.currentConfig.get("id");
                    }, this);
                    this.currentConfig.set("parameterInstance", parameters);
                }

                _.each(parameters, function (sp) {
                    (new ParameterInstanceModel(sp)).save();
                });
                this.savedConfigs.fetchForProject(this.model.get("id"));
                app.showInfo("Configuration saved.");
            },

            saveNewConfig: function (e) {
                e.preventDefault();
                var name = prompt("Configuration name", "New configuration");

                if (!name) {
                    return;
                }
                this.currentConfig = this.savedConfigs.create({
                    name: name,
                    projectId: this.model.get("id"),
                    parameterInstances: $.map(this.$("fieldset :input"), this.paramMapper)
                }, { wait: true });

                this.$("button#save-current-config").removeAttr("disabled");
            },

            clearParams: function (e) {
                e.preventDefault();
                this.$("fieldset :input").val("");
            },

            run: function (e) {
                e.preventDefault();

                var config = new RunConfigModel({
                    projectId: this.model.get("id"),
                    parameterInstances: $.map(this.$("fieldset :input"), this.paramMapper)
                });

                config.run();
            },

            paramMapper: function (input) {
                var type = "source";
                if ($(input).parents("#destination-params").length > 0) {
                    type = "destination";
                }
                return {
                    name: $(input).attr("name"),
                    value: $(input).val(),
                    type: type
                };
            }
        });
    });