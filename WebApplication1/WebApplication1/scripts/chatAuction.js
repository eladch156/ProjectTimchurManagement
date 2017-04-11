
$(function () {
    var notifications = $.connection.notificationsHub;
    notifications.client.sendMessage = function (id) {
        if (id == "-1") {
            window.location = "MainIndex";
            return;
        }
        setTimeout(function () {
            window.location = "EditAuction/" + id;
        }, 1000);
    };
    $.connection.hub.start().done(function () {
        if (sessionStorage.getItem("is_reloaded"))
            notifications.server.addAuctionOperation();
        sessionStorage.setItem("is_reloaded", true);

    }).fail(function () {
        alert("Connection failed");
    });

});