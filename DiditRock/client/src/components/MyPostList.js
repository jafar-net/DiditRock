import React, { useEffect, useState } from "react";
import { getMyPosts } from "../modules/postManager";
import { Post } from "./Post";

const MyPostList = () => {
    const [myPosts, setMyPosts] = useState([]);

    const getAllMyPosts = () => {
        getMyPosts().then((myPosts) => {
            setMyPosts(myPosts);
        });
    };

    useEffect(() => {
        getAllMyPosts();
    }, []);

    return (
        <div className="container">
            <h1 className="reviews">My Posts</h1>
            <div className="row justify-content-center">
                {console.log(myPosts)}
                <p key={0}>
                    {myPosts?.map((post) => (
                        <Post post={post} key={post.Id} />
                    ))}
                </p>
            </div>
        </div>
    );
};
export default MyPostList;