const baseUrl = "/api/concertArtist";

export const getAllConcertArtists = () => {
    return fetch(baseUrl).then((res) => res.json());
};

export const addConcertArtist = (concert) => {
    return fetch(baseUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(concert),
    });
};

export const getConcertArtist = (ConcertArtistId) => {
    return fetch(`${baseUrl}/${ConcertArtistId}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
        },
    }).then((res) => res.json());
};

export const replaceArtists = (concertArtists) => {
    return fetch(baseUrl + `/ClearConcertArtists/${concertArtists[0].concertId}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
        },
    }).then(() => {
        concertArtists.forEach((concertArtist) => {
            addConcertArtist(concertArtist);
        });
    });
};

export const clearConcertArtists = (concertId) => {
    return fetch(baseUrl + `/ClearConcertArtists/${concertId}`, {
        method: "DELETE",
        headers: {
            "Content-Type": "application/json",
        },
    });
};