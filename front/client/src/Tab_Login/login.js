import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom'
import styles from'./login.module.css'
import endpointsService from "../api/axiosService";

const Login = (props) => {
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [emailError, setEmailError] = useState('')
  const [passwordError, setPasswordError] = useState('')

  const navigate = useNavigate()

  const onButtonClick = () => {
    setEmailError('');
    setPasswordError('');

    // Check if the user has entered both fields correctly
    if ('' === email) {
      setEmailError('Please enter your email');
      return
    }
    if (!/^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/.test(email)) {
      setEmailError('Please enter a valid email');
      return
    }
    if ('' === password) {
      setPasswordError('Please enter a password');
      return
    }

   
    const data = JSON.stringify({ email, password })
    endpointsService
      .login(data)
      .then((r) => {
        //local storage is enough for this project
          localStorage.setItem('user', JSON.stringify({ role: r.data.Role }));
          localStorage.setItem('token', JSON.stringify({ token: r.data.Token }));
          props.setLoggedIn(true);
          navigate('/Tab_Voice/voicesite');
        })
      .catch((err) => {
        window.alert('Wrong email or password');
        console.log(err);
      });


  }

  return (
    <div className={styles['mainContainer']}>
      <div className={styles['titleContainer']}>
        <div>Login</div>
      </div>
      <br />
      <div className={styles['inputContainer']}>
        <input
          value={email}
          placeholder="Email"
          onChange={(ev) => setEmail(ev.target.value)}
          className={styles['inputBox']}
        />
        <label className="errorLabel">{emailError}</label>
      </div>
      <br />
      <div className={styles['inputContainer']}>
        <input
          value={password}
          type="password"
          placeholder="Hasło"
          onChange={(ev) => setPassword(ev.target.value)}
          className={styles['inputBox']}
        />
        <label className="errorLabel">{passwordError}</label>
      </div>
      <br />
      <div className={styles['inputContainer']}>
        <input className={styles['inputButton']} type="button" onClick={onButtonClick} value={'Zaloguj'} />
      </div>
    </div>
  )
}

export default Login