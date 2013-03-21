define(['jquery','backbone','data',
    'views/projects-view',
    'views/projectform-view',
    'views/sourceform-view',
    'views/sources-view',
    'views/destinationform-view',
    'views/destinations-view',
    'views/converterform-view',
    'views/converters-view',
    'views/pluginform-view',
    'views/plugins-view'],

    function ($, Backbone, Data,
        ProjectsView, ProjectFormView,
        SourceFormView, SourcesView,
        DestinationFormView, DestinationsView,
        ConverterFormView, ConvertersView,
        PluginFormView, PluginsView) {

        return Backbone.Router.extend({
            routes: {
                "model/:type/list": "list",
                "model/:type/:id/edit": "newOrEditModel",
                "model/:type/new": "newOrEditModel"
            },

            list: function (type) {
                var view;
                if (type == "source") {
                    view = new SourcesView();
                }
                else if (type == "destination") {
                    view = new DestinationsView();
                }
                else if (type == "converter") {
                    view = new ConvertersView();
                }
                else if (type == "plugin") {
                    view = new PluginsView();
                }
                else {
                    view = new ProjectsView();
                }

                $("#content").html(view.render().el);
            },

            newOrEditModel: function (type, id) {
                var data = new Data();
                var collection;
                var view;

                if (type == "project") {
                    view = new ProjectFormView();
                    collection = data.getProjects();
                }
                else if (type == "source") {
                    view = new SourceFormView();
                    collection = data.getSources();
                }
                else if (type == "converter") {
                    view = new ConverterFormView();
                    collection = data.getConverters();
                }
                else if (type == "destination") {
                    view = new DestinationFormView();
                    collection = data.getDestinations();
                }
                else if (type == "plugin") {
                    view = new PluginFormView();
                    collection = data.getPlugins();
                }

                this._crudHandler(id ? "edit" : "new", id, view, collection);
            },

            _crudHandler: function (action, id, view, collection) {
                if (action == "new" || action == "edit") {

                    if (id) {
                        view.model = collection.get(id);
                    }

                    $("#content").html(view.render().el);
                }
            }
        });
    });