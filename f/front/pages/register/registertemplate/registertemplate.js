import React from "react";
import styles from './registertemplate.module.css'
import RegisterButton from './registerbutton.js'

export default function Register(){
    return (
        <div> 
                <div className={styles.inputscontainer}>
                    <form className={styles.form}>
                        <div className={styles.inputcontainer}>
                            <label className={styles.label}>Firstname: </label>
                            <input name="Firstname" type="text" className={styles.input} />
                        </div>
                        <div className={styles.inputcontainer}>
                            <label className={styles.label}>Lastname: </label>
                            <input name="Lastname" type="text" className={styles.input} />
                        </div>
                        <div className={styles.inputcontainer}>
                            <label className={styles.label}>Email: </label>
                            <input name="Email" type="text" placeholder="email@example.com" className="input" />
                        </div>
                        <div className={styles.inputcontainer}>
                            <label className={styles.label}>Password: </label>
                            <input name="Password" type="text" className={styles.input} />
                        </div>
                        <div className={styles.inputcontainer}>
                            <label className={styles.label}>Confirm: </label>
                            <input name="Confirm" type="text" className={styles.input} />
                        </div>

                    </form>
                    <RegisterButton></RegisterButton>
                    

                </div>
                
        </div>
        
            
    )

}