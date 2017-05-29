/*
Originally found at https://cssdeck.com/labs/login-form-using-html5-and-css3

by: https://cssdeck.com/user/kamalchaneman


Free Source Confirmed

*/
window.addEventListener('load', function (event) {
    var caps = event.getModifierState && event.getModifierState('CapsLock');
    if (caps) {
        document.getElementById("ca_indi").innerHTML = "Caps Lock is : On";
        document.getElementById("ca_indi").style.color = "green";
    }
    else {
        document.getElementById("ca_indi").innerHTML = "Caps Lock is : Off";
        document.getElementById("ca_indi").style.color = "red";
    }
});
document.addEventListener('keydown', function (event) {
    var caps = event.getModifierState && event.getModifierState('CapsLock');
    if (caps) {
        document.getElementById("ca_indi").innerHTML = "Caps Lock is : On";
        document.getElementById("ca_indi").style.color = "green";
    }
    else {
        document.getElementById("ca_indi").innerHTML = "Caps Lock is : Off";
        document.getElementById("ca_indi").style.color = "red";
    }
});