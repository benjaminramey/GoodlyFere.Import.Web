require.config({
    shim: {
        'backbone': {
            //These script dependencies should be loaded before loading
            //backbone.js
            deps: ['underscore', 'lib/json2'],
            //Once loaded, use the global 'Backbone' as the
            //module value.
            exports: 'Backbone'
        },
        'underscore': {
            exports: '_',
            init: function () {
                this._.templateSettings = { interpolate: /\{\{(.+?)\}\}/g };
                return this._;
            }
        }
    },
    paths: {
        backbone: "lib/backbone-min",
        underscore: "lib/underscore-min",
        jform: "lib/jquery.form"
    }
});

require(["jquery", "backbone", "routers/main-router", 'routers/crud-router', 'app'], function ($, Backbone, MainRouter, CrudRouter, app) {
    $(function () {
        app.router = new MainRouter();
        app.crudRouter = new CrudRouter();

        $(document).ajaxSend(function(event, jqxhr, settings) {
            app.showInfo("Starting " + settings.type + ": " + settings.url);
        });
        $(document).ajaxError(function (event, jqxhr, settings) {
            app.showError("Completed " + settings.type + " with error: " + settings.url);
        });
        $(document).ajaxSuccess(function (event, jqxhr, settings) {
            app.showSuccess("Completed " + settings.type + ": " + settings.url);
        });

        $("body").on("click", "a", function (e) {
            e.preventDefault();
            
            var fragment = $(this).attr("href");
            if (fragment != "#") {
                app.router.navigate(fragment, { trigger: true });
            }
        });
        
        Backbone.history.start({ pushState: true });
    });
});
