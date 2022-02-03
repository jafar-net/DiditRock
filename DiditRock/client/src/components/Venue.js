import React from "react";
import { Card, CardBody, Row, Button, Col } from "reactstrap"
import { deleteVenue, getAllVenues } from "../modules/venueManager";
import { Link, useHistory } from "react-router-dom";

export const Venue = ({ venue, setVenues }) => {

    const history = useHistory();

    const handleClickDeleteVenue = () => {
        const confirm = window.confirm("Are you sure you want to delete this venue?")
        if (confirm == true) {
            deleteVenue(venue)
                .then(getAllVenues().then(venues => setVenues(venues)))
        } else {
            return;
        }
    }

    const handleClickEditTag = () => {
        history.push(`/venue/edit/${venue.id}`)
    }

    return (
        <Card>
            <CardBody>
                <Link to={`/venue/${venue.id}`}>
                    <strong>{venue.name}</strong>
                </Link>
            </CardBody>
        </Card>
    )
}
