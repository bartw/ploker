import React from "react";
import Croupier from "../services/Croupier";
import Table from "./Table";

export default class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = { croupier: null, loading: true };
  }

  componentDidMount() {
    this.setState(() => ({
      croupier: new Croupier(() =>
        this.setState(() => ({ loading: false }))
      )
    }));
  }

  render() {
    return (
      <div className="container">
        <h1>Ploker</h1>
        {this.state.loading ? (
          <div>loading</div>
        ) : (
          <Table croupier={this.state.croupier} />
        )}
      </div>
    );
  }
}
