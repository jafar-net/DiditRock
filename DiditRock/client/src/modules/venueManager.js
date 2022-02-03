const _apiUrl = '/api/venue';

export const getAllVenues = () => {
    return fetch(_apiUrl)
        .then((res) => res.json())
};

export const addVenue = (venue) => {
    return fetch(_apiUrl, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(venue),
    });
};

export const getVenueById = (id) => {
    return fetch(`${_apiUrl}/${id}`)
        .then((res) => res.json())
};

export const updateVenue = (venue) => {
    return fetch(`${_apiUrl}/${venue.id}`, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(venue),
    }).then(getAllVenues());
}

export const deleteVenue = (venue) => {
    return fetch(`${_apiUrl}/${venue.id}`, {
        method: "DELETE"
    })
}