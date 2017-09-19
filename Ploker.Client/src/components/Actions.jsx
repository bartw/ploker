import React from "react";

export default function Actions({ dealMeOut, dealMeIn, reset, sittingOut }) {
  return (
    <div className="actions">
      {!sittingOut && <button onClick={dealMeOut}>Deal me out</button>}
      {sittingOut && <button onClick={dealMeIn}>Deal me in</button>}
      <button onClick={reset}>Reset</button>
    </div>
  );
}
