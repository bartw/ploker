import React from "react";
import Card from "./Card";

export default function Players({ players }) {
  const playerCards = players.filter(p => !p.SittingOut).map(p => (
    <li key={p.Name}>
      <Card value={p.Hand} selected={false} select={() => {}} />
    </li>
  ));
  return (
    <div>
      <ul className="cards">{playerCards}</ul>
    </div>
  );
}
