import React, { useState } from "react";
import { addConcert, updateConcert } from "../modules/concertManager";
import { useHistory, useParams } from "react-router-dom";
import { Container, Input } from "reactstrap"
import { getConcertById } from "../modules/concertManager";
import { getAllVenues } from "../modules/venueManager";
import { useEffect } from "react";
import { getArtists } from "../modules/artistManager";

const ConcertForm = () => {

    const history = useHistory();

    const [Venues, setVenues] = useState([]);

    const [Artists, setArtists] = useState([]);

    const [concert, setConcert] = useState({
        name: "",
        venueId: "",
        artistId: "",
        genre: "",
        encoresongs: "",
        date: "",
    })

    const concertId = useParams();

    if (concertId.id && concert.name == "") {
        getConcertById(concertId.id)
            .then(concert => setConcert(concert));
    }

    const getVenues = () => {
        getAllVenues().then(Venues => setVenues(Venues));
    };

    const getArtist = () => {
        getArtists().then(Artists => setArtists(Artists));
    };

    useEffect(() => {
        getVenues();
    }, []);

    useEffect(() => {
        getArtist();
    }, []);

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


    return (
        <Container>
            <div className="concertForm">
                <h3>Add a Concert</h3>
                <div className="container-5">
                    <div className="form-group">
                        <label htmlFor="name">Name</label>
                        <input type="name" className="form-control" id="name" placeholder="Tour Name" value={concert.name} onChange={handleInput} required />
                        <label htmlFor="venue">Venue</label>
                        <Input type="select" name="select" value={concert.venueId} id="venueId" onChange={handleInput}>
                            <option key={0} value={null}>Select a Venue</option>
                            {Venues.map(v => {
                                return <option key={v.id} value={v.id}>{v.name}</option>
                            })}
                        </Input>
                        <Input type="select" name="select" value={concert.artistId} id="artistId" onChange={handleInput}>
                            <option key={0} value={null}>Select an Artist</option>
                            {Artists.map(a => {
                                return <option key={a.id} value={a.id}>{a.name}</option>
                            })}
                        </Input>
                        <label htmlFor="name">Genre</label>
                        <input type="name" className="form-control" id="genre" placeholder="Genre" value={concert.genre} onChange={handleInput} required />
                        <label htmlFor="name">Encore Songs</label>
                        <input type="name" className="form-control" id="encoresongs" placeholder="Encore Songs" value={concert.encoresongs} onChange={handleInput} required />
                        <label htmlFor="name">Date</label>
                        <input type="date" className="form-control" id="date" placeholder="Date" value={concert.date} onChange={handleInput} required />
                    </div>

                    {concertId.id ?
                        <div>

                            <button type="submit" className="btn btn-primary mr-3" onClick={event => {
                                handleClickUpdateConcert()
                            }}>Update</button>

                            <button type="cancel" className="btn btn-primary mx-3" onClick={event => {
                                handleClickCancel()
                            }}>Cancel</button>

                        </div>
                        :
                        <button type="submit" className="btn btn-primary" onClick={event => {
                            handleCreateConcert()
                        }}>Create</button>
                    }
                </div>
            </div>
        </Container>
    )
}

export default ConcertForm;