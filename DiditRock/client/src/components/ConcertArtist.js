import React from "react";
import { Button } from "reactstrap";
import { useState, useEffect } from "react";

const ConcertArtist = ({ concertArtist, handleArtistSelected, activeArtistIds }) => {
    const [isArtistgedToConcert, setIsArtistgedToConcert] = useState(false);

    useEffect(() => {
        setIsArtistgedToConcert(
            activeArtistIds.length > 0 && activeArtistIds.includes(concertArtist.id)
        );
    }, [activeArtistIds]);

    return (
        <div>
            <p>{concertArtist.name}</p>
            <Button
                id={`manageArtists--${concertArtist.id}`}
                onClick={handleArtistSelected}
                color={isArtistgedToConcert ? "danger" : "primary"}
            >
                {isArtistgedToConcert ? "Remove" : "Add Artist"}
            </Button>
        </div>
    );
};

export default ConcertArtist;