import React from "react";
import { Card, CardBody, Row, Button, Col } from "reactstrap"
import { deleteArtist, getArtists } from "../modules/artistManager";
import { useHistory } from "react-router-dom";

export const Artist = ({ artist, setArtists }) => {

    const history = useHistory();

    const handleClickDeleteArtist = () => {
        const confirm = window.confirm("Are you sure you want to delete this artist?")
        if (confirm == true) {
            deleteArtist(artist)
                .then(getArtists().then(artists => setArtists(artists)))
        } else {
            return;
        }
    }

    const handleClickEditArtist = () => {
        history.push(`/artist/edit/${artist.id}`)
    }

    return (
        <Card>
            <CardBody>
                <Row>
                    <Col>
                        <strong className="link">{artist.name}</strong>
                    </Col>
                    <Col>
                        <Button onClick={handleClickEditArtist} color="primary">Edit</Button>
                    </Col>
                    <Col>
                        <Button onClick={handleClickDeleteArtist} color="danger">Delete</Button>
                    </Col>
                </Row>
            </CardBody>
        </Card>
    )
}
