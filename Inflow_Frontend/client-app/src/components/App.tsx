import React from 'react';
import Layout from './Layout';
import Nav from './Nav/Nav';
import {BrowserRouter} from "react-router-dom";
import  './App.scss';


function App() {
  return (
      <div className="app-container">
        <Nav/>
        <BrowserRouter>
            <Layout/>
        </BrowserRouter>
      </div>
  );
}

export default App;
