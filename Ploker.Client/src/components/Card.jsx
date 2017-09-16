import React from "react";

export default function Card({ value, selected, select }) {
  const classNames = "card" + (selected ? " selected" : "");
  return (
    <div className={classNames} onClick={() => select(value)}>
      <span>{value}</span>
    </div>
  );
}
