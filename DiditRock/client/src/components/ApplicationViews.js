import React from "react";
import { Switch, Route, Redirect } from "react-router-dom";
import Login from "./Login";
import Register from "./Register";
import Home from "./Home";
import PostList from "./PostList";
import { PostDetails } from "./PostDetails";
import PostForm from "./PostForm";
import { ConcertList } from "./ConcertList";
import { VenueList } from "./VenueList";
import ConcertForm from "./ConcertForm";
import ArtistForm from "./ArtistForm";
import { ArtistList } from "./ArtistList";
import { VenueDetails } from "./VenueDetails";
import UserList from "./UserList";
import UserDetails from "./UserDetails";
import { ConcertDetails } from "./ConcertDetails";
import ManageArtists from "./ManageArtists"
import MyPostList from "./MyPostList";
import VenueForm from "./VenueForm";

export default function ApplicationViews({ isLoggedIn }) {

  return (
    <main>
      <Switch>
        <Route path="/" exact>
          {isLoggedIn ? <Home /> : <Redirect to="/login" />}
        </Route>

        <Route path="/login">
          <Login />
        </Route>

        <Route path="/register">
          <Register />
        </Route>

        <Route path="/users" exact>
          {isLoggedIn ? <UserList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/users/userdetails/:id">
          {isLoggedIn ? <UserDetails /> : <Redirect to="/login" />}
        </Route>

        <Route path="/post" exact>
          {isLoggedIn ? <PostList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/postdetails/:id">
          {isLoggedIn ? <PostDetails /> : <Redirect to="/login" />}
        </Route>

        <Route path="/post/create">
          {isLoggedIn ? <PostForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/post/edit/:id" exact>
          {isLoggedIn ? <PostForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/myPosts">
          {isLoggedIn ? <MyPostList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/concert" exact>
          {isLoggedIn ? <ConcertList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/concert/add" exact>
          {isLoggedIn ? <ConcertForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/concert/edit/:id">
          {isLoggedIn ? <ConcertForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/venue" exact>
          {isLoggedIn ? <VenueList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/venue/:id" exact>
          {isLoggedIn ? <VenueDetails /> : <Redirect to="/login" />}
        </Route>

        <Route path="/venue/create">
          {isLoggedIn ? <VenueForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/venue/edit/:id" exact>
          {isLoggedIn ? <VenueForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/concertdetails/:id" exact>
          {isLoggedIn ? <ConcertDetails /> : <Redirect to="/login" />}
        </Route>

        <Route path="/manageArtists/:id">
          <ManageArtists userparams />
        </Route>

        <Route path="/artist" exact>
          {isLoggedIn ? <ArtistList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/artist/add" exact>
          {isLoggedIn ? <ArtistForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/artist/edit/:id">
          {isLoggedIn ? <ArtistForm /> : <Redirect to="/login" />}
        </Route>


      </Switch>
    </main>
  );
};