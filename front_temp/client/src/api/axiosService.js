import axios from "./axiosInit";

class endpointsService {
  getToken (){
    const tokenData = JSON.parse(localStorage.getItem('token')); 
    const token = tokenData.token;
    return token;
  }
  async getDishes() {
    return axios.get("/Menu/GetDishes");
  }

  async getDishesWithIngredients() {
    return axios.get("/Menu_Ingredients/GetDishesWithIngredients");
  }

  async getDish(id) {
    return axios.get(`/Menu/GetDish/${id}`);
  }

  async getIngredientsOfDish(id){
    return axios.get(`/Menu/GetIngredientsOfDish/${id}`);
  }

  async deleteDish(id) {
    const token = this.getToken();
    const config = {     
      headers: {
                  'Authorization': `Bearer ${token}`
      }
    }
    return axios.delete(`/Menu/DeleteDish?id=${id}`, config);
  }

  async addDish(data) {
    const token = this.getToken();
    const config = {     
      headers: {  'content-type': 'multipart/form-data' ,
                  'Authorization': `Bearer ${token}`
      }
    }
    return axios.post("/Menu/AddDish", data, config);
  }

  async updateDish(data) {
    const token = this.getToken();
    const config = {     
      headers: {
                  'Authorization': `Bearer ${token}`
      }
    }
    return axios.put(`/Menu/UpdateDish`, data, config);
  }

  //ACCOUNT HANDLING
  async register(data) {
    const config = {     
      headers: { 'content-type': 'application/json' }
    }
    return axios.post("/userAccount/register", data, config);
  }

  async login(data) {
    const config = {     
      headers: { 'content-type': 'application/json' }
    }
    return axios.post("/userAccount/login", data, config);
  }

}

export default new endpointsService();