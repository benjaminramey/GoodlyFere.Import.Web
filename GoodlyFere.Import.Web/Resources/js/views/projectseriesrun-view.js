define(['jquery', 'backbone', 'app', 'data',
        'models/run-configuration-collection'],
    function ($, Backbone, app, Data, RunConfigCollection) {
        return Backbone.View.extend({
            tagName: 'div',
            className: 'project-seriesrun',
            template: _.template($("#projectseriesrun-view").html()),
            stepTemplate: _.template($("#projectseriesrun-step-view").html()),
            optionTemplate: _.template("<option value='{{id}}'>{{project.name}} - {{name}}</option>"),
            
            events: {
                'click button#add-step-btn': 'addStep',
                'click button#run-steps-btn': 'runSeries'
            },

            initialize: function () {
                this.runConfigs = new RunConfigCollection();

                this.listenTo(this.runConfigs, "add", this.addOne);
                this.listenTo(this.runConfigs, "reset", this.addAll);
                
                this.runConfigs.fetch();
            },

            render: function () {
                this.$el.html(this.template());
                this.$('#run-steps').html(this.stepTemplate());

                return this;
            },
            
            runSeries: function (e) {
                e.preventDefault();

                app.showInfo("Starting project run series");
                var configIds = this.$('.step-configs').map(function() {
                    return $(this).val();
                });

                this.runConfig(configIds, 0);
            },
            
            runConfig: function (configIds, idx) {
                var view = this;
                var config = this.runConfigs.get(configIds[idx]);
                config.run({
                    success: function () {
                        idx++;
                        if (idx < configIds.length) {
                            view.runConfig(configIds, idx);
                        } else {
                            app.showSuccess("Project run series complete.");
                        }
                    }
                });
            },

            addOne: function (config) {
                var view = this;
                this.$('.step-configs').each(function() {
                    view.addOneTo(config, $(this));
                });
            },
            
            addOneTo: function (config, select) {
                var options = this.optionTemplate(config.toJSON());
                select.append(options);
            },

            addAll: function (configs) {
                var view = this;
                this.$(".step-configs").each(function() {
                    view.addAllTo(configs, $(this));
                });
            },
            
            addAllTo: function (configs, select) {
                select.empty();
                configs.each(function(config) { this.addOneTo(config, select); }, this);
            },
            
            addStep: function(e) {
                e.preventDefault();

                this.$('#run-steps').append(this.stepTemplate());
                this.addAllTo(this.runConfigs, this.$(".step:last select"));
            }
        });
    });