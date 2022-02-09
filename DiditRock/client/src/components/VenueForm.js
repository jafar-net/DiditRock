import React, { useState, useEffect } from "react";
import { addVenue, updateVenue } from "../modules/venueManager";
import { useHistory, useParams } from "react-router-dom";
import { Container, Input } from "reactstrap"
import { getVenueById } from "../modules/venueManager";

const VenueForm = () => {

    const [venue, setVenue] = useState({
        name: "",
        type: "",
        address: "",
        capacity: "",
        upcomingShows: "",
    })

    const venueId = useParams();

    if (venueId.id && venue.name == "") {
        getVenueById(venueId.id)
            .then(venue => setVenue(venue));
    }

    const handleInput = (event) => {
        const newVenue = { ...venue };
        newVenue[event.target.id] = event.target.value;
        setVenue(newVenue);
    }

    const handleClickCreateVenue = () => {
        addVenue(venue)

            .then(history.push("/venue"))
    }

    const handleClickUpdateVenue = () => {
        updateVenue(venue)
            .then(history.push("/venue"))
    }

    const handleClickCancel = () => {
        history.push("/venue")
    }

    const history = useHistory();

    return (
        <Container>
            <div className="venueForm">
                <h3>Add a Venue</h3>
                <div className="container-5">
                    <div className="form-group">

                        <label for="name">Name</label>
                        <Input type="name" class="form-control" id="title" placeholder="Venue Name" value={venue.name} onChange={handleInput} required />

                        <label for="content">Type</label>
                        <Input type="textarea-lg" class="form-control" id="content" placeholder="Type of Venue" value={venue.type} onChange={handleInput} required />

                        <label for="imageLocation">Address</label>
                        <Input type="url" class="form-control" id="imageLocation" placeholder="Address" value={venue.address} onChange={handleInput} required />

                        <label for="name">Capacity</label>
                        <Input type="datetime-local" class="form-control" id="capacity" placeholder="Capacity" value={venue.capacity} onChange={handleInput} required />

                        <label for="category">Upcoming Shows</label>
                        <Input type="select" name="select" value={venue.upcomingShows} id="categoryId" onChange={handleInput}>
                        </Input>
                    </div>
                    {venueId.id ?
                        <div>
                            <button type="submit" class="btn btn-primary mr-3" onClick={event => {
                                handleClickUpdateVenue()
                            }}>Update</button>
                        </div>
                        :
                        <div>
                            <button type="submit" class="btn btn-primary mr-3" onClick={event => {
                                handleClickCreateVenue()
                            }}>Create</button>
                        </div>
                    }
                    <div>
                        <button type="cancel" class="btn btn-primary mx-3" onClick={event => {
                            handleClickCancel()
                        }}>Cancel</button>
                    </div>
                </div>
            </div>
        </Container>
    )
}

export default VenueForm;