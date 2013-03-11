define(['jquery', 'backbone', 'models/plugin-model', 'data', 'app', 'jform'],
    function ($, Backbone, PluginModel, Data, app) {
        return Backbone.View.extend({
            template: _.template($("#pluginform-view").html()),

            events: {
                "click form.plugin-form input[type=submit]": "save"
            },

            initialize: function () {
                this.model = this.model || new PluginModel();

                this.plugins = (new Data()).getPlugins();
                this.listenTo(this.plugins, "sync", this.saveSuccess);
            },

            render: function () {
                this.$el.html(this.template(this.model.toJSON()));

                var view = this;
                this.$("form.pluginpackage-form").ajaxForm(function (result) {
                    view.model.set({ packagePath: result });
                    view.enableSave();
                });
                
                if (this.model.get("PackagePath") != "") {
                    this.enableSave();
                }

                return this;
            },
            
            enableSave: function() {
                this.$("form.plugin-form input[type=submit]").removeAttr("disabled");
            },

            save: function (e) {
                if (e) {
                    e.preventDefault();
                }

                this.model.set({
                    name: this.$("input#name").val()
                });

                if (this.model.isNew()) {
                    this.model = this.plugins.create(this.model, { wait: true });
                } else {
                    this.model.save();
                }
            },

            saveSuccess: function () {
                app.navigate("model/plugin/list");
            }
        });
    });