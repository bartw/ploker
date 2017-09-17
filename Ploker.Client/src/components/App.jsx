import * as signalR from "@aspnet/signalr-client";
import React from "react";
import Table from "./Table";

export default class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = { connection: null };
  }

  componentDidMount() {
    const connection = new signalR.HubConnection("/croupier");
    connection
      .start()
      .then(() => this.setState(() => ({ connection: connection })));
  }

  render() {
    return (
      <div className="container">
        <h1>Ploker</h1>
        {this.state.connection ? (
          <Table connection={this.state.connection} />
        ) : (
          <div>loading</div>
        )}
      </div>
    );
  }
}
