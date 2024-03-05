import React from "react";

const MenuFilter = ({ selectedCategory, onCategoryChange }) => {
  const categories = ["All", "Soup", "Main course", "Dessert", "Drink"]; 

  return (
    <div>
      <label>Filter by Dish Type:</label>
      <select
        value={selectedCategory}
        onChange={(e) => onCategoryChange(e.target.value)}
      >
        {categories.map((category) => (
          <option key={category} value={category}>
            {category}
          </option>
        ))}
      </select>
    </div>
  );
};

export default MenuFilter;