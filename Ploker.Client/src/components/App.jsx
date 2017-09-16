import * as signalR from "@aspnet/signalr-client";
import React from "react";
import Cards from "./Cards";
import Players from "./Players";

export default class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = { selectedCard: null, players: [], loading: true };
  }

  componentDidMount() {
    this.connection = new signalR.HubConnection("/croupier");
    this.connection.on("status", players => {
      this.setState(() => ({ players: players }));
    });
    this.connection
      .start()
      .then(() => this.setState(() => ({ loading: false })));
  }

  selectCard = value => {

    this.setState(prevState => ({
      selectedCard: prevState.selectedCard === value ? null : value
    }));
    this.connection.invoke("setHand", this.state.selectedCard === value ? null : value);
  };

  dealMeOut = () => this.connection.invoke("dealMeOut");

  dealMeIn = () => this.connection.invoke("dealMeIn");

  render() {
    return (
      <div className="container">
        <h1>Ploker</h1>
        {this.state.loading ? (
          <div>loading</div>
        ) : (
          <div>
            <Cards
              selectedCard={this.state.selectedCard}
              selectCard={this.selectCard}
            />
            <div>
              <button onClick={this.dealMeOut}>Deal me out</button>
              <button onClick={this.dealMeIn}>Deal me in</button>
            </div>
            <Players players={this.state.players} />
          </div>
        )}
      </div>
    );
  }
}
