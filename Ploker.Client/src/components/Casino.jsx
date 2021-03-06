import React from "react";
import Table from "./Table";

export default class Casino extends React.Component {
  constructor(props) {
    super(props);
    this.state = { selectedCard: null, players: [], id: null, toJoin: "" };

    this.props.croupier.setOnStatus(table => {
      if (table) {
        const me = table.Players.find(
          p => p.Name === this.props.croupier.getPlayerName()
        );
        if (!me || !me.Hand || me.SittingOut) {
          this.setState(() => ({ selectedCard: null }));
        }
        this.setState(() => ({ players: table.Players, id: table.Id }));
      }
      var tableId = window.location.hash.substr(1);

      if (table && tableId != table.Id) {
        window.history.pushState(
          "",
          "",
          "#" + table.Id
        );
      } else if (!table && tableId) {
        this.props.croupier.joinTable(tableId);
      }
    });
  }

  selectCard = value => {
    this.setState(prevState => ({
      selectedCard: prevState.selectedCard === value ? null : value
    }));
    this.props.croupier.setHand(
      this.state.id,
      this.state.selectedCard === value ? null : value
    );
  };

  dealMeOut = () => this.props.croupier.dealMeOut(this.state.id);
  dealMeIn = () => this.props.croupier.dealMeIn(this.state.id);
  reset = () => this.props.croupier.reset(this.state.id);
  joinTable = () => {
    if (this.state.toJoin) {
      this.props.croupier.joinTable(this.state.toJoin);
    }
  };
  onChangeToJoin = event => this.setState({ toJoin: event.target.value });

  render() {
    const me = this.state.players.find(
      p => p.Name === this.props.croupier.getPlayerName()
    );
    const sittingOut = me && me.SittingOut;
    return (
      <div>
        {this.state.id && (
          <Table
            id={this.state.id}
            selectedCard={this.state.selectedCard}
            selectCard={this.selectCard}
            dealMeOut={this.dealMeOut}
            dealMeIn={this.dealMeIn}
            reset={this.reset}
            players={this.state.players}
            sittingOut={sittingOut}
          />
        )}
        {!this.state.id && (
          <div>
            <div>
              <button onClick={this.props.croupier.createTable}>
                Create new table
              </button>
            </div>
            <div>
              <input
                type="text"
                value={this.state.toJoin}
                onChange={this.onChangeToJoin}
              />
              <button onClick={this.joinTable}>Join table</button>
            </div>
          </div>
        )}
      </div>
    );
  }
}
