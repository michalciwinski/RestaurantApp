import React from "react";
import styles from './navigation.module.css'

export default function navigationtemplate(){
    return (
        <nav className={styles.nav}>

          <ul className={styles.ull1}>
            <li className={styles.lii}><a className={styles.linkreload} href="/menuf/menu">Menu</a></li>
            <li className={styles.lii}><a className={styles.linkreload} href="/adminview/add">Admin</a></li>
            <li className={styles.lii}><a className={styles.linkreload} href="/chat/chatmain">GPT</a></li>
          </ul>
          <ul className={styles.ull2}>
            <li className={styles.lii}><a className={styles.linkreload} href="/register/register">Register</a></li>
            <li className={styles.lii}><a className={styles.linkreload} href="/login/login">Login</a></li>
          </ul>
        </nav > 
    );

};