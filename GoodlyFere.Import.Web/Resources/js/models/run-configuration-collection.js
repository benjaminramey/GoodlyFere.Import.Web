define(['backbone', 'models/run-configuration-model'], function(Backbone, RunConfigModel) {
    return Backbone.Collection.extend({
        model: RunConfigModel,
        url: "/api/runconfigurations",
        
        fetchForProject: function (projectId) {
            var collection = this;
            $.ajax({
                url: this.url,
                method: "GET",
                data: { projectId: projectId },
                success: function(result) {
                    collection.reset(result);
                }
            });
        }
    });
});