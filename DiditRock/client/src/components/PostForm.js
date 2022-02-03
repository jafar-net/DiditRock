import React, { useState, useEffect } from "react";
import { addPost } from "../modules/postManager";
import { useHistory, useParams } from "react-router-dom";
import { Container, Input } from "reactstrap"
import { getAllConcerts } from "../modules/concertManager";
import { getPostById, updatePost } from "../modules/postManager";

const PostForm = () => {

    const history = useHistory();

    const [Concert, setConcert] = useState([]);

    const [post, setPost] = useState({
        headline: "",
        review: "",
        imageUrl: "",
        createDateTime: "",
        concertId: "",
    })

    const postId = useParams();

    if (postId.id && post.headline === "") {
        getPostById(postId.id)
            .then(post => setPost(post));
    }

    const getConcert = () => {
        getAllConcerts().then(Concert => setConcert(Concert));
    };

    useEffect(() => {
        getConcert();
    }, []);


    const handleInput = (event) => {
        const newPost = { ...post };
        newPost[event.target.id] = event.target.value;
        setPost(newPost);
    }

    const handleClickCreatePost = () => {
        addPost(post)
            .then(history.push("/post"))
    }

    const handleClickUpdatePost = () => {
        updatePost(post)
            .then(history.push("/post"))
    }

    return (
        <Container>
            <div className="postForm">
                <h3>Write a Review</h3>
                <div className="container-5">
                    <div className="form-group">

                        <label for="name">Title</label>
                        <Input type="name" class="form-control" id="headline" placeholder="headline" value={post.headline} onChange={handleInput} required />

                        <label for="content">Review</label>
                        <Input type="textarea-lg" class="form-control" id="content" placeholder="Content" value={post.content} onChange={handleInput} required />

                        <label for="imageUrl">Image URL</label>
                        <Input type="url" class="form-control" id="imageUrl" placeholder="Image URL" value={post.imageUrl} onChange={handleInput} required />

                        <label for="name">Date</label>
                        <Input type="datetime-local" class="form-control" id="createDateTime" placeholder="headline" value={post.createDateTime} onChange={handleInput} required />

                        <label for="concert">Concert</label>
                        <Input type="select" name="select" value={post.concertId} id="concertId" onChange={handleInput}>
                            <option value={null}>Select a Concert</option>
                            {Concert.map(c => {
                                return <option value={c.id}>{c.name}</option>
                            })}
                        </Input>
                    </div>
                    {postId.id ?
                        <div>
                            <button type="submit" class="btn btn-primary mr-3" onClick={event => {
                                handleClickUpdatePost()
                            }}>Update</button>
                        </div>
                        :
                        <div>
                            <button type="submit" class="btn btn-primary mr-3" onClick={event => {
                                handleClickCreatePost()
                            }}>Create</button>
                        </div>
                    }
                </div>
            </div>
        </Container>
    )
}

export default PostForm;