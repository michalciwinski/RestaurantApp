import React from "react";



function Register_Post(data){
    fetch('https://localhost:7197/Controller_UserAccount/RegisterUser', 
    { 
      method: 'POST', 
      mode: 'cors', 
      headers: {
        'Content-Type': 'application/json',
        //'Access-Control-Allow-Origin': '*'
        },
      body: JSON.stringify(data)
    })
    .then(response => {
        if (!response.ok) {
          throw new Error(`HTTP error! Status: ${response.status}`);
        }
        return response.json();
      })
      .then(data => {
        console.log('Success:', data);
      })
      .catch(error => {
        console.error('Error:', error);
      });
}

export default function Register(){
    const handleClick = () => {
        const Firstname = document.querySelector('input[name="Firstname"]');
        const Lastname = document.querySelector('input[name="Lastname"]');
        const Email = document.querySelector('input[name="Email"]');
        const Password = document.querySelector('input[name="Password"]');
        const FirstnameValue = Firstname.value;
        const LastnameValue = Lastname.value;
        const EmailValue = Email.value;
        const PasswordValue = Password.value;

        const newUser = {
            Email: EmailValue,
            Password: PasswordValue,
            FirstName: FirstnameValue,
            LastName: LastnameValue,
            TUserTypeId: 1,
          };
        Register_Post(newUser)
    };



    return (
        <input onClick={handleClick} type="button" value="Register">
        </input>
        )
    
    }