define(['jquery', 'backbone', 'app'], function ($, Backbone, app) {
    return Backbone.Model.extend({
        urlRoot: "/api/runconfigurations",

        run: function (options) {
            options = options || {};
            options = $.extend({
                method: "RUN",
                url: this.urlRoot,
                data: JSON.stringify(this.toJSON()),
                dataType: "json",
                contentType: "application/json",
                processData: false,
                success: function() {
                    app.showInfo("Project ran successfully!");
                },
                error: function() {
                    app.showError("Project run failed.");
                }
            }, options);

            $.ajax(options);
        }
    });
});