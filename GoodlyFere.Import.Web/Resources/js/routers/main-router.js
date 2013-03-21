define(['jquery',
        'backbone',
        'data',
        'views/main-view',
        'views/projectrun-view',
        'views/projectseriesrun-view'],

    function ($, Backbone, Data, MainView, ProjectRunView, ProjectSeriesRunView) {

        return Backbone.Router.extend({
            routes: {
                "": "home",
                "/": "home",
                "project/:id/run": "runProject",
                "project/seriesrun": "runSeries",
                "plugins": "plugins"
            },

            home: function () {
                var mainView = new MainView();
                mainView.render();
            },
            
            runProject: function (id) {
                var project = (new Data()).getProjects().get(id);
                var view = new ProjectRunView({ model: project });
                
                $("#content").html(view.render().el);
            },
            
            runSeries: function() {
                var view = new ProjectSeriesRunView();
                $("#content").html(view.render().el);
            }
        });
    });