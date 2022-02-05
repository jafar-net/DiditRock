import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { Row, Col, Button } from "reactstrap";
import { Artist } from "./Artist";
import { getArtists } from "../modules/artistManager";

export const ArtistList = () => {
    const [artists, setArtists] = useState([]);

    const getArtist = () => {
        getArtists().then((artists) => setArtists(artists));
    };

    const history = useHistory();

    useEffect(() => {
        getArtist();
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
                            getArtist={getArtist}
                        />
                    ))}
                </div>
            </div>
        </div>
    );
};