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
                    <h1>Posts</h1>
                </Col>
                <Col xs={{ size: 2, offset: 0.5 }}>
                    <button
                        className="btn-primary"
                        name="postForm"
                        onClick={handleClickPostForm}
                    >
                        Create a Post{" "}
                    </button>
                </Col>
            </Row>

            <div className="row">
                <p>
                    {posts.map((post) => (
                        <Post
                            post={post}
                            key={post.Id}
                            setPosts={setPosts}
                            getPosts={getPosts}
                        />
                    ))}
                </p>
            </div>
        </div>
    );
};

export default PostList;