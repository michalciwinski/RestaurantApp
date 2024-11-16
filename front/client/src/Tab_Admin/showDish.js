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
    <div id={styles['single-dish-div']}>
      <div id={styles['single-dish-border-container']}> 

        <table id={styles['single-dish-table']}>
          <tbody >
            <tr>
              <td>Nazwa:</td>
              <td>{dish.name}</td>
            </tr>
            <tr>
              <td>Opis:</td>
              <td>{dish.description}</td>
            </tr>
            <tr>
              <td>Rodzaj dania:</td>
              <td>{dishType}</td>
            </tr>
            <tr>
              <td>Cena:</td>
              <td>{dish.price} PLN</td>
            </tr>
            <tr>
              <td>Składniki:</td>
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