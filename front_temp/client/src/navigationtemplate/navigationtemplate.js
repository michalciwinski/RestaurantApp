import React from "react";
import { Route, Routes, Link, useNavigate } from 'react-router-dom';
import styles from './navigation.module.css';
import Menu from '../Tab_Menu/menu'; 
import AdminView from '../Tab_Admin/adminView';
import VoiceSite from '../Tab_Voice/voicesite';
import Register from '../Tab_Register/register';
import Login from '../Tab_Login/login';
import EditDish from "../Tab_Admin/editDish";
import ShowDish from "../Tab_Admin/showDish";
import AddDish from "../Tab_Admin/addDish";
import AccessDeniedAdmin from "./accessDenied";
import { useEffect, useState } from 'react';

export default function NavigationTemplate() {
  const [loggedIn, setLoggedIn] = useState(false)
  const [userRole, setUserRole] = useState('');
  const [userToken, setUserToken] = useState('');

  const navigate = useNavigate()

  useEffect(() => {
    const user = JSON.parse(localStorage.getItem('user'));
    const token = JSON.parse(localStorage.getItem('token'));
    if (user) {
      setLoggedIn(true);
      setUserRole(user.role);
      setUserToken(token.token);
    }
  }, [loggedIn]);//Without dependency array it will work only one in load, if there exist something, it listens to changes.
                 //If we use return, it first work and later code above


  function handleLogout() {
    localStorage.removeItem('user');
    localStorage.removeItem('token');
    setLoggedIn(false);
    setUserRole(''); 
    setUserToken('');
    navigate('/Tab_Voice/voicesite');
  }

  return (
    <div>
        <nav className={styles.nav}>
          <ul className={styles.ull1}>
            <li className={styles.lii}><Link to="/Tab_Menu/menu">Menu</Link></li>
            <li className={styles.lii}><Link to="/Tab_Voice/voicesite">Voice</Link></li>
            {loggedIn && userRole === 'Admin' ? <li className={styles.lii}><Link to="/Tab_Admin/table">Admin</Link></li> : null  }
          </ul>
          <ul className={styles.ull2}>
            {!loggedIn ? <li className={styles.lii}><Link to="/Tab_Register/register">Register</Link></li> : null}
            {!loggedIn ? <li className={styles.lii}><Link to="/Tab_Login/login">Login</Link></li> : null}
            {loggedIn ? <li className={styles.lii}><button className={styles.buttonLogout} onClick={() => handleLogout()}>Logout</button></li> : null  }
          </ul>
        </nav>

        <Routes>
          <Route path="/Tab_Menu/menu" element={<Menu />} />
          <Route path="/Tab_Voice/voicesite" element={<VoiceSite />} />
          <Route path="/Tab_Admin/table" element={loggedIn && userRole === 'Admin' ? <AdminView /> : <AccessDeniedAdmin/>} />

          <Route path="/Tab_Register/register" element={!loggedIn ? <Register /> : <p>Access is denied - You are logged in</p>} />
          <Route path="/Tab_Login/login" element={!loggedIn ? <Login loggedIn={loggedIn} setLoggedIn={setLoggedIn}/> : <p>Access is denied - You are logged in</p>} />

          <Route path="/Tab_Admin/getDish/:id" element={loggedIn && userRole === 'Admin' ? <ShowDish /> : <AccessDeniedAdmin/>} />
          <Route path="/Tab_Admin/editDish/:id" element={loggedIn && userRole === 'Admin' ? <EditDish userToken={userToken} setUserToken={setUserToken}/> : <AccessDeniedAdmin/>} />
          <Route path="/Tab_Admin/addDish" element={loggedIn && userRole === 'Admin' ? <AddDish userToken={userToken} setUserToken={setUserToken}/> : <AccessDeniedAdmin/>} />
        </Routes>

    </div>
  );
}