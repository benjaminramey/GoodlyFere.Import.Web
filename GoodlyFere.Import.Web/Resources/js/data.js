define(['jquery', 'app',
    'models/project-collection',
    'models/source-collection',
    'models/destination-collection',
    'models/converter-collection',
    'models/plugin-collection'],
    function ($, app, ProjectCollection, SourceCollection, DestinationCollection, ConverterCollection, PluginCollection) {

        var requestCount = 0;
        var _hideLoader = function () {
            requestCount--;
            if (requestCount <= 0) {
                $("#loading").hide();
            }
        };

        var _get = function (collection) {
            if (!collection.__fetched) {
                collection.fetch();
                collection.__fetched = true;
            }

            return collection;
        };
        
        if (!window.Import.data) {
            var data = {
                projects: new ProjectCollection(),
                sources: new SourceCollection(),
                destinations: new DestinationCollection(),
                converters: new ConverterCollection(),
                plugins: new PluginCollection()
            };

            data.projects.reset(window.Import._initialData.projects);
            
            for (var key in data) {

                data[key].on("request", function() {
                    $("#loading").show();
                    requestCount++;
                });
                data[key].on("sync", _hideLoader);

                data[key].on("error", function (model, xhr) {
                    _hideLoader();
                    var err = JSON.parse(xhr.responseText);
                    app.showError(
                        err.message
                        + "<br />" + err.exceptionMessage
                        + "<br />" + err.stackTrace
                    );
                });
            }

            window.Import.data = data;
        }

        return function () {
            return {
                getSources: function () { return _get(window.Import.data.sources); },
                getProjects: function () { return _get(window.Import.data.projects); },
                getDestinations: function () { return _get(window.Import.data.destinations); },
                getConverters: function () { return _get(window.Import.data.converters); },
                getPlugins: function () { return _get(window.Import.data.plugins); }
            };
        };
    });