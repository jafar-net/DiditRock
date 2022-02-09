import React, { useEffect, useState, UseState } from "react";
import { useParams } from "react-router-dom";
import { useHistory } from "react-router";
import { getConcertById } from "../modules/concertManager";
import { Link } from "react-router-dom";
import { getArtists } from "../modules/artistManager";
import { Row, Col } from "reactstrap";
import { getArtistsByConcertId } from "../modules/artistManager";


export const ConcertDetails = () => {
    const [concert, setConcert] = useState({});
    const [artists, setArtists] = useState([]);
    const [showArtists, setShowArtists] = useState(false);
    const { id } = useParams();

    const date = new Date(concert.date).toDateString()

    const history = useHistory();

    useEffect(() => {
        getConcertById(id)
            .then((concert) => setConcert(concert))
            .then(getArtistsByConcertId(id)
                .then((artists) => setArtists(artists)))
    }, [])

    // const handleCheck = (artistId) => {
    //     const concertArtist = { artistId: artistId, concertId: id }
    //     replaceArtists(concertArtist)
    //         .then(getConcertArtistsByConcertId(id))
    //         .then(ca => setConcertArtists(ca))
    // }

    let ArtistArray = []
    for (const Artist in concert.artists) {
        ArtistArray.push(Artist)
    }

    console.log(concert)

    return (
        <div className="container">
            {showArtists ?
                <>
                    <div className="artists-style">
                        {artists.map((artist) => (
                            <p className="artist">{artist.artist?.name}</p>
                        ))}
                    </div>
                </>
                :
                null}
            <div className="row justify-content-center">
            </div>
            <div>
                <div className="col-sm-12 col-lg-6">
                    <button className="mng-tags-button" onClick={() => { history.push(`/manageartists/${concert.id}`) }}>Manage Artists</button>
                    <Col>
                        <strong>{concert.venue?.name}</strong>
                    </Col>
                    <div>Artists : {artists.map((a) => <p>{a.name}</p>)}</div>
                    <Col>
                        {concert.genre}
                    </Col>
                    <div>{date}</div>
                    <Col>
                        Encore Songs: {concert.encoreSongs}
                    </Col>
                </div>
            </div>
        </div>
    )
}