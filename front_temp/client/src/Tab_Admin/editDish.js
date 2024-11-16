import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Loader from "../loader";
import endpointsService from "../api/axiosService";
import styles from "./editDish.module.css";
import { FaSave } from 'react-icons/fa'; 


const EditDish = () => {
  const [dish, setDish] = useState([]);
  const [error, setError] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const { id } = useParams();
  const navigate = useNavigate();

  useEffect(() => {
    getDish_();
  }, []);

  const getDish_ = () => {
    endpointsService
      .getDish(id)
      .then((item) => {
        setDish(item.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  const handleChange = (e) => {
    e.preventDefault();
    const { name, value } = e.target;
    setDish({ ...dish, [name]: value });
    console.log(dish);
  };



  const handleSubmit = (e) => {
    e.preventDefault();

  };

  let dishType;
  switch (dish.dishType) {
    case 'Starter':
      dishType = 'Starter';
      break;
    case 'Main course':
      dishType = 'Danie główne';
      break;
    case 'Soup':
      dishType = 'Zupa';
      break;
    case 'Dessert':
      dishType = 'Deser';
      break;
    case 'Drink':
      dishType = 'Napój';
      break;
    default:
      dishType = dish.dishType;
      break;
  }

  return (
    <div id={styles['edit-dish-div']}>
      <div id={styles['edit-dish-border-container']}>

      <table id={styles['edit-dish-table']}>
        <tbody >
          <tr>
            <td>
              <label for="name">
                Nazwa:
              </label>
            </td>
            <td>
              <input type="text" name="name" value={dish.name} onChange={handleChange}/>
              </td>
          </tr>
          <tr>
            <td>
              <label for="description">
                Opis:
              </label>
            </td>
            <td>
              <input type="text" name="description" value={dish.description} onChange={handleChange}/>
            </td>
          </tr>
          <tr>
            <td>
              <label for="dishType">
                Rodzaj dania:
              </label>
            </td>
            <td>
              <input type="text" name="dishType" value={dishType} onChange={handleChange}/>
            </td>
          </tr>
          <tr>
            <td>
              <label for="price">
                Cena:
              </label>
            </td>
            <td>
              <input type="text" name="price" value={dish.price} onChange={handleChange}/>
            </td>
          </tr>
          <tr>
            <td>
              <label for="price">
                Składniki:
              </label>
            </td>
            <td>
                {/* TO DO*/}
            </td>
          </tr>
        </tbody>
      </table>
      <img src={dish.srcPic} alt={dish.Name} id={styles['edit-picture-pict']}/> {/* Picture is not able to change. we have to delete record and add new one*/}
      <button id={styles['edit-button-confirm']} onClick={handleSubmit}> 
          <FaSave /> Potwierdź
      </button>
      </div>
    </div>
  );
};
export default EditDish;