import React from "react";
import { Card, CardBody, Row, Button, Col } from "reactstrap"
import { deleteArtist, getAllArtists } from "../modules/artistManager";
import { useHistory } from "react-router-dom";

export const Artist = ({ artist, setArtists }) => {

    const history = useHistory();

    const handleClickDeleteArtist = () => {
        const confirm = window.confirm("Are you sure you want to delete this artist?")
        if (confirm == true) {
            deleteArtist(artist)
                .then(getAllArtists().then(artists => setArtists(artists)))
        } else {
            return;
        }
    }

    const handleClickEditTag = () => {
        history.push(`/artist/edit/${artist.id}`)
    }

    return (
        <Card>
            <CardBody>
                <Row>
                    <Col>
                        <strong>{artist.name}</strong>
                    </Col>
                </Row>
            </CardBody>
        </Card>
    )
}
