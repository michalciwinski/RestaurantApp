import React from 'react';
import './App.css';
import Navigationtemplate from './navigationtemplate/navigationtemplate.js';
import { CartProvider, useCart } from './cartContext';


function App() {
  return (
    <div className="App">
      <CartProvider>
        <Navigationtemplate/>       
      </CartProvider>
    </div>
  );
}

export default App;
