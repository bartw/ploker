import React from "react";
import Cards from "./Cards";
import Actions from "./Actions";
import Players from "./Players";

export default class Table extends React.Component {
  constructor(props) {
    super(props);
    this.state = { selectedCard: null, players: [] };

    this.props.croupier.setOnStatus(players => {
      const me = players.find(
        p => p.Name === this.props.croupier.getPlayerName()
      );
      if (!me || !me.Hand) {
        this.setState(() => ({ selectedCard: null }));
      }
      this.setState(() => ({ players: players }));
    });
  }

  selectCard = value => {
    this.setState(prevState => ({
      selectedCard: prevState.selectedCard === value ? null : value
    }));
    this.props.croupier.setHand(
      this.state.selectedCard === value ? null : value
    );
  };

  render() {
    return (
      <div>
        <Cards
          selectedCard={this.state.selectedCard}
          selectCard={this.selectCard}
        />
        <Actions
          dealMeOut={this.props.croupier.dealMeOut}
          dealMeIn={this.props.croupier.dealMeIn}
          reset={this.props.croupier.reset}
        />
        <Players players={this.state.players} />
      </div>
    );
  }
}
