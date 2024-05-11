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

  return (
    <div id={styles['edit-dish-div']}>
      <div id={styles['edit-dish-border-container']}>

      <table id={styles['edit-dish-table']}>
        <tbody >
          <tr>
            <td>
              <label for="name">
                Name:
              </label>
            </td>
            <td>
              <input type="text" name="name" value={dish.name} onChange={handleChange}/>
              </td>
          </tr>
          <tr>
            <td>
              <label for="description">
                Description:
              </label>
            </td>
            <td>
              <input type="text" name="description" value={dish.description} onChange={handleChange}/>
            </td>
          </tr>
          <tr>
            <td>
              <label for="dishType">
                Dish Type:
              </label>
            </td>
            <td>
              <input type="text" name="dishType" value={dish.dishType} onChange={handleChange}/>
            </td>
          </tr>
          <tr>
            <td>
              <label for="price">
                Price
              </label>
            </td>
            <td>
              <input type="text" name="price" value={dish.price} onChange={handleChange}/>
            </td>
          </tr>
          <tr>
            <td>
              <label for="price">
                Ingredients:
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
          <FaSave /> Confirm
      </button>
      </div>
    </div>
  );
};
export default EditDish;