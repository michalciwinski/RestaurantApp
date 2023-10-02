import React from "react";
import styles from './registertemplate.module.css'

export default function Register(){
    return (
        <div> 
                <div className={styles.inputscontainer}>
                    <form className={styles.form}>
                        <div className={styles.inputcontainer}>
                            <label className={styles.label}>Firstname: </label>
                            <input type="text" className={styles.input} />
                        </div>
                        <div className={styles.inputcontainer}>
                            <label className={styles.label}>Lastname: </label>
                            <input type="text" className={styles.input} />
                        </div>
                        <div className={styles.inputcontainer}>
                            <label className={styles.label}>Email: </label>
                            <input type="text" placeholder="email@example.com" className="input" />
                        </div>
                        <div className={styles.inputcontainer}>
                            <label className={styles.label}>Password: </label>
                            <input type="text" className={styles.input} />
                        </div>
                        <div className={styles.inputcontainer}>
                            <label className={styles.label}>Confirm: </label>
                            <input type="text" className={styles.input} />
                        </div>

                    </form>

                    <input type="button" className={styles.buttonconfirm}></input>

                </div>
                
        </div>
        
            
    )

}