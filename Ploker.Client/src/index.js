import React from "react";
import { render } from "react-dom";
import App from "./components/App";
import "reset-css/reset.css";
import "./style.css";

render(<App />, document.getElementById("root"));

/*
import * as signalR from "@aspnet/signalr-client";
let connection = new signalR.HubConnection("/croupier");

connection.on("send", data => {
  console.log(data);
});

const send = value => connection.invoke("send", value);

connection.start().then(() => send(""));

const registerClickFor = id => document.getElementById(id).addEventListener("click", () => send(id));

registerClickFor("1");
registerClickFor("2");
registerClickFor("3");
registerClickFor("5");
*/