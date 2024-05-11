import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import endpointsService from "../api/axiosService";
import styles from './showDish.module.css';


const EditDish = () => {
  const [dish, setDish] = useState([]);
  const [dishIngredients, setDishIngredients] = useState([]);
  const { id } = useParams();


  useEffect(() => {
    getDish_();
    getIngredientsOfDish();
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

  const getIngredientsOfDish = () => {
    endpointsService
      .getIngredientsOfDish(id)
      .then((item) => {
        console.log(item.data);
        setDishIngredients(item.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

  

  return (
    <div id={styles['single-dish-div']}>
      <div id={styles['single-dish-border-container']}> 

        <table id={styles['single-dish-table']}>
          <tbody >
            <tr>
              <td>Name:</td>
              <td>{dish.name}</td>
            </tr>
            <tr>
              <td>Description:</td>
              <td>{dish.description}</td>
            </tr>
            <tr>
              <td>Dish type:</td>
              <td>{dish.dishType}</td>
            </tr>
            <tr>
              <td>Price:</td>
              <td>{dish.price}</td>
            </tr>
            <tr>
              <td>Ingredients:</td>
              <td>{dishIngredients.join(', ')}</td>
            </tr>
          </tbody>
        </table>
        <img src={dish.srcPic} alt={dish.Name} id={styles['single-picture-pict']}/>

      </div>
    </div>
  );
};
export default EditDish;