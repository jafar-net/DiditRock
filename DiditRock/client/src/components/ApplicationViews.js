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
// import ConcertDetails from "./ConcertDetails"

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

        <Route path="/post/:id" exact>
          {isLoggedIn ? <PostDetails /> : <Redirect to="/login" />}
        </Route>

        <Route path="/post/create" exact>
          {isLoggedIn ? <PostForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/post/edit/:id" exact>
          {isLoggedIn ? <PostForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/concert" exact>
          {isLoggedIn ? <ConcertList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/concert/add" exact>
          {isLoggedIn ? <ConcertForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/concert/edit/:id" exact>
          {isLoggedIn ? <ConcertForm /> : <Redirect to="/login" />}
        </Route>

        <Route path="/venue" exact>
          {isLoggedIn ? <VenueList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/venue/:id" exact>
          {isLoggedIn ? <VenueDetails /> : <Redirect to="/login" />}
        </Route>

        <Route path="/artist" exact>
          {isLoggedIn ? <ArtistList /> : <Redirect to="/login" />}
        </Route>

        <Route path="/artist/add" exact>
          {isLoggedIn ? <ArtistForm /> : <Redirect to="/login" />}
        </Route>

      </Switch>
    </main>
  );
};