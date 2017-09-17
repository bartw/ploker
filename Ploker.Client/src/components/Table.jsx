import React from "react";
import Cards from "./Cards";
import Actions from "./Actions";
import Players from "./Players";

export default class Table extends React.Component {
  constructor(props) {
    super(props);
    this.state = { selectedCard: null, players: [] };
    const name = this.props.connection.connection.connectionId;
    this.props.connection.on("status", players => {
      const me = players.find(p => p.Name === name);
      if (me && !me.Hand) {
        this.setState(() => ({ selectedCard: null }));
      }
      this.setState(() => ({ players: players }));
    });
  }

  selectCard = value => {
    this.setState(prevState => ({
      selectedCard: prevState.selectedCard === value ? null : value
    }));
    this.props.connection.invoke(
      "setHand",
      this.state.selectedCard === value ? null : value
    );
  };

  dealMeOut = () => this.props.connection.invoke("dealMeOut");

  dealMeIn = () => this.props.connection.invoke("dealMeIn");

  reset = () => this.props.connection.invoke("reset");

  render() {
    return (
      <div>
        <Cards
          selectedCard={this.state.selectedCard}
          selectCard={this.selectCard}
        />
        <Actions
          dealMeOut={this.dealMeOut}
          dealMeIn={this.dealMeIn}
          reset={this.reset}
        />
        <Players players={this.state.players} />
      </div>
    );
  }
}
