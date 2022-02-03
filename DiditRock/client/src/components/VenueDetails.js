import React, { useEffect, useState, UseState } from "react";
import { ListGroup, ListGroupItem } from "reactstrap";
import { useParams } from "react-router-dom";
import { getVenueById } from "../modules/venueManager";
import Venue from "./Venue";


export const VenueDetails = () => {
    const [venue, setVenue] = useState({});
    const { id } = useParams();

    const date = new Date(venue.createDateTime).toDateString()

    useEffect(() => {
        getVenueById(id)
            .then(setVenue);
    }, [])

    return (
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-sm-12 col-lg-6">
                    {/* <Venue venue={venue} /> */}

                    <h2>{venue.name}</h2>
                    <div>{venue.Location}</div>
                    <div>{venue.VenueType}</div>
                    <div>{venue.Capacity} {date}</div><br></br>
                    <p>{venue.UpcomingShows}</p>

                </div>
            </div>
        </div>
    )
}