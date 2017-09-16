import React from "react";
import Card from "./Card";

export default function Cards({ selectedCard, selectCard }) {
  const cards = [
    /*"?",*/ "1",
    "2",
    "3",
    "5",
    "8",
    "13",
    "20" /*, "Coffee"*/
  ].map(c => (
    <li key={c}>
      <Card value={c} selected={selectedCard == c} select={selectCard} />
    </li>
  ));
  return <ul className="cards">{cards}</ul>;
}
