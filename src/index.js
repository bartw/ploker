import * as signalR from "@aspnet/signalr-client";
let connection = new signalR.HubConnection("/table");

connection.on("send", data => {
  console.log(data);
});

connection.start().then(() => connection.invoke("send", "Hello"));
