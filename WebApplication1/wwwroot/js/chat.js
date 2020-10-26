"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/hub/clinic").build();
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

connection.on("onGhostAppointmentDeleted", function (message) {
    alert("ghost app deleted\n\n" + JSON.stringify(message));
});

connection.on("onGhostAppointmentAdded", function (message) {
    alert("ghost app added\n\n" + JSON.stringify(message));
});

connection.on("onAppointmentBooked", function (message) {
    alert("app booked\n\n" + JSON.stringify(message));
});


connection.on("UpdateApptInfo", function (id, message) {
    alert("Appointment ID " + id + " changed!\n\n" + JSON.stringify(message));
    var appointm = JSON.parse(message);
    alert(document.getElementById(id));
    document.getElementById(id).textContent = "Appt ID: " + appointm.id + ", Patient: " + appointm.patientName + ", Start: " + appointm.start + ", End: " + appointm.end;
});


connection.on("AllAppointments", function (message) {
    document.getElementById("appointmentsList").innerHTML = "";
    var appoints = JSON.parse(message);
    for (var i = 0; i < appoints.length; i++) {
        var appt = appoints[i];
        var li = document.createElement("li");
        li.setAttribute("id", appt.Id);
        connection.invoke("Connect", appt.Id).catch(function (err) {
            return console.error(err.toString());
        })
        li.textContent = "Appt ID: " + appt.Id + ", Patient: " + appt.PatientName + ", Start: " + appt.Start + ", End: " + appt.End;

        document.getElementById("appointmentsList").appendChild(li);
    }
});

connection.on("ApptBooked", function (message) {
    connection.on("ApptModified", function (message) {
        connection.on("ApptDeleted", function (message) {

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
    connection.invoke("Connect", "AllAppointments").catch(function (err) {
        return console.error(err.toString());
    })
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

const url = "https://localhost:8080/api/clinic";
document.getElementById("btn").addEventListener("click", function (event) {
    var Http = new XMLHttpRequest();
    Http.addEventListener("load", function () {

        var obj = JSON.parse(Http.responseText); 
        connection.invoke("Connect", obj.id).catch(function (err) {
            return console.error(err.toString());
        })
    });
    Http.open("GET", url);
    Http.send();
    
});


