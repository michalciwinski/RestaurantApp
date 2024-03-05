import React from "react";
import { Link } from 'react-router-dom';
import styles from './navigation.module.css';

export default function NavigationTemplate() {
  return (
    <nav className={styles.nav}>
      <ul className={styles.ull1}>
        <li className={styles.lii}><Link to="/menuf/menu" className={styles.linkreload}>Menu</Link></li>
        <li className={styles.lii}><Link to="/adminview/add" className={styles.linkreload}>Admin</Link></li>
        <li className={styles.lii}><Link to="/chat/chatmain" className={styles.linkreload}>GPT</Link></li>
        <li className={styles.lii}><Link to="/voice/voicesite" className={styles.linkreload}>Voice</Link></li>
      </ul>
      <ul className={styles.ull2}>
        <li className={styles.lii}><Link to="/register/register" className={styles.linkreload}>Register</Link></li>
        <li className={styles.lii}><Link to="/login/login" className={styles.linkreload}>Login</Link></li>
      </ul>
    </nav>
  );
}