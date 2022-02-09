import firebase from "firebase/app";
import "firebase/auth";
import { getToken } from "./authManager";

const _apiUrl = "/api/post";

export const getMyPosts = () => {
    return getToken()
        .then(
            (token) =>

                fetch(`${_apiUrl}/myPosts`, {
                    method: "Get",
                    headers: { Authorization: `Bearer ${token}` },
                })
        )
        .then((res) => res.json());
};

export const getPosts = () => {
    return getToken().then((token) =>
        fetch(_apiUrl, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
        }).then((res) => res.json())
    );
};

export const getPostById = (id) => {
    return getToken().then((token) =>
        fetch(`${_apiUrl}/${id}`, {
            method: "GET",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
        }).then((res) => res.json())
    );
};

export const updatePost = (post) => {
    return getToken().then((token) =>
        fetch(_apiUrl, {
            method: "PUT",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify(post),
        })
    );
};

export const deletePost = (post) => {
    return getToken().then((token) =>
        fetch(`${_apiUrl}/${post.id}`, {
            method: "DELETE",
            headers: {
                Authorization: `Bearer ${token}`,
            },
        })
    );
};

export const addPost = (post) => {
    return getToken().then((token) =>
        fetch(_apiUrl, {
            method: "POST",
            headers: {
                Authorization: `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify(post),
        })
    );
};
