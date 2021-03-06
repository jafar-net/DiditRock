import React, { useState } from "react";
import { addArtist, updateArtist } from "../modules/artistManager";
import { useHistory, useParams } from "react-router-dom";
import { Container } from "reactstrap"
import { getArtistById } from "../modules/artistManager";

const ArtistForm = () => {

    const [artist, setArtist] = useState({
        name: ""
    })

    const artistId = useParams();

    if (artistId.id && artist.name == "") {
        getArtistById(artistId.id)
            .then(artist => setArtist(artist));
    }

    const handleInput = (event) => {
        const newArtist = { ...artist };
        newArtist[event.target.id] = event.target.value;
        setArtist(newArtist);
    }

    const handleCreateArtist = () => {
        addArtist(artist)

            .then(history.push("/artist"))
    }

    const handleClickUpdateArtist = () => {
        updateArtist(artist)
            .then(history.push("/artist"))
    }

    const handleClickCancel = () => {
        history.push("/artist")
    }

    const history = useHistory();

    return (
        <Container>
            <div className="artistForm">
                <h3 className="reviews">Add an Artist</h3>
                <div className="container-5">
                    <div className="form-group">
                        <label htmlFor="name">Name</label>
                        <input type="name" className="form-control" id="name" placeholder="Name" value={artist.name} onChange={handleInput} required />
                    </div>

                    {artistId.id ?
                        <div>

                            <button type="submit" className="btn btn-primary mr-3" onClick={event => {
                                handleClickUpdateArtist()
                            }}>Update</button>

                            <button type="cancel" className="btn btn-primary mx-3" onClick={event => {
                                handleClickCancel()
                            }}>Cancel</button>

                        </div>
                        :
                        <button type="submit" className="btn btn-primary" onClick={event => {
                            handleCreateArtist()
                        }}>Create</button>
                    }
                </div>
            </div>
        </Container>
    )
}

export default ArtistForm;