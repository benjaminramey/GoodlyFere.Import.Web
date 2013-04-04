define({
    router: null,
    navigate: function(fragment, options) {
        this.router.navigate(fragment, options || { trigger: true });
    },
    showError: function(message) {
        _showMessage(message, "error");
    },
    showInfo: function (message) {
        _showMessage(message, "info");
    },
    _showMessage: function(message, type) {
        $("#messages").append("<div class='"+type.toLowerCase()+"'>"+type.toUpperCase()+": " + message + "</div>");
    }
});