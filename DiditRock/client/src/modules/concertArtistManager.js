const _apiUrl = "/api/concertArtist"

export const addConcertArtist = (concertArtist) => {
    return fetch(_apiUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(concertArtist),
    })
}

export const getConcertArtists = (concertArtistId) => {
    return fetch(`${_apiUrl}/${concertArtistId}`)
        .then((res) => res.json())
}


export const updateConcertArtists = (concertArtist) => {
    return fetch(_apiUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(concertArtist),
    }).then(res => res.json())
}