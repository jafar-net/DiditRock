import React from "react";
import "../css/Home.css"

export default function Home() {
    return (
        <body className="home">
            <span className="home" key="home" style={{
                position: "fixed",
                left: 0,
                right: 0,
                top: "50%",
                marginTop: "-0.5rem",
                textAlign: "center",

            }}><h1>Welcome to Did it Rock!</h1></span>
        </body>

    );
}