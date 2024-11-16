interface Dish {
    id: number;
    name: string;
    ingredients: string[];
  }

  export default async function fetchDishesIngredients(): Promise<Dish[]> {
    try{
        const response = await fetch('https://localhost:7197/Controller_MenuWithIngredients/GetDishesWithIngredients');
        const data = await response.json();
        return data;
    }
    catch (error) {
        console.error('Error during load data from server', error);
        throw error;
    }

  }
