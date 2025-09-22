"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/notificationHub")
    .build();

let count = 0;
let announcements = [];


document.getElementById("sendAnnouncementBtn").addEventListener("click", function () {
    const title = document.getElementById("announcementTitle").value;
    const content = document.getElementById("announcementContent").value;

    if (!title || !content) {
        alert("Please enter both title and content.");
        return;
    }

    fetch(`/Home/SendAnnouncement?title=${encodeURIComponent(title)}&content=${encodeURIComponent(content)}`)
        .then(response => {
            if (response.ok) {
                alert("✅ Announcement sent!");
                document.getElementById("announcementTitle").value = "";
                document.getElementById("announcementContent").value = "";
            } else {
                alert("❌ Failed to send announcement.");
            }
        })
        .catch(error => console.error("Error:", error));
});


connection.on("ReceiveAnnouncement", function (title, content) {
    count++;
    document.getElementById("notificationCount").textContent = count;

    announcements.push({ title, content });
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("showListBtn").addEventListener("click", function () {
    const list = document.getElementById("announcementList");
    list.innerHTML = "";



    announcements.forEach(a => {
        const li = document.createElement("li");
        li.innerHTML = `<strong>${a.title}</strong>: ${a.content}`;
        list.appendChild(li);
    });
});
