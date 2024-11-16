import React from 'react';
import { useCart } from '../cartContext';


const styles = {
  dishItem: {
    border: '1px solid #ccc',
    padding: '10px',
    borderRadius: '5px',
    margin: '10px 0',
    display: 'flex',
    marginLeft: '10%',
    marginRight: '10%',
    justifyContent: 'space-between'
  },
  image: {
    width: '60px',
    height: '60px',
    objectFit: 'cover',
    margin: '5px'
  },
  dishButton:{
    border: 'none',
    background: '#1677ff',
    color: 'white',
    padding: '12px 24px',
    margin: '8px',
    fontSize: '14px',
    borderRadius: '8px',
    cursor: 'pointer',
    marginLeft: 'auto'
  },
  dishName:{
    display: 'flex',
    textAlign: 'left',
    marginLeft: '5%',
    width: '50%',
    alignItems: 'center' 
  },
  dishPrice:{
    display: 'flex',
    textAlign: 'center',
    marginLeft: 'auto',
    alignItems: 'center' 
  }
};

const RecordToAdd = ({ id, name, price, srcPic }) => {
  const defaultImage = 'https://via.placeholder.com/150/808080/808080?text=No+Image'; 
  const { addToCart } = useCart();


  return (
    <div id={`dish-${id}`} style={styles.dishItem}>
      <img src={srcPic || defaultImage} alt={name} style={styles.image} />
      <div style={styles.dishName}>{name}</div>
      <div style={styles.dishPrice}>Cena: {price} PLN</div>
      <button
        style={styles.dishButton}
        onClick={() => {
          addToCart({ id, name, price, srcPic: srcPic || defaultImage });
          alert(`Dodano ${name} do koszyka!`);
        }}
      >
        Dodaj do koszyka
      </button>
    </div>
  );
};

export default RecordToAdd;