import React from "react";
import { Button } from "reactstrap";
import { useState, useEffect } from "react";

const ConcertArtist = ({ concertArtist, handleArtistSelected, activeArtistIds }) => {
    const [isArtistOnConcert, setIsArtistOnConcert] = useState(false);

    useEffect(() => {
        setIsArtistOnConcert(
            activeArtistIds.length > 0 && activeArtistIds.includes(concertArtist.id)
        );
    }, [activeArtistIds]);

    return (
        <div>
            <p>{concertArtist.name}</p>
            <Button
                id={`manageArtists--${concertArtist.id}`}
                onClick={handleArtistSelected}
                color={isArtistOnConcert ? "danger" : "primary"}
            >
                {isArtistOnConcert ? "Remove" : "Add Artist"}
            </Button>
        </div>
    );
};

export default ConcertArtist;