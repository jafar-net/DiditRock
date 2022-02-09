const _apiUrl = '/api/artist';

export const getArtists = () => {
    return fetch(_apiUrl)
        .then((res) => res.json())
};

export const addArtist = (artist) => {
    return fetch(_apiUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(artist),
    });
};

export const getArtistById = (id) => {
    return fetch(`${_apiUrl}/${id}`)
        .then((res) => res.json())
};

export const updateArtist = (artist) => {
    return fetch(`${_apiUrl}/${artist.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(artist),
    }).then(getArtists());
}

export const deleteArtist = (artist) => {
    return fetch(`${_apiUrl}/${artist.id}`, {
        method: "DELETE"
    })
}

export const getArtistsByConcertId = (concertId) => {
    return fetch(_apiUrl + `/GetConcertArtists/${concertId}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
        },
    }).then((resp) => resp.json());
};