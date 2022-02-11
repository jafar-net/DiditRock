import React, { useEffect, useState } from "react";
import { getPosts } from "../modules/postManager";
import { Post } from "./Post";
import { Col, Row } from "reactstrap";
import { useHistory } from "react-router-dom";

export const PostList = () => {
    const [posts, setPosts] = useState([]);

    const history = useHistory();

    const getAllPosts = () => {
        getPosts().then((posts) => setPosts(posts));
    };

    useEffect(() => {
        getAllPosts();
    }, []);

    const handleClickPostForm = () => {
        history.push("/post/create");
    };

    return (
        <div className="container">
            <Row>
                <Col xs={{ size: 2, offset: 1 }}>
                    <h1>Reviews</h1>
                </Col>
                <Col xs={{ size: 2, offset: 0.5 }}>
                    <button
                        className="btn-primary"
                        key={0}
                        name="postForm"
                        onClick={handleClickPostForm}
                    >
                        Write a Review{" "}
                    </button>
                </Col>
            </Row>

            <div key={0} className="row">
                <div>
                    {posts.map((post) => (
                        <Post
                            post={post}
                            key={post.Id}
                            setPosts={setPosts}
                            getPosts={getPosts}
                        />
                    ))}
                </div>
            </div>
        </div >
    );
};

export default PostList;