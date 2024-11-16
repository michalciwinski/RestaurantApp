import React, { useState, useEffect } from 'react';
import styles from './menu.module.css';
import axios from "../api/axiosService";
import { useCart } from '../cartContext';


const dishTypes = {
  Starter: 'Przystawka',
  Soup: 'Zupa',
  'Main course': 'Danie główne',
  Dessert: 'Deser',
  Drink: 'Napoje',
};


const Menu = () => {
  const [dishes, setDishes] = useState([]);
  const [ingredients, setIngredients] = useState({});
  const [selectedDishId, setSelectedDishId] = useState(null);
  const [activeFilters, setActiveFilters] = useState(Object.keys(dishTypes));

  const defaultImage = 'https://via.placeholder.com/150/808080/808080?text=No+Image'; 
  const { addToCart } = useCart();

  useEffect(() => {
    fetchDishes();
  }, []);

  const fetchDishes = async () => {
    try {
      const response = await axios.getDishes();  
      console.log(response.data);
      setDishes(response.data);
    } catch (error) {
      console.error('Problem z pobraniem dań:', error);
    }
  };

  const fetchIngredients = async (dishId) => {
    try {
      const response = await axios.getIngredientsOfDish(dishId);
      setIngredients(prevState => ({ ...prevState, [dishId]: response.data }));
      console.log(response);
      setSelectedDishId(dishId);
    } catch (error) {
      console.error('Problem z pobraniem składników:', error);
    }
  };

  const toggleIngredients = (dishId) => {
    if (selectedDishId === dishId) {
      setSelectedDishId(null); 
    } else {
      if (!ingredients[dishId]) {
        fetchIngredients(dishId);
      } else {
        setSelectedDishId(dishId);
      }
    }
  };

  const toggleFilter = (type) => {
    setActiveFilters(prevFilters =>
      prevFilters.includes(type)
        ? prevFilters.filter(filter => filter !== type)
        : [...prevFilters, type]
    );
  };

  const filteredDishes = dishes.filter(dish => activeFilters.includes(dish.dishType));  

  const add = (dish) => {
    addToCart({ 
      id: dish.id, 
      name: dish.name, 
      price: dish.price, 
      srcPic: dish.srcPic || defaultImage 
    }) 
    alert(`Dodano ${dish.name} do koszyka!`);
  };


  return (
    <div>
      <div className={styles.filterContainer}>
        {Object.entries(dishTypes).map(([type, displayName]) => (
          <button
            key={type}
            className={`${styles.filterButton} ${activeFilters.includes(type) ? styles.active : ''}`}
            onClick={() => toggleFilter(type)}
          >
            {displayName}
          </button>
        ))}
      </div>
      <div className={styles.menuContainer}>
        {filteredDishes.length > 0 ? (
          filteredDishes.map(dish => (
            <div key={dish.id} className={styles.dishCard}>
              <h2 className={styles.dishName}>{dish.name}</h2>
              <img src={dish.srcPic || defaultImage} alt={dish.name} className={styles.dishImage} />
              <p className={styles.dishDescription}>{dish.description}</p>
              <p className={styles.dishType}>Rodzaj: {dishTypes[dish.dishType]}</p> 
              <p className={styles.dishPrice}>{dish.price} zł</p>
              <button 
                className={styles.addToCartButton} 
                onClick={() => add(dish) }>
                Dodaj do koszyka
              </button>
              <button className={styles.showIngredientsButton} onClick={() => toggleIngredients(dish.id)}>
                {selectedDishId === dish.id ? 'Ukryj składniki' : 'Pokaż składniki'}
              </button>
              {selectedDishId === dish.id && ingredients[dish.id] && (
                <ul className={styles.ingredientsList}>
                  {ingredients[dish.id].map((ingredient, index) => (
                    <li key={index} className={styles.ingredientItem}>{ingredient}</li>
                  ))}
                </ul>
              )}
            </div>
          ))
        ) : (
          <p className={styles.noDishesMessage}>Brak dań do wyświetlenia</p>
        )}
      </div>
    </div>
  );
};
export default Menu;