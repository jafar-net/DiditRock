import React from "react";
import { Card, CardBody, Row, Button, Col } from "reactstrap"
import { deleteConcert, getAllConcerts } from "../modules/concertManager";
import { useHistory, Link } from "react-router-dom";
import { useState, useEffect } from "react";
import { getArtistsByConcertId } from "../modules/artistManager";

export const Concert = ({ concert, setConcerts }) => {
    const [concertArtists, setConcertArtists] = useState([]);
    const [artist, setArtists] = useState([]);

    const history = useHistory();

    const handleClickDeleteConcert = () => {
        const confirm = window.confirm("Are you sure you want to delete this concert?")
        if (confirm == true) {
            deleteConcert(concert)
                .then(getAllConcerts().then(concert => setConcerts(concert)))
        } else {
            return;
        }
    }

    const handleClickEditConcert = () => {
        history.push(`/concert/edit/${concert.id}`)
    }

    const getConcertArtists = () => {
        getArtistsByConcertId(concert.id).then(artists => setConcertArtists(artists));
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
                    <Col>
                        <Button onClick={handleClickEditConcert} color="primary">Edit</Button>
                    </Col>
                    <Col>
                        <Button onClick={handleClickDeleteConcert} color="danger">Delete</Button>
                    </Col>

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
