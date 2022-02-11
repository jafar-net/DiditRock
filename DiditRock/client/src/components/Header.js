import React, { useState } from 'react';
import { NavLink as RRNavLink } from "react-router-dom";
import {
    Collapse,
    Navbar,
    NavbarToggler,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink
} from 'reactstrap';
import { logout } from '../modules/authManager';
import { getPosts } from '../modules/postManager';
import '../css/NavBar.css';

export default function Header({ isLoggedIn }) {
    const [isOpen, setIsOpen] = useState(false);
    const toggle = () => setIsOpen(!isOpen);



    return (
        <div>
            <Navbar light expand="md">
                <NavbarBrand tag={RRNavLink} to="/">Did It Rock?</NavbarBrand>
                <Nav className="navbar" navbar>
                    {/* When isLoggedIn === true, we will render the Home link */}
                    {isLoggedIn && (
                        <>
                            <NavItem className="navbar__link">
                                <NavLink tag={RRNavLink} to="/">
                                    Home
                                </NavLink>
                            </NavItem>
                            <NavItem className="navbar__link">
                                <NavLink tag={RRNavLink} to="/concert">
                                    Concerts
                                </NavLink>
                            </NavItem>
                            <NavItem className="navbar__link">
                                <NavLink tag={RRNavLink} to="/post">
                                    Reviews
                                </NavLink>
                            </NavItem>
                            <NavItem className="navbar__link">
                                <NavLink tag={RRNavLink} to="/myposts">
                                    My Reviews
                                </NavLink>
                            </NavItem>
                            <NavItem className="navbar__link">
                                <NavLink tag={RRNavLink} to="/venue">
                                    Venues
                                </NavLink>
                            </NavItem>
                            <NavItem className="navbar__link">
                                <NavLink tag={RRNavLink} to="/artist">
                                    Artists
                                </NavLink>
                            </NavItem>
                            <NavItem className="navbar__link">
                                <NavLink tag={RRNavLink} to="/users">
                                    Users
                                </NavLink>
                            </NavItem>
                        </>
                    )}
                </Nav>
                <Nav navbar>
                    {isLoggedIn && (
                        <>
                            <NavItem>
                                <a
                                    aria-current="page"
                                    className="navbar__link"
                                    style={{ cursor: "pointer" }}
                                    onClick={logout}
                                >Logout</a>
                            </NavItem>
                        </>
                    )}
                    {!isLoggedIn && (
                        <>
                            <NavItem>
                                <NavLink tag={RRNavLink} to="/login">
                                    Login
                                </NavLink>
                            </NavItem>
                            <NavItem>
                                <NavLink tag={RRNavLink} to="/register">
                                    Register
                                </NavLink>
                            </NavItem>
                        </>
                    )}
                </Nav>
            </Navbar>
        </div>
    );
}