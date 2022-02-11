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
                    <h1 className="reviews">Venues</h1>
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
            <br></br>
            <Col className="mt-3">
                <Button
                    className="addVenueButton"
                    name="venueForm"
                    onClick={handleClickVenueForm}
                    color="primary"
                >
                    Add a Venue{" "}
                </Button>
            </Col>
        </div>
    );
};