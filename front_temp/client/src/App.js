import React from 'react';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import './App.css';
import Navigationtemplate from './navigationtemplate/navigationtemplate.js';
import Menu from './menuf/menu.js'; 
import AdminView from './adminview/add.js';
import VoiceSite from './voice/voicesite.js';
import Register from './register/register.js';
import Login from './login/login.js';
import styles from './navigationtemplate/navigation.module.css'


function App() {
  return (
    <Router>
      <div className="App">
        <nav className={styles.nav}>
          <ul className={styles.ull1}>
            <li className={styles.lii}><Link to="/menuf/menu">Menu</Link></li>
            <li className={styles.lii}><Link to="/adminview/add">Admin</Link></li>
            <li className={styles.lii}><Link to="/voice/voicesite">Voice</Link></li>
          </ul>
          <ul className={styles.ull2}>
            <li className={styles.lii}><Link to="/register/register">Register</Link></li>
            <li className={styles.lii}><Link to="/login/login">Login</Link></li>
          </ul>
        </nav>
        <Routes>
          <Route path="/menuf/menu" element={<Menu />} />
          <Route path="/adminview/add" element={<AdminView />} />
          <Route path="/voice/voicesite" element={<VoiceSite />} />
          <Route path="/register/register" element={<Register />} />
          <Route path="/login/login" element={<Login />} />
        </Routes>
      </div>
    </Router>
  );
}

export default App;
