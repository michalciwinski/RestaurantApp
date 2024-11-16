import React,{ useState } from 'react';
import { useCart } from '../cartContext';
import { FaTrashAlt} from 'react-icons/fa'; 
import axios from "../api/axiosService";
import TileButton from './TileButton';

const styles = {
    headerLabel:{
      fontSize: '26px',
      marginTop: '1rem'
    },
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
    dishButtonDelete:{
      display: 'flex',
      textAlign: 'center',
      marginLeft: '3%',
      marginRight: '2%',
      alignItems: 'center' 
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



  const Cart = () => {
    const { cart, clearCart, removeFromCart } = useCart();
    const total = cart.reduce((sum, item) => sum + item.price, 0);

    const [selectedTileId, setSelectedTileId] = useState(1);  

    const handleTileClick = (id) => {
        setSelectedTileId(id); 
    };

    const tiles = [
      { id: 1, title: 'Stolik 1' },
      { id: 2, title: 'Stolik 2' },
      { id: 3, title: 'Stolik 3' },
      { id: 4, title: 'Stolik 4' },
      { id: 5, title: 'Stolik 5' },
      { id: 6, title: 'Stolik 6' }
  ];

    const submitOrder = async () => {
    
      const dateOfOrder = new Date().toISOString();
      const bill = cart.reduce((total, item) => total + item.price, 0);
      const additionalComment = 'Stolik ' + selectedTileId; 
      console.log(additionalComment);
      const stateId = 1; // 1 = "preparing", hardcoded - TO DO
    
      const orderPositions = cart.map(item => ({
        tMenuId: item.id,
      }));
    
      const order = {
        dateOfOrder,
        bill,
        additionalComment,
        stateId,
        orderPositions,
      };
    
      try {
        console.log(JSON.stringify(order));
        const response = await axios.addOrder(order);
        if (response.status === 200) {
          alert('Zamówienie dodane poprawnie, prosimy oczekiwać na swoje dania');
        } else {
          alert('Błąd podczas dodawania zamówienia, skieruj się do obsługi.');
          console.error('', response.data);
        }
        clearCart();

      } catch (error) {
        console.error('Problem z serwerem', error);
      }
    };


    

    return (
      <div>
        <div style={styles.headerLabel}>Twój koszyk</div>
        <ul id='dishList'>
          {cart.map((item, index) => (
            <div key={item.uniqueId} style={styles.dishItem} value = {item.id}>
              <img src={item.srcPic} alt={item.name} style={styles.image} />
              <div style={styles.dishName}>{item.name}</div>
              <div style={styles.dishPrice}>Cena: {item.price} PLN</div>
              <div style={styles.dishButtonDelete}>
                <FaTrashAlt style={{ color: 'black' }} aria-hidden="true" onClick={() => removeFromCart(item.uniqueId)}/>
              </div>
            </div>
          ))}
        </ul>
        <div id='summary'>
          <h2>Podsumowanie</h2>
          <p>Łączna kwota: {total} PLN</p>
        </div>
        <div className="tile-container">
            {tiles.map(tile => (
                <TileButton
                    id={tile.id}
                    key={tile.id}
                    title={tile.title}
                    selected={selectedTileId === tile.id}
                    onClick={handleTileClick}
                />
            ))}
        </div>
        <button style={styles.dishButton} onClick={clearCart}>Opróżnij koszyk</button>
        <button style={styles.dishButton} onClick={submitOrder}>Złóż zamówienie</button>
      </div>
    );
  };

  

export default Cart;