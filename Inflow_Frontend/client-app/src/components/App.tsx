import React from 'react';
import Layout from './Layout'
import Nav from './Nav/Nav'
import  './App.scss'

function App() {
  return (
      <div className="app-container">
        <Nav/>
        <Layout/>
      </div>
  );
}

export default App;
