import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { Row, Col, Button } from "reactstrap";
import { Artist } from "./Artist";
import { getAllArtists } from "../modules/artistManager";

export const ArtistList = () => {
    const [artists, setArtists] = useState([]);

    const getArtists = () => {
        getAllArtists().then((artists) => setArtists(artists));
    };

    const history = useHistory();

    useEffect(() => {
        getArtists();
    }, []);

    return (
        <div className="container">
            <div className="justify-content-center">
                <Row xs="3">
                    <Col>
                        <h1>Artists</h1>
                    </Col>
                    <Col className="mt-3">
                        <Button
                            className="addArtistButton"
                            onClick={() => {
                                history.push("/artist/add");
                            }}
                            color="primary"
                        >
                            Add a Artist
                        </Button>
                    </Col>
                </Row>
                <div>
                    {artists.map((artist) => (
                        <Artist
                            artist={artist}
                            key={artist.id}
                            setArtists={setArtists}
                            getArtists={getArtists}
                        />
                    ))}
                </div>
            </div>
        </div>
    );
};