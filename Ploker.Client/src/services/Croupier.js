import * as signalR from "@aspnet/signalr-client";

export default class Croupier {
  constructor(onStarted) {
    const connection = new signalR.HubConnection("/croupier");
    connection.start().then(onStarted);

    this.setOnStatus = onStatus => {
      connection.on("status", onStatus);
    };

    this.setHand = hand => {
      connection.invoke("setHand", hand);
    };

    this.dealMeOut = () => {
      connection.invoke("dealMeOut");
    };

    this.dealMeIn = () => {
      connection.invoke("dealMeIn");
    };

    this.reset = () => {
      connection.invoke("reset");
    };

    this.getPlayerName = () => connection.connection.connectionId;
  }
}
