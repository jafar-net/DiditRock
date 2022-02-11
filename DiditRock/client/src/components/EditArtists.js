import React, { useState } from "react";
import { useHistory } from "react-router";
import { getArtistById, getArtists, updateArtist } from "../modules/artistManager";
import { useEffect } from "react";
import { useParams } from "react-router-dom";

const EditArtist = () => {
    const [artist, setArtist] = useState({
        name: "",
    });
    const { id } = useParams();

    const history = useHistory();

    useEffect((event) => {
        getArtistById(id).then((res) => {
            setArtist(res);
        });
    }, []);

    const handleControlledInputChange = (event) => {
        const newArtist = { ...artist };
        let selectedVal = event.target.value;

        newArtist[event.target.id] = selectedVal;
        // update state
        setArtist(newArtist);
    };

    const handleConfirm = (event) => {
        event.preventDefault();

        updateArtist(artist).then(() => history.push("/artistlist"));
    };
    console.log(artist.name)
    return (
        <form className="main-content">
            <h2 className="_title">Edit Artist:</h2>
            <fieldset className="fieldset">
                <div className="form-group">
                    <label htmlFor="name">Artist name:</label>
                    <input
                        type="text"
                        id="name"
                        onChange={handleControlledInputChange}
                        required
                        autoFocus
                        className="form-control"
                        value={artist.name}
                    />
                </div>
            </fieldset>
            <button
                className="btn-add-delete"
                variant="primary"
                onClick={handleConfirm}
            >
                Submit
            </button>
            <button
                className="btn-add-edit"
                variant="secondary"
                onClick={() => history.push("/artistlist")}
            >
                Cancel
            </button>
        </form>
    );
};

export default EditArtist;