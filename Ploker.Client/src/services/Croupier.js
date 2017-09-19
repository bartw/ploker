import * as signalR from "@aspnet/signalr-client";

export default class Croupier {
  constructor(onStarted) {
    const connection = new signalR.HubConnection("/croupier");
    connection.start().then(onStarted);

    this.setOnStatus = onStatus => {
      connection.on("status", onStatus);
    };

    this.createTable = () => {
      connection.invoke("createTable");
    };

    this.joinTable = id => {
      connection.invoke("joinTable", id);
    }

    this.setHand = (id, hand) => {
      connection.invoke("setHand", id, hand);
    };

    this.dealMeOut = id => {
      connection.invoke("dealMeOut", id);
    };

    this.dealMeIn = id => {
      connection.invoke("dealMeIn", id);
    };

    this.reset = id => {
      connection.invoke("reset", id);
    };

    this.getPlayerName = () => connection.connection.connectionId;
  }
}
