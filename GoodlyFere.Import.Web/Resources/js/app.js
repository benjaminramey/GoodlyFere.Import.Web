define({
    router: null,
    navigate: function(fragment, options) {
        this.router.navigate(fragment, options || { trigger: true });
    },
    showError: function(message) {
        $("#messages").html("ERROR: " + message);
    },
    showInfo: function (message) {
        $("#messages").html("INFO: " + message);
    }
});