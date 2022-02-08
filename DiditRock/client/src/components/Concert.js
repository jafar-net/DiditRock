import React from "react";
import { Card, CardBody, Row, Button, Col } from "reactstrap"
import { deleteConcert, getAllConcerts } from "../modules/concertManager";
import { useHistory, Link } from "react-router-dom";
import { useState, useEffect } from "react";
import { getConcertArtistsByConcertId } from "../modules/concertArtistManager";

export const Concert = ({ concert, setConcerts }) => {
    const [concertArtists, setConcertArtists] = useState([]);
    const [artist, setArtists] = useState([]);

    const history = useHistory();

    const getConcertArtists = () => {
        getConcertArtistsByConcertId(concert.id).then(artists => setConcertArtists(artists));
    }

    useEffect(() => {
        getConcertArtists();
    }, []);



    console.log(concertArtists);

    {
        return (
            <Card >

                <CardBody>

                    <Row>
                        <Link to={`/concertdetails/${concert.id}`}>
                            <strong> {concert.name}</strong>
                        </Link>
                    </Row>
                    {/* <div className="vidcard-rbox">
                        <p className="vid-title">
                            <strong>{concert.title}</strong>
                        </p>

                        <button className="mng-artists-button" onClick={() => { history.push(`/manageartists/${concert.id}`) }}>Manage Artists</button>

                    </div> */}

                </CardBody>
            </Card>
        );
    }


};

export default Concert;



//     return (
//         <Card>
//             <CardBody>
//                 <Row>
//                     <Link to={`/concertdetails/${concert.id}`}>
//                         <strong> {concert.name}</strong>
//                     </Link>
//                     <Col>
//                         <Button onClick={handleClickEditConcert} color="primary">Edit</Button>
//                     </Col>
//                     <Col>
//                         <Button onClick={handleClickDeleteConcert} color="danger">Delete</Button>
//                     </Col>
//                 </Row>
//             </CardBody>
//         </Card>
//     )
// }
