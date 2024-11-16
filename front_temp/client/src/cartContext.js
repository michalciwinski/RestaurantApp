import React, { createContext, useState, useEffect, useContext } from 'react';
import { v4 as uuidv4 } from 'uuid';


const CartContext = createContext();

//custom hook do automatycznego czyszczenia koszyka po 30min
const useClearCartAfterInactivity = (clearCart) => {
  useEffect(() => {
    let timeout;
    const resetTimeout = () => {
      clearTimeout(timeout);
      timeout = setTimeout(() => {
        clearCart();
      }, 30 * 60 * 1000); 
    };

    window.addEventListener('mousemove', resetTimeout);
    window.addEventListener('keypress', resetTimeout);

    resetTimeout();

    return () => {
      window.removeEventListener('mousemove', resetTimeout);
      window.removeEventListener('keypress', resetTimeout);
    };
  }, [clearCart]);
};


export const CartProvider = ({ children }) => {
  const [cart, setCart] = useState(() => {
    //stanu koszyka z localStorage
    const savedCart = localStorage.getItem('cart');
    return savedCart ? JSON.parse(savedCart) : [];
  });

  useEffect(() => {
    //zapis stanu koszyka do localStorage gdy się zmienia
    localStorage.setItem('cart', JSON.stringify(cart));
  }, [cart]);

  const addToCart = (item) => {
    const itemWithUniqueId = { ...item, uniqueId: uuidv4() };
    setCart((prevCart) => [...prevCart, itemWithUniqueId]);
  };

  const removeFromCart = (uniqueId) => {
    setCart((prevCart) => prevCart.filter(cartItem => cartItem.uniqueId !== uniqueId));
  };

  const clearCart = () => {
    setCart([]);
    localStorage.removeItem('cart'); // czyszczenie localStorage
  };

  useClearCartAfterInactivity(clearCart);

  return (
    <CartContext.Provider value={{ cart, addToCart, removeFromCart, clearCart }}>
      {children}
    </CartContext.Provider>
  );
};

//kontekst koszyka
export const useCart = () => {
  return useContext(CartContext);
};

