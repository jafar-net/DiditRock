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

    const handleClickVenueForm = () => {
        history.push("/venue/create");
    };

    return (
        <div className="container">
            <Row>
                <Col xs={{ size: 2, offset: 1 }}>
                    <h1>Venues</h1>
                </Col>
                <Col xs={{ size: 2, offset: 0.5 }}>
                    <button
                        className="btn-primary"
                        name="venueForm"
                        onClick={handleClickVenueForm}
                    >
                        Add a Venue{" "}
                    </button>
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
    );
};