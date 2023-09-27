import React, { Component } from "react";
import Menuposition from "../menuf/menutableposition";
import menupositionstyle from '../menuf/menutableposition.module.css';

const tableHeaderListStyle = menupositionstyle['tableheader-list'];
const tableHeaderStyle = menupositionstyle['tableheader'];

class MenuList extends Component {
    state = {
      menuItems: [], //data from get operation
    };

    
  
    componentDidMount() {
      fetch("https://localhost:7197/Controller_Menu/GetDishes")
        .then((response) => response.json())
        .then((data) => {
          this.setState({ menuItems: data });
        })
        .catch((error) => {
          console.error("Error:", error);
        });
    }
  
    render() {
      const { menuItems } = this.state;
  
      return (
        <div>
          <h2>Menu</h2>
          
            <ul className={tableHeaderListStyle}>
                <li className={tableHeaderStyle}>ID</li>
                <li className={tableHeaderStyle}>Name</li>
                <li className={tableHeaderStyle}>Description</li>
                <li className={tableHeaderStyle}>Price</li>
                <li className={tableHeaderStyle}>Dish Type</li>
                <li className={tableHeaderStyle}>Amount</li>
            </ul>

            <ul >
                {menuItems.map((position) => (
                    <Menuposition position={position} />
                ))}
            </ul>
        </div>
      );
    }
  }
  
  export default MenuList;