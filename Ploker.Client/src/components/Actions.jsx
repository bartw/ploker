import React from "react";

export default function Actions({ dealMeOut, dealMeIn, reset }) {
  return (
    <div className="actions">
      <button onClick={dealMeOut}>Deal me out</button>
      <button onClick={dealMeIn}>Deal me in</button>
      <button onClick={reset}>Reset</button>
    </div>
  );
}
