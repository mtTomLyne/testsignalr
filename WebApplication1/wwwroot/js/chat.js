"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chathub").build();
var oldValue = "";

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;
document.getElementById("btn").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

connection.on("UpdateMessage", function (message) {
    document.getElementById("messageInput").value = message;
});

document.getElementById("roomId").addEventListener("input", function (event) {

    connection.invoke("Disconnect", oldValue).catch(function (err) {
        return console.error(err.toString());
    })

    connection.invoke("Connect", this.value).catch(function (err) {
        return console.error(err.toString());
    })

    oldValue = this.value;
});

document.getElementById("messageInput").addEventListener("input", function (event) {
    var message = document.getElementById("messageInput").value;
    var roomId = document.getElementById("roomId").value;
    connection.invoke("UpdateMessage", message, roomId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
    document.getElementById("btn").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value;
    var roomId = document.getElementById("roomId").value;
    connection.invoke("SendMessage", user, message, roomId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

const url = "https://localhost:44342/api/appointments";
const data = {
    name: "Said",
    id:23
}

document.getElementById("btn").addEventListener("click", function (event) {
    alert("wow");
    const Http = new XMLHttpRequest();
    Http.open("POST", url);
    Http.send();
});