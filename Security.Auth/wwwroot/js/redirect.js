var linkElement = document.querySelector("a.PostLogoutRedirectUri");
var countElement = document.querySelector("#count");
var count = 5;

var intervalId = setInterval(function () {
    if (count > 0) {
        countElement.innerHTML = --count;
    } else {
        clearInterval(intervalId);
        linkElement.click();
    }

}, 1000);