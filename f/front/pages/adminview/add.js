import React, {useState} from "react";
import Link from 'next/link';
import addstyle from './add.module.css';


const addform = addstyle['AddForm'];
const input = addstyle['input'];
const selector = addstyle['selector'];
const button = addstyle['button'];


function Addtodatebase(data){
    fetch('https://localhost:7197/Controller_Menu/AddDish', 
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
        return response.json(); // Parsuj odpowiedź jako JSON
      })
      .then(data => {
        // Obsłuż poprawną odpowiedź tutaj
        console.log('Success:', data);
      })
      .catch(error => {
        // Obsłuż błąd tutaj
        console.error('Error:', error);
      });
}

    


export default function AddDish() {
    /*const [formData, setFormData] = useState({
        id: 0,
        name: "",
        description: "",
        price: "",
        category: ""
    });

    const handleChange = e => {
        const { name, value } = e.target;
        setFormData({
            ...formData,
            [name]: value
        });
        console.log('value is:', formData);
    };*/
    const handleSubmit = (e) => {
        const nameInput = document.querySelector('input[name="name"]');
        const descriptionInput = document.querySelector('input[name="description"]');
        const priceInput = document.querySelector('input[name="price"]');
        const categorySelect = document.querySelector('select[name="category"]');
        const nameValue = nameInput.value;
        const descriptionValue = descriptionInput.value;
        const priceValue = priceInput.value;
        const categoryValue = categorySelect.value;
        /*setFormData({
            id: 0, 
            name: nameValue,
            description: descriptionValue,
            price: priceValue,
            category: categoryValue
        });*/
        //console.log('value is:', formData);
        const newDish = {
            id: 0,
            name: nameValue,
            description: descriptionValue,
            price: priceValue,
            dishType: categoryValue,
          };
        if (priceValue === "" || nameValue === "" || descriptionValue === "") {
            alert("wrong inputs");
            return;
        }
        Addtodatebase(newDish);
    };
    return (
        <div>
            <form className={addform}>
                <label className={input}>Name:
                    <input name="name" type="text" />
                </label>
                <label className={input}>Description:
                    <input name="description" type="text" />
                </label>
                <label className={input}>Price:
                    <input name="price" type="text" />
                </label>
                <select className={selector} name="category" >
                    <option className="optionsel">Soup</option>
                    <option className="optionsel">Main course</option>
                    <option className="optionsel">Dessert</option>
                    <option className="optionsel">Drink</option>
                </select>
                <button className={button} onClick={handleSubmit}>Add Dish
                </button>

                
                
            </form>

            <h2>
                <Link href="/">Back to home</Link>
            </h2> 

        </div>
        
        

    )

}