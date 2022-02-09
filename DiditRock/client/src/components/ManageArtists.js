import React, { useEffect, useState } from "react";
import ConcertArtist from "./ConcertArtist";
import { getArtists, getArtistsByConcertId, } from "../modules/artistManager";
import { replaceArtists, clearConcertArtists } from "../modules/concertArtistManager";
import { useHistory, useParams } from "react-router-dom";
import { Button } from "reactstrap";

const ManageArtists = () => {
    const { id } = useParams();

    const history = useHistory();
    const [artists, setArtists] = useState([]);
    const [activeArtistIds, setActiveArtistIds] = useState([]);

    let isAdmin = localStorage.getItem("LoggedInUserType") == 1;
    const getAllArtists = () => {
        getArtists().then((artists) => setArtists(artists));
    };

    useEffect(() => {
        getAllArtists();
        getArtistsByConcertId(id).then((concertArtists) => {
            setActiveArtistIds(concertArtists.map((vt) => vt.artistId));
        });
    }, []);

    const handleSave = () => {
        if (activeArtistIds.length > 0)
            replaceArtists(
                activeArtistIds.map((artistId) => {
                    return { artistId: artistId, concertId: parseInt(id) };
                })
            );
        else clearConcertArtists(id);
        setTimeout(() => {
            history.push(`/`);
        }, 500);
    };

    const handleArtistSelected = (event) => {
        event.preventDefault();
        const newId = parseInt(event.target.id.split("--")[1]);

        const activeArtistIdsCopy = [...activeArtistIds];
        // set new list of artists by filtering out a artist that already exists else adding a new id to the list
        if (activeArtistIdsCopy.includes(newId)) {
            setActiveArtistIds([
                ...activeArtistIdsCopy.filter((artist) => artist != newId),
            ]);
        } else {
            activeArtistIdsCopy.push(newId);
            setActiveArtistIds(activeArtistIdsCopy);
        }
    };

    return (
        <div className="container">
            <h1>Artists</h1>
            <div className="mt-2">
                <div>
                    <div>
                        <p>Artist Name</p>

                    </div>
                </div>
                <div>
                    {artists.map((concertArtist) => (
                        <ConcertArtist
                            concertArtist={concertArtist}
                            key={concertArtist.id}
                            handleArtistSelected={handleArtistSelected}
                            activeArtistIds={activeArtistIds}
                        />
                    ))}
                </div>
            </div>
            <Button
                color="secondary"
                className="ManageArtists__save"
                onClick={handleSave}
            >
                Save
            </Button>
        </div>
    );
};

export default ManageArtists;