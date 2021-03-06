import React from 'react';
import ReactDOM from 'react-dom';
import App from './App';
import firebase from "firebase/app";
import "bootstrap/dist/css/bootstrap.min.css";
import "./index.css";
import reportWebVitals from './reportWebVitals';

const firebaseConfig = {
  apiKey: process.env.REACT_APP_API_KEY,
};

firebase.initializeApp(firebaseConfig);

ReactDOM.render(
  <App>
    <App />
  </App>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
