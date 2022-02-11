import React from "react";
import { Card, CardBody } from "reactstrap";
import { useHistory } from "react-router-dom/cjs/react-router-dom.min";
import "../css/User.css"

const User = ({ user }) => {
    const history = useHistory()
    {
        return (
            <Card >
                <CardBody>
                    <p className="user-info"> Display Name: {user.displayName}</p>
                    <p className="user-info">Name: {user.firstName} {user.lastName}</p>
                    <p className="user-info">User Type: {user.userTypeId}</p>
                    <button onClick={() => history.push(`/users/userdetails/${user.id}`)}> User Details</button>
                    <button onClick={() => history.push(`/users/UserTypeEdit/${user.id}`)}> edit user</button>
                </CardBody>
            </Card>
        );
    }


};

export default User;