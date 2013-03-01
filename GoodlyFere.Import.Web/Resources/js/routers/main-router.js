define(['jquery',
        'backbone',
        'data',
        'views/main-view',
        'views/projectrun-view'],

    function ($, Backbone, Data, MainView, ProjectRunView) {

        return Backbone.Router.extend({
            routes: {
                "": "home",
                "/": "home",
                "project/run/:id": "runProject",
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
            }
        });
    });