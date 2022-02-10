import React, { useEffect, useState, UseState } from "react";
import { ListGroup, ListGroupItem } from "reactstrap";
import { useParams } from "react-router-dom";
import { getVenueById } from "../modules/venueManager";
import { Venue } from "./Venue";


export const VenueDetails = () => {
    const [venue, setVenue] = useState({});
    const { id } = useParams();

    useEffect(() => {
        getVenueById(id)
            .then(setVenue);
    }, [])

    return (
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-sm-12 col-lg-6">
                    <h2>{venue.name}</h2>
                    <div>{venue.venueType}</div>
                    <div>Address: {venue.location}</div>
                    <div>Capacity: {venue.capacity}</div><br></br>
                    <div>Upcoming Shows</div>
                    <p>{venue.upcomingShows}</p>

                </div>
            </div>
        </div>
    )
}