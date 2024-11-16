import React, { useState, useEffect } from "react";
import endpointsService from "../api/axiosService";
import styles from"./addDish.module.css";
import { FaSave } from 'react-icons/fa'; 
import tempImage from './temp.png';

const AddDish = () => {

    const [values, setValues] = useState({
        Id: 0,
        Name: '',
        Description: '',
        Price: '',
        DishType: 1,
        ImageFile: null,
        ImageSrc: ''
    });

    const handleChange = (e) => {
        const { name, value } = e.target;
        setValues({
            ...values,
            [name]: value
        })
    };


    const defaultImageSrc = 'temp.png'
    const showPreview = e => {
        if (e.target.files && e.target.files[0]) {
            let ImageFile = e.target.files[0];
            console.log(ImageFile);
            const reader = new FileReader();
            reader.onload = x => {
                setValues({
                    ...values,
                    ImageFile,
                    ImageSrc: x.target.result
                })
            }
            reader.readAsDataURL(ImageFile)
        }
    }



    const handleSubmit = async (e) => {

        if (values.Price === "" || values.Name === "" || values.Description === "" || values.ImageFile === null) {
            alert("Złe dane");
            return;
        }
        const newDish = new FormData();
        newDish.append('Id', values.Id);
        newDish.append('Name', values.Name);
        newDish.append('Description', values.Description);
        newDish.append('Price', values.Price);
        newDish.append('DishType', values.DishType);
        newDish.append('ImageFile', values.ImageFile, values.ImageFile.name);

        inputFields.forEach(element => {
            newDish.append('Namee', element.ingredients);
        });


        try {
        const response = await endpointsService.addDish(newDish);
        if (response.status === 200) {
            window.confirm("Danie dodane poprawnie")
        }
        //return response;
        } catch (error) {
            console.error('Error:', error);
            window.confirm("Problem z dodaniem")
        }

        window.location.reload();
    };

    //ingredients handling
    const [inputFields, setInputFields] = useState([
        {ingredients: ''}
    ])

    const handleFormChange = (index, event) => {
        let data = [...inputFields];
        data[index][event.target.name] = event.target.value;
        setInputFields(data);
    }

    const addFields = () => {
        let newfield = { ingredients: ''}
    
        setInputFields([...inputFields, newfield])
    }

    const removeFields = (index) => {
        let data = [...inputFields];
        data.splice(index, 1)
        setInputFields(data)
    }

    return (
    
      <div id={styles['single-dish-div']}>
        <div id={styles['single-dish-container']} >
            <table id={styles['single-dish-table']}>
            <tbody>
                <tr>
                    <td>Nazwa</td>
                    <td><input type="text" name='Name' placeholder="Wpisz nazwe" onChange={handleChange}/></td>
                </tr>
                <tr>
                    <td>Opis</td>
                    <td><input type="text" name='Description' placeholder="Wpisz opis" onChange={handleChange}/></td>
                </tr>
                <tr>
                    <td>Rodzaj dania</td>
                    <td>
                        <select name='DishType' className={styles['dish-type-select-class']} onChange={handleChange}>
                        <option value="1">Starter</option>
                        <option value="2">Zupa</option>
                        <option value="3">Danie Główne</option>
                        <option value="4">Deser</option>
                        <option value="5">Napoje</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>Cena</td>
                    <td><input type="number" name='Price' placeholder="Wpisz cene" onChange={handleChange}/></td>
                </tr>
                <tr>
                    <td>Składniki</td>
                    <td>
                        {inputFields.map((input, index) => {
                        return (
                                <div key={index}>
                                    <input
                                        name='ingredients'
                                        className="ingredients"
                                        placeholder='Składnik'
                                        value={input.ingredients}
                                        onChange={event => handleFormChange(index, event)}
                                    />
                                    <button onClick={() => removeFields(index)}>Usuń</button>
                                </div>
                        )
                        })}
                        <button onClick={addFields}>Dodaj więcej..</button>
                    </td>
                </tr>
                <tr>
                    <td>Zdjęcie</td>
                    <td>
                        <input type="file" accept="image/*" onChange={showPreview}/>
                    </td>
                </tr>
                </tbody>
            </table>

            <button id={styles['button-add']} onClick={handleSubmit}> 
                <FaSave /> Zapisz
            </button>

        </div>
    </div>
    );
};
export default AddDish;