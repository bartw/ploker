import * as signalR from "@aspnet/signalr-client";
let connection = new signalR.HubConnection("/table");

connection.on("send", data => {
  console.log(data);
});

const send = value => connection.invoke("send", value);

connection.start().then(() => send(""));

const registerClickFor = id => document.getElementById(id).addEventListener("click", () => send(id));

registerClickFor("one");
registerClickFor("two");
registerClickFor("three");
registerClickFor("five");
