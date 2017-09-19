import React from "react";
import Cards from "./Cards";
import Actions from "./Actions";
import Players from "./Players";

export default function Table({id, selectedCard, selectCard, dealMeOut, dealMeIn, reset, players, sittingOut}) {
  return (
    <div>
      <p>{id}</p>
      <Cards
        selectedCard={selectedCard}
        selectCard={selectCard}
      />
      <Actions
        dealMeOut={dealMeOut}
        dealMeIn={dealMeIn}
        reset={reset}
        sittingOut={sittingOut}
      />
      <Players players={players} />
    </div>
  );
}
