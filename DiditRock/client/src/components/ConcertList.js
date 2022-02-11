import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { Row, Col, Button } from "reactstrap";
import { Concert } from "./Concert";
import { getAllConcerts } from "../modules/concertManager";
import "../css/Concert.css"

export const ConcertList = () => {
    const [concerts, setConcerts] = useState([]);

    const getConcerts = () => {
        getAllConcerts().then((concerts) => setConcerts(concerts));
    };

    const history = useHistory();

    useEffect(() => {
        getConcerts();
    }, []);

    return (
        <div className="container">
            <div className="justify-content-center">
                <Row xs="3">
                    <Col>
                        <h1 className="reviews">Concerts</h1>
                    </Col>
                </Row>
                <div>
                    {concerts.map((concert) => (
                        <Concert
                            concert={concert}
                            key={concert.id}
                            setConcerts={setConcerts}
                            getConcerts={getConcerts}
                        />
                    ))}
                </div>
                <br></br>
                <Col className="mt-3">
                    <Button
                        className="addConcertButton"
                        onClick={() => {
                            history.push("/concert/add");
                        }}
                        color="primary"
                    >
                        Add a Concert
                    </Button>
                </Col>
            </div>
        </div>
    );
};