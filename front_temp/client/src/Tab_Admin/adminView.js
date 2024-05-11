import React, {useEffect,useState} from "react";
import {Link } from 'react-router-dom';
import styles from './adminView.module.css';
import endpointsService from "../api/axiosService";
import { FaPencilAlt, FaEye, FaTrashAlt, FaPlus} from 'react-icons/fa'; 


export default function AddDish() {

  const [dishes, setDishes] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);   

  const handleDelete = async (id) => {
    setIsLoading(true);
    try {
      const confirmed = window.confirm("Are you sure?");
      if (!confirmed) {
        return; 
      }
      const response = await endpointsService.deleteDish(id);
      if (response.status === 200) {
        window.confirm("Dish deleted succesfully")
      }
      setDishes(dishes.filter((item) => item.id !== id));
    } catch (error) {
      setError(error.message);
      window.confirm("Failed to delete item")
    } finally {
      window.location.reload();
      setIsLoading(false);
    }
};

  useEffect(() => {
    getDishes();
  }, []);

  const getDishes = () => {
    endpointsService
      .getDishes()
      .then((res) => {
        setDishes(res.data);
        console.log(res.data);
      })
      .catch((err) => {
        console.log(err);
      });
  };

    

    if (dishes.length < 0) {
      return <h1>no dishes found</h1>;
    } else {
    return (
        <div id={styles['main-div']}>
              <div id={styles['button-div']}>
                <div id = {styles['text']}>Create a new position</div>
                <Link to={`/Tab_Admin/addDish`} >
                  <button id={styles['create']}>Create<FaPlus/>
                  </button>            
                </Link>

              </div>
              <table id={styles['admin-view-table']}>
                <thead>
                  <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Description</th>
                    <th>Dish type</th>
                    <th>Price</th>
                    <th></th>
                  </tr>
                </thead>
                <tbody>
                  {dishes?.sort((a, b) => a.id - b.id).map((item, i) => {
                    return (
                      <tr key={i}>
                        <td>{item.id}</td>
                        <td>{item.name}</td>
                        <td>{item.description}</td>
                        <td>{item.dishType}</td>
                        <td>{item.price}</td>
                        <td>
                          <Link to={`/Tab_Admin/editDish/${item.id}`} id={item.id} >
                              <FaPencilAlt id={item.id} className="icon" style={{ color: 'black', marginRight: '3px'}} aria-hidden="true" />
                          </Link>
                          <Link to={`/Tab_Admin/getDish/${item.id}`} id={item.id} >
                              <FaEye id={item.id} className="icon" style={{ color: 'black', marginRight: '3px' }} aria-hidden="true" />
                          </Link>
                          <FaTrashAlt id={item.id} style={{ color: 'black' }} aria-hidden="true" onClick={() => handleDelete(item.id)}/>
                        </td>
                      </tr>
                    );
                  })}
                </tbody>
              </table>

        </div>
        
        

    )
  }
}