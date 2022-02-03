import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { Row, Col, Button } from "reactstrap";
import { Venue } from "./Venue";
import { getAllVenues } from "../modules/venueManager";

export const VenueList = () => {
    const [venues, setVenues] = useState([]);

    const getVenues = () => {
        getAllVenues().then((venues) => setVenues(venues));
    };

    const history = useHistory();

    useEffect(() => {
        getVenues();
    }, []);

    return (
        <div className="container">
            <div className="justify-content-center">
                <Row xs="3">
                    <Col>
                        <h1>Venues</h1>
                    </Col>
                    <Col className="mt-3">
                        <Button
                            className="addVenueButton"
                            onClick={() => {
                                history.push("/venue/add");
                            }}
                            color="primary"
                        >
                            Add a Venue
                        </Button>
                    </Col>
                </Row>
                <div>
                    {venues.map((venue) => (
                        <Venue
                            venue={venue}
                            key={venue.id}
                            setVenues={setVenues}
                            getVenues={getVenues}
                        />
                    ))}
                </div>
            </div>
        </div>
    );
};