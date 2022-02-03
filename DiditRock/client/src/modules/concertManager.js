const _apiUrl = '/api/concert';

export const getAllConcerts = () => {
    return fetch(_apiUrl)
        .then((res) => res.json())
};

export const addConcert = (concert) => {
    return fetch(_apiUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(concert),
    });
};

export const getConcertById = (id) => {
    return fetch(`${_apiUrl}/${id}`)
        .then((res) => res.json())
};

export const updateConcert = (concert) => {
    return fetch(`${_apiUrl}/${concert.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(concert),
    }).then(getAllConcerts());
}

export const deleteConcert = (concert) => {
    return fetch(`${_apiUrl}/${concert.id}`, {
        method: "DELETE"
    })
}