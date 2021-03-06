﻿$(function () {
    var notifications = $.connection.notificationsHub;
    notifications.client.sendMessage = function (id) {
        if (id == "-1") {
            window.location = "MainIndex";
            return;
        }
        setTimeout(function () {
            window.location = "EditUnit/" + id;
        }, 1000);
    };
    $.connection.hub.start().done(function () {
       
        if (sessionStorage.getItem("is_reloaded"))
            notifications.server.editUnitOperation();
        sessionStorage.setItem("is_reloaded", true);
    }).fail(function () {
        alert("Connection failed");
    });

});