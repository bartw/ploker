import * as signalR from "@aspnet/signalr-client";
import React from "react";
import Cards from "./Cards";

export default class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = { selectedCard: null, players: [] };
  }

  componentDidMount() {
    this.connection = new signalR.HubConnection("/croupier");
    this.connection.on("send", players => {
      this.setState(() => ({ players: players }));
    });
    this.connection.start().then(() => this.connection.invoke("send", ""));
  }

  selectCard = value => {
    this.setState(prevState => ({
      selectedCard: prevState.selectedCard === value ? null : value
    }));
    this.connection.invoke("send", value);
  };

  render() {
    const players = this.state.players.map(p => (
      <li key={p.Name}>
        {p.Name}: {p.Hand}
      </li>
    ));
    return (
      <div className="container">
        <h1>Ploker</h1>
        <Cards
          selectedCard={this.state.selectedCard}
          selectCard={this.selectCard}
        />
        <div>
          <p>Selected card:</p>
          <p>{this.state.selectedCard}</p>
        </div>
        <div>
          <p>Players</p>
          {players}
        </div>
      </div>
    );
  }
}
