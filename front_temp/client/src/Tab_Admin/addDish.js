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
            alert("wrong inputs");
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
            window.confirm("Dish added succesfully")
        }
        //return response;
        } catch (error) {
            console.error('Error:', error);
            window.confirm("Failed to add item")
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
                    <td>Name</td>
                    <td><input type="text" name='Name' placeholder="Enter name" onChange={handleChange}/></td>
                </tr>
                <tr>
                    <td>Description</td>
                    <td><input type="text" name='Description' placeholder="Enter description" onChange={handleChange}/></td>
                </tr>
                <tr>
                    <td>Dish type</td>
                    <td>
                        <select name='DishType' className={styles['dish-type-select-class']} onChange={handleChange}>
                        <option value="1">Starter</option>
                        <option value="2">Soup</option>
                        <option value="3">Main course</option>
                        <option value="4">Dessert</option>
                        <option value="5">Drink</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>Price</td>
                    <td><input type="number" name='Price' placeholder="Enter price" onChange={handleChange}/></td>
                </tr>
                <tr>
                    <td>Ingredients</td>
                    <td>
                        {inputFields.map((input, index) => {
                        return (
                                <div key={index}>
                                    <input
                                        name='ingredients'
                                        className="ingredients"
                                        placeholder='Ingredient'
                                        value={input.ingredients}
                                        onChange={event => handleFormChange(index, event)}
                                    />
                                    <button onClick={() => removeFields(index)}>Remove</button>
                                </div>
                        )
                        })}
                        <button onClick={addFields}>Add More..</button>
                    </td>
                </tr>
                <tr>
                    <td>Image</td>
                    <td>
                        <input type="file" accept="image/*" onChange={showPreview}/>
                    </td>
                </tr>
                </tbody>
            </table>

            <button id={styles['button-add']} onClick={handleSubmit}> 
                <FaSave /> Save
            </button>

        </div>
    </div>
    );
};
export default AddDish;