import React, { useState } from 'react';

const styles = {
    "tile-button": {
        backgroundColor: "#e0e0e0",
        border: "1px solid #ccc",
        padding: "10px",
        margin: "5px",
        cursor: "pointer",
        outline: "none"
    },
    "tile-button.selected": {
        backgroundColor: "#a0a0a0",
        color: "white",
    }
}

const TileButton = ({ id, title, selected, onClick }) => {
    const buttonStyle = selected ? { ...styles["tile-button"], ...styles["tile-button.selected"] } : styles["tile-button"];

    return (
        <button
            id={id}
            style={buttonStyle}
            onClick={() => onClick(id)}  
        >
            {title}
        </button>
    );
};


export default TileButton;