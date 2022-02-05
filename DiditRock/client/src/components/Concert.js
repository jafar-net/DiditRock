import React from "react";
import { Card, CardBody, Row, Button, Col } from "reactstrap"
import { deleteConcert, getAllConcerts } from "../modules/concertManager";
import { useHistory, Link } from "react-router-dom";

export const Concert = ({ concert, setConcerts }) => {

    const history = useHistory();

    const handleClickDeleteConcert = () => {
        const confirm = window.confirm("Are you sure you want to delete this concert?")
        if (confirm == true) {
            deleteConcert(concert)
                .then(getAllConcerts().then(concerts => setConcerts(concerts)))
        } else {
            return;
        }
    }

    const handleClickEditConcert = () => {
        history.push(`/concert/edit/${concert.id}`)
    }

    return (
        <Card>
            <CardBody>
                <Row>
                    <Link to={`/concertdetails/${concert.id}`}>
                        <strong> {concert.name}</strong>
                    </Link>
                    <Col>
                        <Button onClick={handleClickEditConcert} color="primary">Edit</Button>
                    </Col>
                    <Col>
                        <Button onClick={handleClickDeleteConcert} color="danger">Delete</Button>
                    </Col>
                </Row>
            </CardBody>
        </Card>
    )
}
