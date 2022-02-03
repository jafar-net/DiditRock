import React, { useState } from "react";
import { addConcert, updateConcert } from "../modules/concertManager";
import { useHistory, useParams } from "react-router-dom";
import { Container } from "reactstrap"
import { getConcertById } from "../modules/concertManager";

const ConcertForm = () => {

    const [concert, setConcert] = useState({
        name: "",
        venue: "",
        genre: "",
        encoresongs: "",
        date: "",
    })

    const concertId = useParams();

    if (concertId.id && concert.name == "") {
        getConcertById(concertId.id)
            .then(concert => setConcert(concert));
    }

    const handleInput = (event) => {
        const newConcert = { ...concert };
        newConcert[event.target.id] = event.target.value;
        setConcert(newConcert);
    }

    const handleCreateConcert = () => {
        addConcert(concert)

            .then(history.push("/concert"))
    }

    const handleClickUpdateConcert = () => {
        updateConcert(concert)
            .then(history.push("/concert"))
    }

    const handleClickCancel = () => {
        history.push("/concert")
    }

    const history = useHistory();

    return (
        <Container>
            <div className="concertForm">
                <h3>Add a Concert</h3>
                <div className="container-5">
                    <div className="form-group">
                        <label for="name">Name</label>
                        <input type="name" class="form-control" id="name" placeholder="name" value={concert.name} onChange={handleInput} required />
                        <label for="name">Venue</label>
                        <input type="name" class="form-control" id="name" placeholder="name" value={concert.venue?.id} onChange={handleInput} required />
                        <label for="name">Genre</label>
                        <input type="name" class="form-control" id="name" placeholder="name" value={concert.genre} onChange={handleInput} required />
                        <label for="name">Encore Songs</label>
                        <input type="name" class="form-control" id="name" placeholder="name" value={concert.encoresongs} onChange={handleInput} required />
                        <label for="name">Date</label>
                        <input type="name" class="form-control" id="name" placeholder="name" value={concert.Date} onChange={handleInput} required />
                    </div>

                    {concertId.id ?
                        <div>

                            <button type="submit" class="btn btn-primary mr-3" onClick={event => {
                                handleClickUpdateConcert()
                            }}>Update</button>

                            <button type="cancel" class="btn btn-primary mx-3" onClick={event => {
                                handleClickCancel()
                            }}>Cancel</button>

                        </div>
                        :
                        <button type="submit" class="btn btn-primary" onClick={event => {
                            handleCreateConcert()
                        }}>Create</button>
                    }
                </div>
            </div>
        </Container>
    )
}

export default ConcertForm;