define(['jquery'], function($) {
    return {
        router: null,
        navigate: function(fragment, options) {
            this.router.navigate(fragment, options || { trigger: true });
        },
        requestCount: 0,
        showLoader: function() {
            $("#loading").show();
            this.requestCount++;
        },
        hideLoader: function() {
            this.requestCount--;
            if (this.requestCount <= 0) {
                $("#loading").hide();
            }
        },
        showError: function(message) {
            this._showMessage(message, "error");
        },
        showInfo: function(message) {
            this._showMessage(message, "info");
        },
        showSuccess: function(message) {
            this._showMessage(message, "success");
        },
        _showMessage: function(message, type) {
            $("#messages").prepend("<div class='" + type.toLowerCase() + "'>" + message + "</div>");
        }
    };
});