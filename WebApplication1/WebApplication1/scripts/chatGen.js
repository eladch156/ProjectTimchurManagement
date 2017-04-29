/***
$(function () {

    var notifications = $.connection.notificationsHub;
    notifications.client.broadcastNotification = function (message) {
     alert(message);
       
       
    };
    $.connection.hub.start().done(function () {
        notifications.server.sendNotification();

    }).fail(function () {
        alert("Connection failed");
    });

});**/