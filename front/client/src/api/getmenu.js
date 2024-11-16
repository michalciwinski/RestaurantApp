import React, { Component } from "react";
import Menuposition from "../Tab_Menu/menutableposition";
import menupositionstyle from '../Tab_Menu/menutableposition.module.css';
import MenuFilter from "../Tab_Menu/filtermenubutton.js"

const tableHeaderListStyle = menupositionstyle['tableheader-list'];
const tableHeaderStyle = menupositionstyle['tableheader'];

class MenuList extends Component {
    state = {
      menuItems: [], //data from get operation
      selectedCategory: "All", //first/defualt all data
    };

    
  
    componentDidMount() {
      fetch("https://localhost:7197/api/Menu/GetDishes")
        .then((response) => response.json())
        .then((data) => {
          this.setState({ menuItems: data });
        })
        .catch((error) => {
          console.error("Error:", error);
        });
    }
    handleCategoryChange = (category) => {
      this.setState({ selectedCategory: category });
    };

  
    render() {
      const { menuItems, selectedCategory } = this.state;
  
      return (
        <div>
          <h2>Menu</h2>
          
          <MenuFilter
            selectedCategory={selectedCategory}
            onCategoryChange={this.handleCategoryChange}
          />
            <ul className={tableHeaderListStyle}>
                <li className={tableHeaderStyle}>ID</li>
                <li className={tableHeaderStyle}>Name</li>
                <li className={tableHeaderStyle}>Description</li>
                <li className={tableHeaderStyle}>Price</li>
                <li className={tableHeaderStyle}>Dish Type</li>
                <li className={tableHeaderStyle}>Amount</li>
            </ul>

            <ul>
              {menuItems
                .filter(
                  (position) =>
                    selectedCategory === "All" || position.dishType === selectedCategory
                )
                .map((position) => (
                  <Menuposition key={position.id} position={position} />
                ))}
            </ul>


        </div>
      );
    }
  }
  
  export default MenuList;