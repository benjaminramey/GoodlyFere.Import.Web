define(['backbone', 'app'], function (Backbone, app) {
    return Backbone.Model.extend({
        defaults: {
            "packagePath": "",
            "name": ""
        },

        reload: function () {
            $.ajax({
                method: "RELOAD",
                url: "/api/plugins",
                data: JSON.stringify(this.toJSON()),
                dataType: "json",
                contentType: "application/json",
                processData: false,
                success: function () {
                    app.showInfo("Plugin reloaded!");
                },
                error: function () {
                    app.showError("Plugin failed to reload.");
                }
            });
        }
    });
});