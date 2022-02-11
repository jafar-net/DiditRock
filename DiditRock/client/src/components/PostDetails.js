import React, { useEffect, useState, UseState } from "react";
import { ListGroup, ListGroupItem } from "reactstrap";
import { useParams } from "react-router-dom";
import { getPostById } from "../modules/postManager";
import Post from "./Post";


export const PostDetails = () => {
    const [post, setPost] = useState({});
    const { id } = useParams();

    const date = new Date(post.createDateTime).toDateString()

    useEffect(() => {
        getPostById(id)
            .then(setPost);
    }, [])

    return (
        <div className="container">
            <div className="row justify-content-center">
                <div className="col-sm-12 col-lg-6">
                    <img src={post.imageUrl} alt="post-img" />
                    <h2>{post.headline}</h2>
                    <div>{post.concert?.name}</div>
                    <div>{post.userProfile?.displayName} {date}</div><br></br>
                    <p>{post.review}</p>
                </div>
            </div>
        </div>
    )
}