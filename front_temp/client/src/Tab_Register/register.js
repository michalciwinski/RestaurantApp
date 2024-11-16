import React, { useState } from "react";
import Navigationtemplate from '../navigationtemplate/navigationtemplate.js'
import { useNavigate } from 'react-router-dom'
import styles from'./register.module.css'
import endpointsService from "../api/axiosService";


const Register = () => {
    const [firstName, setFirstName] = useState('')
    const [lastName, setLastName] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [emailError, setEmailError] = useState('')
    const [passwordError, setPasswordError] = useState('')

    const navigate = useNavigate()

    const onButtonClick = () => {
        setEmailError('')
        setPasswordError('')
    
        // Check if the user has entered both fields correctly
        if ('' === email) {
          setEmailError('Please enter your email')
          return
        }
        if (!/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(email)) {
          setEmailError('Please enter a valid email')
          return
        }
        if ('' === password) {
          setPasswordError('Please enter a password')
          return
        }
        if (password.length < 5) {
          setPasswordError('The password must be 6 characters or longer')
          return
        }
        const defaultUSerType = 2;
        const data = JSON.stringify({ email, password, firstName, lastName, defaultUSerType })
        endpointsService
        .register(data)
        .then((r) => {
            console.log(r.data); 
            window.alert('You are registered. Now You can login');
            navigate('/Tab_Menu/menu');
        })
        .catch((err) => {
            window.alert('Wrong email or password');
            console.log(err);
        });


      }

    return (
        <div className={styles['mainContainer']}>
                <div className={styles['titleContainer']}>
                    <div>Rejestracja</div>
                </div>
                <br />
                <div className={styles['inputContainer']}>
                    <input
                    value={firstName}
                    placeholder="Jan"
                    onChange={(ev) => setFirstName(ev.target.value)}
                    className={styles['inputBox']}
                    />
                </div>
                <br />
                <div className={styles['inputContainer']}>
                    <input
                    value={lastName}
                    placeholder="Kowalski"
                    onChange={(ev) => setLastName(ev.target.value)}
                    className={styles['inputBox']}
                    />
                </div>
                <br />
                <div className={styles['inputContainer']}>
                    <input
                    value={email}
                    placeholder="Email@przyklad.pl"
                    onChange={(ev) => setEmail(ev.target.value)}
                    className={styles['inputBox']}
                    />
                    <label className="errorLabel">{emailError}</label>
                </div>
                <br />
                <div className={styles['inputContainer']}>
                    <input
                    type="password"
                    value={password}
                    placeholder="Haslo"
                    onChange={(ev) => setPassword(ev.target.value)}
                    className={styles['inputBox']}
                    />
                    <label className="errorLabel">{passwordError}</label>
                </div>
                <br />
                <div className={styles['inputContainer']}>
                    <input className={styles['inputButton']} type="button" onClick={onButtonClick} value={'Zarejestruj'} />
                </div>
        </div>
    )

}
export default Register